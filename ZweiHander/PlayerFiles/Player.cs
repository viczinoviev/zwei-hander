using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;

namespace ZweiHander.PlayerFiles
{
    public class Player : IPlayer
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerHandler _handler;
        private readonly ItemManager _itemManager;
        private Vector2 _position;
        private bool _isDamaged;
        private float _damageTimer;
        private const float DAMAGE_DURATION = 1.0f; // How long the damage state lasts

        // Health system (measured in half-hearts: 1 = half heart, 2 = full heart)
        private int _currentHealth;
        private int _maxHealth;
        private const int STARTING_HEARTS = 3; // 3 hearts = 6 half-hearts

        public Dictionary<Type, int> Inventory { get; private set; } = new Dictionary<Type, int>();

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }

        /// <summary>
        /// Current health in # of half-hearts (1 = half heart, 2 = full heart)
        /// </summary>
        public int CurrentHealth => _currentHealth;

        /// <summary>
        /// Maximum health in # of half-hearts
        /// </summary>
        public int MaxHealth => _maxHealth;

        public HashSet<PlayerInput> InputBuffer { get; private set; } = [];
        public PlayerState CurrentState => _stateMachine.CurrentState;
        public ItemManager ItemManager => _itemManager;
        public bool IsDamaged => _isDamaged;

        public Color Color
        {
            get => _handler.Color;
            set => _handler.Color = value;
        }
        public Player(PlayerSprites playerSprites, ItemSprites itemSprites, TreasureSprites treasureSprites)
        {
            _itemManager = new ItemManager(itemSprites, treasureSprites);
            _stateMachine = new PlayerStateMachine(this);
            _handler = new PlayerHandler(playerSprites, this, _stateMachine);
            _stateMachine.SetPlayerHandler(_handler);
            Position = Vector2.Zero;

            // Initialize health (3 hearts = 6 half-hearts)
            _maxHealth = STARTING_HEARTS * 2;
            _currentHealth = _maxHealth;
            Inventory[typeof(Bomb)] = 10;
            Inventory[typeof(Sword)] = 1;
            Inventory[typeof(Bow)] = 1;
        }

        public void Update(GameTime gameTime)
        {
            // Update damage state timer
            _handler?.UpdateCollisionBox();
            if (_isDamaged)
            {
                _damageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_damageTimer <= 0f)
                {
                    _isDamaged = false;
                    _damageTimer = 0f;
                }
            }

            _stateMachine.Update(gameTime);
            _handler.Update(gameTime);
            _itemManager.Update(gameTime);
        }

        public void AddInput(PlayerInput input)
        {
            InputBuffer.Add(input);
        }

        public void ClearInputBuffer()
        {
            InputBuffer.Clear();
        }

        public void AddItemToInventory(Type itemType)
        {
            if (Inventory.TryGetValue(itemType, out int value)) Inventory[itemType] = ++value;
            else Inventory[itemType] = 1;

        }

        public void RemoveItemFromInventory(Type itemType)
        {
            if (Inventory.TryGetValue(itemType, out int value)) Inventory[itemType] = Math.Max(--value, 0);
        }

        public int InventoryCount(Type itemType)
        {
            // If item is in Inventory, return its count, else there is 0 of this item
            return Inventory.TryGetValue(itemType, out int value) ? value : 0;
        }

        /// <summary>
        /// Attempts to damage the player. Returns true if damage was applied, false if player is already in damaged state.
        /// </summary>
        public bool TakeDamage(int damage = 1)
        {
            // Player is invulnerable while already damaged
            if (_isDamaged)
            {
                return false;
            }

            _currentHealth = Math.Max(0, _currentHealth - damage);
            _isDamaged = true;
            _damageTimer = DAMAGE_DURATION;
            return true;
        }

        /// <summary>
        /// Heals the player by the specified amount
        /// </summary>
        public void Heal(int amount)
        {
            _currentHealth = System.Math.Min(_maxHealth, _currentHealth + amount);
        }

        /// <summary>
        /// Increases maximum health (e.g., from heart containers)
        /// </summary>
        public void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            _currentHealth += amount; // Also heal when getting heart container
        }

        public void SetPositionFromCollision(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public void MoveUp()
        {
            AddInput(PlayerInput.MovingUp);
        }

        public void MoveDown()
        {
            AddInput(PlayerInput.MovingDown);
        }

        public void MoveLeft()
        {
            AddInput(PlayerInput.MovingLeft);
        }

        public void MoveRight()
        {
            AddInput(PlayerInput.MovingRight);
        }

        public void Attack()
        {
            AddInput(PlayerInput.Attacking);
        }

        public void UseItem1()
        {
            AddInput(PlayerInput.UsingItem1);
        }

        public void UseItem2()
        {
            AddInput(PlayerInput.UsingItem2);
        }

        public void UseItem3()
        {
            AddInput(PlayerInput.UsingItem3);
        }

        public void Idle()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _handler.Draw();
            _itemManager.Draw();
        }

        public void ForceUpdateCollisionBox()
        {
            _handler?.UpdateCollisionBox();
        }

    }
}