using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

public class PlayerHandler
{
    private Player _player;
    private PlayerStateMachine _stateMachine;
    private PlayerSprites _playerSprites;
    private ISprite _currentSprite;
    
    private float _moveSpeed = 200f;
    private float _attackMoveSpeed = 50f;
    private PlayerState _lastObservedState = PlayerState.Idle;

    public PlayerHandler(PlayerSprites playerSprites, Player player, PlayerStateMachine stateMachine)
    {
        _playerSprites = playerSprites;
        _player = player;
        _stateMachine = stateMachine;
        _currentSprite = _playerSprites.PlayerIdle();
    }

    private void UpdateSprite(PlayerState state, Direction lastDirection)
    {
        switch (state)
        {
            case PlayerState.Attacking:
                switch (lastDirection)
                {
                    case Direction.Up:
                        _currentSprite = _playerSprites.PlayerAttackSwordUp();
                        break;
                    case Direction.Down:
                        _currentSprite = _playerSprites.PlayerAttackSwordDown();
                        break;
                    case Direction.Left:
                        _currentSprite = _playerSprites.PlayerAttackSwordLeft();
                        break;
                    case Direction.Right:
                        _currentSprite = _playerSprites.PlayerAttackSwordRight();
                        break;
                }
                break;
            case PlayerState.MovingUp:
                _currentSprite = _playerSprites.PlayerMoveUp();
                break;
            case PlayerState.MovingDown:
                _currentSprite = _playerSprites.PlayerMoveDown();
                break;
            case PlayerState.MovingRight:
                _currentSprite = _playerSprites.PlayerMoveRight();
                break;
            case PlayerState.MovingLeft:
                _currentSprite = _playerSprites.PlayerMoveLeft();
                break;
            case PlayerState.Idle:
                _currentSprite = _playerSprites.PlayerIdle();
                break;
            default:
                _currentSprite = _playerSprites.PlayerIdle();
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        // Check for state changes and update sprite if needed
        if (_stateMachine.CurrentState != _lastObservedState)
        {
            _lastObservedState = _stateMachine.CurrentState;
            UpdateSprite(_stateMachine.CurrentState, _stateMachine.LastDirection);
        }
        
        UpdatePosition(gameTime);
        _currentSprite.Update(gameTime);
    }

    private void UpdatePosition(GameTime gameTime)
    {
        Vector2 movement = Vector2.Zero;
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Use different speed based on whether player is attacking
		float currentSpeed;
		if (_stateMachine.CurrentState == PlayerState.Attacking)
		{
			currentSpeed = _attackMoveSpeed;
		}
		else
		{
			currentSpeed = _moveSpeed;
		}

        switch (_player.CurrentInput)
        {
            case PlayerInput.MovingUp:
                movement.Y = -currentSpeed * deltaTime;
                break;
            case PlayerInput.MovingDown:
                movement.Y = currentSpeed * deltaTime;
                break;
            case PlayerInput.MovingLeft:
                movement.X = -currentSpeed * deltaTime;
                break;
            case PlayerInput.MovingRight:
                movement.X = currentSpeed * deltaTime;
                break;
        }

        _player.Position += movement;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentSprite.Draw(_player.Position);
    }
}
