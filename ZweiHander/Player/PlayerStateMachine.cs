using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PlayerStateMachine : IStateMachine
{
    private Player _player;
    private PlayerState _currentState;
    private Vector2 _lastDirection = Vector2.UnitY; // Default facing down
    private Vector2 _currentMovementVector = Vector2.Zero;
    private float _attackTimer = 0f;
    private float _attackDuration = 800f;
    
    public PlayerState CurrentState => _currentState;
    public Vector2 LastDirection => _lastDirection;
    public Vector2 CurrentMovementVector => _currentMovementVector;
    public bool IsAttacking => _player.InputBuffer.Contains(PlayerInput.Attacking);

    public PlayerStateMachine(Player player)
    {
        _player = player;
        _currentState = PlayerState.Idle;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        
        // Update current movement vector from input buffer
        _currentMovementVector = GetMovementVectorFromInput();
        
        // Always update direction when we have movement input
        UpdateDirection();
        
        // Handle attack timer
        if (_attackTimer > 0f)
        {
            _attackTimer -= deltaTime;
            if (_attackTimer <= 0f)
            {
                _attackTimer = 0f;
                ChangeState(PlayerState.Idle);
            }
            // Attack locks out other actions
            return; 
        }

        // Process input buffer with hierarchy
        PlayerState newState = ProcessInputBuffer();
        if (newState != _currentState)
        {
            ChangeState(newState);
        }
    }

    private PlayerState ProcessInputBuffer()
    {
        var inputBuffer = _player.InputBuffer;
        
        // Priority-based if-else chain: Attack > Movement > Idle
        if (inputBuffer.Contains(PlayerInput.Attacking))
        {
            return PlayerState.Attacking;
        }
        else if (inputBuffer.Contains(PlayerInput.MovingUp) || 
                 inputBuffer.Contains(PlayerInput.MovingDown) || 
                 inputBuffer.Contains(PlayerInput.MovingLeft) || 
                 inputBuffer.Contains(PlayerInput.MovingRight))
        {
            return PlayerState.Moving;
        }
        else
        {
            return PlayerState.Idle;
        }
    }

    private Vector2 GetMovementVectorFromInput()
    {
        Vector2 movement = Vector2.Zero;
        var inputBuffer = _player.InputBuffer;
        
        if (inputBuffer.Contains(PlayerInput.MovingUp)) movement.Y -= 1;
        if (inputBuffer.Contains(PlayerInput.MovingDown)) movement.Y += 1;
        if (inputBuffer.Contains(PlayerInput.MovingLeft)) movement.X -= 1;
        if (inputBuffer.Contains(PlayerInput.MovingRight)) movement.X += 1;
        
        // Normalize diagonal movement
        if (movement != Vector2.Zero)
            movement.Normalize();
            
        return movement;
    }

    private void ChangeState(PlayerState newState)
    {
        _currentState = newState;

        // Start attack timer
        if (newState == PlayerState.Attacking)
        {
            _attackTimer = _attackDuration;
        }
    }
    
    private void UpdateDirection()
    {
        // Don't update direction while attacking - player is locked in their attack direction
        if (_currentState == PlayerState.Attacking)
        {
            return;
        }
        
        // Update direction based on current movement input whenever we have movement
        if (_currentMovementVector != Vector2.Zero)
        {
            _lastDirection = _currentMovementVector;
        }
    }


}