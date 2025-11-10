using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;

namespace ZweiHander.PlayerFiles
{
    public class PlayerHandler
    {
        private readonly Player _player;
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerSprites _playerSprites;
        private readonly PlayerCollisionHandler _collisionHandler;
        private ISprite _currentSprite;
        private readonly float _moveSpeed = 250f;
        private readonly float _attackMoveSpeed = 50f;
        private PlayerState _lastState = PlayerState.Idle;
        private Vector2 _lastDirectionVector = Vector2.UnitY; // Default facing down

        public Vector2 movement = Vector2.Zero;

        // Blink effect settings for damage state
        private float _blinkTimer = 0f;
        private const float BLINK_INTERVAL = 0.075f; // How fast to blink
        private bool _isVisible = true;

        public Color Color { get; set; } = Color.White;

        public PlayerHandler(PlayerSprites playerSprites, Player player, PlayerStateMachine stateMachine)
        {
            _playerSprites = playerSprites;
            _player = player;
            _stateMachine = stateMachine;
            _currentSprite = _playerSprites.PlayerIdle();
            _lastState = _stateMachine.CurrentState;
            _lastDirectionVector = _stateMachine.LastDirection;
            _collisionHandler = new PlayerCollisionHandler(_player);
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

            // Handle damage blink effect
            if (_player.IsDamaged)
            {
                _blinkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_blinkTimer >= BLINK_INTERVAL)
                {
                    _isVisible = !_isVisible;
                    _blinkTimer = 0f;
                }
            }
            else
            {
                // Reset blinking effect
                _isVisible = true;
                _blinkTimer = 0f;
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
            movement = movementVector * currentSpeed * deltaTime;

            _player.Position += movement;
        }

        public void Draw()
        {
            // Apply blink effect when damaged
            if (_player.IsDamaged && !_isVisible)
            {
                // Make sprite transparent during blink
                _currentSprite.Color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                // Normal color
                _currentSprite.Color = this.Color;
            }

            _currentSprite.Draw(_player.Position);
        }

        public void SpawnSwordProjectile()
        {
            Vector2 swordPosition = _player.Position + _stateMachine.LastDirection * 10f;
            Vector2 swordVelocity = _stateMachine.LastDirection * 400f;

            _player.ItemManager.GetItem<SwordItem>(
                life: 1.1,
                position: swordPosition,
                velocity: swordVelocity
            );
        }

        public void HandleItemUse(PlayerInput itemInput)
        {
            Vector2 itemPosition = _player.Position;
            Vector2 itemVelocity = _stateMachine.LastDirection * 300f;
            if (itemInput == PlayerInput.UsingItem1)
            {
                _player.ItemManager.GetItem<ArrowItem>(
                    life: 1.1,
                    position: itemPosition,
                    velocity: itemVelocity,
                    properties: [ItemProperty.DeleteOnEnemy,
                    ItemProperty.DeleteOnBlock,
                    ItemProperty.CanDamageEnemy]
                );
            }
            else if (itemInput == PlayerInput.UsingItem2)
            {
                _player.ItemManager.GetItem<BoomerangItem>(
                    life: 2.15f,
                    position: itemPosition,
                    velocity: itemVelocity,
                    acceleration: -itemVelocity * 0.9f,
                    properties: [ItemProperty.DeleteOnBlock,
                         ItemProperty.CanDamageEnemy]
                );
            }
            else if (itemInput == PlayerInput.UsingItem3)
            {
                _player.ItemManager.GetItem<BombItem>(
                    life: 1.5f,
                    position: itemPosition + _stateMachine.LastDirection * 30f,
                    velocity: Vector2.Zero,
                    acceleration: Vector2.Zero
                );
            }
            else
            {
                return;
            }

        }

        public void UpdateCollisionBox()
        {
            _collisionHandler?.UpdateCollisionBox();
        }


    }
}