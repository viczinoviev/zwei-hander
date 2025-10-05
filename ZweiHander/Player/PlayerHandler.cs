using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;

public class PlayerHandler
{
    private Player _player;
    private PlayerStateMachine _stateMachine;
    private PlayerSprites _playerSprites;
    private ISprite _currentSprite;
    
    private float _moveSpeed = 200f;
    private float _attackMoveSpeed = 50f;
    private PlayerState _lastState = PlayerState.Idle;
    private Vector2 _lastDirectionVector = Vector2.UnitY; // Default facing down

    public Color Color { get; set; } = Color.White;

    public PlayerHandler(PlayerSprites playerSprites, Player player, PlayerStateMachine stateMachine)
    {
        _playerSprites = playerSprites;
        _player = player;
        _stateMachine = stateMachine;
        _currentSprite = _playerSprites.PlayerIdle();
        _lastState = _stateMachine.CurrentState;
        _lastDirectionVector = _stateMachine.LastDirection;
    }

    private void UpdateSprite(PlayerState state, Vector2 directionVector)
    {
        switch (state)
        {
            case PlayerState.Attacking:
                // Determine attack sprite based on primary direction (prioritize horizontal)
                if (Math.Abs(directionVector.X) > Math.Abs(directionVector.Y))
                {
                    if (directionVector.X < 0)
                        _currentSprite = _playerSprites.PlayerAttackSwordLeft();
                    else
                        _currentSprite = _playerSprites.PlayerAttackSwordRight();
                }
                else
                {
                    if (directionVector.Y < 0)
                        _currentSprite = _playerSprites.PlayerAttackSwordUp();
                    else
                        _currentSprite = _playerSprites.PlayerAttackSwordDown();
                }
                break;
            case PlayerState.UsingItem:
                // Use item sprite based on direction
                if (Math.Abs(directionVector.X) > Math.Abs(directionVector.Y))
                {
                    if (directionVector.X < 0)
                        _currentSprite = _playerSprites.PlayerUseItemLeft();
                    else
                        _currentSprite = _playerSprites.PlayerUseItemRight();
                }
                else
                {
                    if (directionVector.Y < 0)
                        _currentSprite = _playerSprites.PlayerUseItemUp();
                    else
                        _currentSprite = _playerSprites.PlayerUseItemDown();
                }
                break;
            case PlayerState.Moving:

                if (directionVector.Length() < 0.1f)
                {
                    _currentSprite = _playerSprites.PlayerIdle();
                    break;
                }

                if (directionVector.X < 0)
                    _currentSprite = _playerSprites.PlayerMoveLeft();
                else if (directionVector.X > 0)
                    _currentSprite = _playerSprites.PlayerMoveRight();
                else if (directionVector.Y < 0)
                    _currentSprite = _playerSprites.PlayerMoveUp();
                else
                    _currentSprite = _playerSprites.PlayerMoveDown();
                break;
            case PlayerState.Idle:
                _currentSprite = _playerSprites.PlayerIdle();
                break;
            default:
                _currentSprite = _playerSprites.PlayerIdle();
                break;
        }
        _currentSprite.Color = this.Color;
    }

    public void Update(GameTime gameTime)
    {
        // Check if state or direction vector has changed
        PlayerState currentState = _stateMachine.CurrentState;
        Vector2 currentDirectionVector = _stateMachine.LastDirection;
        
        // Only update sprite when state or direction actually changes
        if (currentState != _lastState || currentDirectionVector != _lastDirectionVector)
        {
            _lastState = currentState;
            _lastDirectionVector = currentDirectionVector;
            UpdateSprite(currentState, currentDirectionVector);
        }
        
        UpdatePosition(gameTime);
        // Let the sprite animate automatically (AnimatedSprite handles this)
        _currentSprite.Update(gameTime);
    }

    private void UpdatePosition(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Don't move when using items
        if (_stateMachine.CurrentState == PlayerState.UsingItem)
            return;
        
        // Use different speed based on whether player is attacking
        float currentSpeed = _stateMachine.CurrentState == PlayerState.Attacking ? _attackMoveSpeed : _moveSpeed;

        // Get normalized movement vector from state machine
        Vector2 movementVector = _stateMachine.CurrentMovementVector;
        Vector2 movement = movementVector * currentSpeed * deltaTime;

        _player.Position += movement;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //always refresh color no matter if a different state has been triggered
        _currentSprite.Color = this.Color;

        _currentSprite.Draw(_player.Position);
    }
    
    public void HandleItemUse(PlayerInput itemInput)
    {
        Vector2 itemPosition = _player.Position;
        Vector2 itemVelocity = _stateMachine.LastDirection * 300f;
        
        ItemType itemType;
        if (itemInput == PlayerInput.UsingItem1)
        {
            itemType = ItemType.Arrow;
            _player.ItemManager.GetItem(
                itemType,
                life: 2.0,
                position: itemPosition,
                velocity: itemVelocity
            );
        }
        else if (itemInput == PlayerInput.UsingItem2)
        {
            itemType = ItemType.Boomerang;
            _player.ItemManager.GetItem(
                itemType,
                life: 2.15f,
                position: itemPosition,
                velocity: itemVelocity,
                acceleration: -itemVelocity*0.9f
            );
        }
        else
        {
            return;
        }
        
    }
}
