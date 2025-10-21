using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;

namespace ZweiHander.PlayerFiles
{
    public class PlayerHandler
    {
        private readonly Player _player;
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerSprites _playerSprites;
        private readonly PlayerCollisionHandler _collisionHandler;
        private ISprite _currentSprite;
        private readonly float _moveSpeed = 500f;
        private readonly float _attackMoveSpeed = 50f;
        private PlayerState _lastState = PlayerState.Idle;
        private Vector2 _lastDirectionVector = Vector2.UnitY; // Default facing down

        public Vector2 movement = Vector2.Zero;

        public Color Color { get; set; } = Color.White;

        public PlayerHandler(PlayerSprites playerSprites, Player player, PlayerStateMachine stateMachine)
        {
            _playerSprites = playerSprites;
            _player = player;
            _stateMachine = stateMachine;
            _currentSprite = _playerSprites.PlayerIdle();
            _lastState = _stateMachine.CurrentState;
            _lastDirectionVector = _stateMachine.LastDirection;
            _collisionHandler = new PlayerCollisionHandler(_player, this);
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
            movement = movementVector * currentSpeed * deltaTime;

            _player.Position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //always refresh color no matter if a different state has been triggered
            _currentSprite.Color = this.Color;

            _currentSprite.Draw(_player.Position);
        }

        public void SpawnSwordProjectile()
        {
            Vector2 swordPosition = _player.Position + _stateMachine.LastDirection * 10f;
            Vector2 swordVelocity = _stateMachine.LastDirection * 400f;

            _player.ItemManager.GetItem(
                ItemType.Sword,
                life: 1.0,
                position: swordPosition,
                velocity: swordVelocity
            );
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
                    acceleration: -itemVelocity * 0.9f
                );
            }
            else if (itemInput == PlayerInput.UsingItem3)
            {
                itemType = ItemType.Bomb;
                _player.ItemManager.GetItem(
                    itemType,
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