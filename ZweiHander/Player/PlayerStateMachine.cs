using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

public class PlayerStateMachine : IStateMachine
{
    private Player _player;
    private PlayerState _currentState;
    private ISprite _currentSprite;
    private PlayerSprites _playerSprites;
    private float _moveSpeed = 100f;

    public PlayerStateMachine(PlayerSprites playerSprites, Player player)
    {
        _playerSprites = playerSprites;
        _player = player;
        _currentState = PlayerState.Idle;
        _currentSprite = _playerSprites.PlayerIdle();
    }

    public void SetState(PlayerState newState)
    {
        if (_currentState != newState)
        {
            _currentState = newState;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        switch (_currentState)
        {
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
                _currentSprite = _playerSprites.PlayerMoveRight();
                break;
            case PlayerState.Attacking:
                _currentSprite = _playerSprites.PlayerAttackSwordDown();
                break;
            default:
                _currentSprite = _playerSprites.PlayerMoveDown();
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (_player != null)
        {
            UpdatePosition(gameTime);
        }
        _currentSprite?.Update(gameTime);
    }

    private void UpdatePosition(GameTime gameTime)
    {
        Vector2 movement = Vector2.Zero;
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        switch (_currentState)
        {
            case PlayerState.MovingUp:
                movement.Y = -_moveSpeed * deltaTime;
                break;
            case PlayerState.MovingDown:
                movement.Y = _moveSpeed * deltaTime;
                break;
            case PlayerState.MovingLeft:
                movement.X = -_moveSpeed * deltaTime;
                break;
            case PlayerState.MovingRight:
                movement.X = _moveSpeed * deltaTime;
                break;
        }

        _player.Position += movement;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_player != null)
        {
            _currentSprite?.Draw(_player.Position);
        }
    }
}