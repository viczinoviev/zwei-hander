using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.PlayerFiles{
    public class PlayerStateMachine(Player player) : IStateMachine
    {
        private readonly Player _player = player;
        private PlayerHandler _playerHandler;
        private PlayerState _currentState = PlayerState.Idle;
        private Vector2 _lastDirection = Vector2.UnitY; // Default facing down
        private Vector2 _currentMovementVector = Vector2.Zero;
        private float _actionTimer = 0f;
        private readonly float _attackDuration = 800f;
        private readonly float _itemUseDuration = 250f;
        private bool _itemUsedLastFrame = false;

        public PlayerState CurrentState => _currentState;
        public Vector2 LastDirection => _lastDirection;
        public Vector2 CurrentMovementVector => _currentMovementVector;
        public bool IsAttacking => _player.InputBuffer.Contains(PlayerInput.Attacking);
        public bool IsUsingItem => _currentState == PlayerState.UsingItem && _actionTimer > 0f;

        public bool CanUseAction()
        {
            return _actionTimer <= 0f;
        }

        public void SetPlayerHandler(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Update current movement vector from input buffer
            _currentMovementVector = GetMovementVectorFromInput();

            // Always update direction when we have movement input
            UpdateDirection();

            // Handle unified action timer
            if (_actionTimer > 0f)
            {
                _actionTimer -= deltaTime;
                if (_actionTimer <= 0f)
                {
                    _actionTimer = 0f;
                    ChangeState(PlayerState.Idle);
                }
                // Action in progress locks out other actions
                return;
            }

            // Process input buffer with hierarchy
            PlayerState newState = ProcessInputBuffer();
            if (newState != _currentState)
            {
                ChangeState(newState);
            }

            // Reset item usage flag at end of frame
            _itemUsedLastFrame = false;
        }

        private PlayerState ProcessInputBuffer()
        {
            var inputBuffer = _player.InputBuffer;

            // Priority: Attack > Item Use > Movement > Idle
            if (inputBuffer.Contains(PlayerInput.Attacking))
            {
                return PlayerState.Attacking;
            }
            else if (inputBuffer.Contains(PlayerInput.UsingItem1) || inputBuffer.Contains(PlayerInput.UsingItem2) || inputBuffer.Contains(PlayerInput.UsingItem3) || inputBuffer.Contains(PlayerInput.UsingItem4))
            {
                return PlayerState.UsingItem;
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

            // Start action timer based on state
            if (newState == PlayerState.Attacking)
            {
                _actionTimer = _attackDuration;

                _playerHandler.SpawnSwordProjectile();
            }
            else if (newState == PlayerState.UsingItem && !_itemUsedLastFrame)
            {
                _actionTimer = _itemUseDuration;
                _itemUsedLastFrame = true;

                // Pass the specific input to PlayerHandler
                if (_player.InputBuffer.Contains(PlayerInput.UsingItem1))
                {
                    _playerHandler.HandleItemUse(PlayerInput.UsingItem1);
                }
                else if (_player.InputBuffer.Contains(PlayerInput.UsingItem2))
                {
                    _playerHandler.HandleItemUse(PlayerInput.UsingItem2);
                }
                else if (_player.InputBuffer.Contains(PlayerInput.UsingItem3))
                {
                    _playerHandler.HandleItemUse(PlayerInput.UsingItem3);
                }
                else if(_player.InputBuffer.Contains(PlayerInput.UsingItem4))
                {
                    _playerHandler.HandleItemUse(PlayerInput.UsingItem4);
                }
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
}