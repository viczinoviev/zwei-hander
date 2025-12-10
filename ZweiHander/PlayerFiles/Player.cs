using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Damage;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;
using Effect = ZweiHander.Damage.Effect;

namespace ZweiHander.PlayerFiles
{
    public class Player : IPlayer
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerHandler _handler;
        private bool _isDamaged;
        private float _damageTimer;
        private const float DAMAGE_DURATION = 1.0f; // How long the damage state lasts

        // Health system (measured in half-hearts: 1 = half heart, 2 = full heart)
        private int _currentHealth;
        private int _maxHealth;
        private const int STARTING_HEARTS = 3; // 3 hearts = 6 half-hearts

        public Dictionary<Type, int> Inventory { get; } = [];
        public UsableItem EquippedItem { get; set; } = UsableItem.None;

        // Slot-to-item mapping (Used for HUD and inventory to index into these items)
        private static readonly Dictionary<int, (UsableItem item, Type itemType)> _itemSlots = new()
        {
            { 0, (UsableItem.Boomerang, typeof(Boomerang)) },
            { 1, (UsableItem.Bow, typeof(Bow)) },
            { 2, (UsableItem.Bomb, typeof(Bomb)) },
            { 3, (UsableItem.RedCandle, typeof(RedCandle)) },
            { 4, (UsableItem.RedPot, typeof(RedPotion)) },
            { 5, (UsableItem.BluePot, typeof(BluePotion)) },
        };

        public Vector2 Position { get; set; }

        /// <summary>
        /// Current health in # of half-hearts (1 = half heart, 2 = full heart)
        /// </summary>
        public int CurrentHealth => _currentHealth;

        /// <summary>
        /// Maximum health in # of half-hearts
        /// </summary>
        public int MaxHealth => _maxHealth;

        public HashSet<PlayerInput> InputBuffer { get; } = [];
        public PlayerState CurrentState => _stateMachine.CurrentState;
        public ItemManager ItemManager { get; }
        public bool IsDamaged => _isDamaged;

        public Game1 GameInstance;

        public bool allowedToUpdate = true;

        public EffectManager Effects { get; } = [];

        public Color Color
        {
            get => _handler.Color;
            set => _handler.Color = value;
        }
        public Player(Game1 game, PlayerSprites playerSprites, ItemSprites itemSprites, TreasureSprites treasureSprites, ContentManager content)
        {
            ItemManager = new ItemManager(itemSprites, treasureSprites);
            var collisionHandler = new PlayerCollisionHandler(this, content);
            _stateMachine = new PlayerStateMachine(this, collisionHandler, content);
            _handler = new PlayerHandler(playerSprites, this, _stateMachine, collisionHandler, content);
            _stateMachine.SetPlayerHandler(_handler);
            Position = Vector2.Zero;
            GameInstance = game;

            // Initialize health (3 hearts = 6 half-hearts)
            _maxHealth = STARTING_HEARTS * 2;
            _currentHealth = _maxHealth;
            Inventory[typeof(Sword)] = 1;
            Inventory[typeof(Boomerang)] = 1;
        }

        public void Update(GameTime gameTime)
        {
            if (!allowedToUpdate) return;
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
            ItemManager.Update(gameTime);
            ApplyEffects(gameTime);
        }

        public void AddInput(PlayerInput input)
        {
            InputBuffer.Add(input);
        }

        public void ClearInputBuffer()
        {
            InputBuffer.Clear();
        }

        public void SetUpdateEnabled(bool enabled)
        {
            allowedToUpdate = enabled;
        }


        public void AddItemToInventory(Type itemType, int count = 1)
        {
            if (Inventory.TryGetValue(itemType, out int value)) Inventory[itemType] = value + count;
            else Inventory[itemType] = count;

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
        /// Attempts to damage the player. Returns true if damage was applied, false otherwise.
        /// </summary>
        /// <param name="damage">Damage to try to apply.</param>
        /// <param name="iframes">Whether to account for and give iframes</param>
        /// <returns>Whether damage was applied.</returns>
        public bool TakeDamage(DamageObject damage = null, bool iframes = true)
        {
            // Player is invulnerable while already damaged
            if (iframes && _isDamaged)
            {
                return false;
            }

            damage ??= new DamageObject(1);
            _currentHealth = Math.Max(0, _currentHealth - damage.Damage);
            if (iframes)
            {
                _isDamaged = true;
                _damageTimer = DAMAGE_DURATION;
            }
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
            Position = newPosition;
        }

        public void ClearSpawnedItems()
        {
            ItemManager.Clear();
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

        public void UseEquippedItem()
        {
            AddInput(PlayerInput.UsingEquippedItem);
        }

        public void EquipItemSlot(int slotIndex)
        {
            if (_itemSlots.TryGetValue(slotIndex, out var slot))
            {
                EquippedItem = slot.item;
            }
        }

        public bool HasItemInSlot(int slotIndex)
        {
            if (_itemSlots.TryGetValue(slotIndex, out var slot))
            {
                return InventoryCount(slot.itemType) > 0;
            }
            return false;
        }

        public void Idle()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _handler.Draw();
            ItemManager.Draw();
        }

        public void ForceUpdateCollisionBox()
        {
            _handler?.UpdateCollisionBox();
        }

        public void AddEffect(Effect effect, double duration)
        {
            Effects[effect] = duration;
        }

        public bool Effected(Effect effect)
        {
            return Effects.Contains(effect);
        }

        private void ApplyEffects(GameTime gameTime)
        {
            Effects.Update(gameTime);
            foreach (Effect effect in Effects.Ticked)
            {
                switch (effect)
                {
                    case Effect.Regen:
                        Heal(1);
                        break;
                    case Effect.OnFire:
                        TakeDamage(new(1), false);
                        break;
                }
            }
        }

    }
}