using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

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
        private readonly float _attackMoveSpeed = 100f;

        private readonly float _itemUseMoveSpeed = 50f;
        private PlayerState _lastState = PlayerState.Idle;
        private Vector2 _lastDirectionVector = Vector2.UnitY;

        public Vector2 movement = Vector2.Zero;

        private float _blinkTimer = 0f;
        private const float BLINK_INTERVAL = 0.075f;
        private bool _isVisible = true;

        private readonly List<SoundEffect> Sounds;

        public Color Color { get; set; } = Color.White;

        public PlayerHandler(PlayerSprites playerSprites, Player player, PlayerStateMachine stateMachine, PlayerCollisionHandler collisionHandler, ContentManager content)
        {
            _playerSprites = playerSprites;
            _player = player;
            _stateMachine = stateMachine;
            _currentSprite = _playerSprites.PlayerIdle();
            _lastState = _stateMachine.CurrentState;
            _lastDirectionVector = _stateMachine.LastDirection;
            _collisionHandler = collisionHandler;
            Sounds = [
                content.Load<SoundEffect>("Audio/SwordAttack"),
                content.Load<SoundEffect>("Audio/Fireball")
            ];
        }

        private void UpdateSprite(PlayerState state, Vector2 directionVector)
        {
            switch (state)
            {
                case PlayerState.Attacking:
                    Sounds[0].Play();
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
            PlayerState currentState = _stateMachine.CurrentState;
            Vector2 currentDirectionVector = _stateMachine.LastDirection;

            if (currentState != _lastState || currentDirectionVector != _lastDirectionVector)
            {
                _lastState = currentState;
                _lastDirectionVector = currentDirectionVector;
                UpdateSprite(currentState, currentDirectionVector);
            }

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
                _isVisible = true;
                _blinkTimer = 0f;
            }

            UpdatePosition(gameTime);
            _currentSprite.Update(gameTime);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float currentSpeed = _moveSpeed;
            if (_stateMachine.CurrentState == PlayerState.Attacking)
            {
                currentSpeed = _attackMoveSpeed;
            }
            else if (_stateMachine.CurrentState == PlayerState.UsingItem)
            {
                currentSpeed = _itemUseMoveSpeed;
            }

            Vector2 movementVector = _stateMachine.CurrentMovementVector;
            Vector2 intendedMovement = movementVector * currentSpeed * deltaTime;

            Vector2 safeMovement = _collisionHandler.CalculateSafeMovement(intendedMovement);

            _player.Position += safeMovement;
        }

        public void Draw()
        {
            if (_player.IsDamaged && !_isVisible)
            {
                _currentSprite.Color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                _currentSprite.Color = this.Color;
            }

            _currentSprite.Draw(_player.Position);
        }

        public void SpawnSwordProjectile()
        {
            Vector2 swordPosition = _player.Position + _stateMachine.LastDirection * 10f;
            Vector2 swordVelocity = _stateMachine.LastDirection * 400f;

            _player.ItemManager.GetItem(
                "Sword",
                life: 1.1,
                position: swordPosition,
                velocity: swordVelocity
            );
        }

        public void UpdateCollisionBox()
        {
            _collisionHandler?.UpdateCollisionBox();
        }


    }
}