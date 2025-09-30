using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PlayerStateMachine : IStateMachine
{
    private Player _player;
    private PlayerState _currentState;
    private Direction _lastDirection = Direction.Down;
    private float _attackTimer = 0f;
    private float _attackDuration = 800f;
    
    public PlayerState CurrentState => _currentState;
    public Direction LastDirection => _lastDirection;

    public PlayerStateMachine(Player player)
    {
        _player = player;
        _currentState = PlayerState.Idle;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        
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

        // Process input and change state
        PlayerState newState = GetStateFromInput(_player.CurrentInput);
        if (newState != _currentState)
        {
            ChangeState(newState);
        }
    }

    private PlayerState GetStateFromInput(PlayerInput input)
    {
        // More logic will be added later to handle more complex state changes
        switch (input)
        {
            case PlayerInput.Attacking:
                return PlayerState.Attacking;
            case PlayerInput.MovingUp:
                return PlayerState.MovingUp;
            case PlayerInput.MovingDown:
                return PlayerState.MovingDown;
            case PlayerInput.MovingLeft:
                return PlayerState.MovingLeft;
            case PlayerInput.MovingRight:
                return PlayerState.MovingRight;
            default:
                return PlayerState.Idle;
        }
    }

    private void ChangeState(PlayerState newState)
    {
        _currentState = newState;

        // Update direction for movement and attacking
        if (newState == PlayerState.MovingUp) _lastDirection = Direction.Up;
        else if (newState == PlayerState.MovingDown) _lastDirection = Direction.Down;
        else if (newState == PlayerState.MovingLeft) _lastDirection = Direction.Left;
        else if (newState == PlayerState.MovingRight) _lastDirection = Direction.Right;

        // Start attack timer
        if (newState == PlayerState.Attacking)
        {
            _attackTimer = _attackDuration;
        }
    }


}