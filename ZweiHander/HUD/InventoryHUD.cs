using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.Items.ItemStorages;
using System.Diagnostics;

namespace ZweiHander.HUD
{
    /// <summary>
    /// Displays the player's health as heart containers
    /// </summary>
    public class InventoryHUD : IHUDComponent
    {
        private readonly ISprite _inventoryDisplayHUD;
        private readonly ISprite _redFrameHUD;
        private readonly ISprite _swordSprite;
        private readonly Vector2 _position;
        private readonly Vector2 _relativePosition;
        private readonly Player _player;
        private readonly Vector2 _selectedPositionB = new Vector2(264, 464);
        private readonly Vector2 _selectedPositionA = new Vector2(312, 464);

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < 0 || value >= _orderedUsableCount) return;
                _selectedIndex = value;
            }
        }

        public void SelectNext()
        {
            do
            {
                _selectedIndex = (_selectedIndex + 1) % _orderedUsableCount;
            }
            while (!_acquiredItems[_selectedIndex]);
        }

        public void SelectPrevious()
        {
            do
            {
                _selectedIndex = (_selectedIndex - 1 + _orderedUsableCount) % _orderedUsableCount;
            }
            while (!_acquiredItems[_selectedIndex]);
        }

        public OrderedUsable? GetSelectedItem()
        {
            return (OrderedUsable)_selectedIndex;
        }

        /// <summary>
        /// The items in the inventory, ordered in this enum
        /// </summary>
        public enum OrderedUsable
        {
            Boomerang, Bow, Bomb, Fire
        }

        /// <summary>
        /// The permanents in the inventory, ordered in this enum
        /// </summary>
        private enum OrderedPermanent
        {
            Map = 4
        }

        /// <summary>
        /// Position of each item relative to inventory sprite, ordered by enum
        /// </summary>
        private readonly List<Vector2> _itemPositions = [
            new(272, 106), new(320, 106), new(368, 106), new(416, 106),
            //new(272, 146), new(320, 146), new(368, 146), new(416, 146),
            new(271, 63)
        ];
        /// <summary>
        /// Which of the items in the enum does the player have
        /// </summary>
        private readonly List<bool> _acquiredItems;
        /// <summary>
        /// Sprites for drawing each item in the enum
        /// </summary>
        private readonly List<ISprite> _itemSprites;
        /// <summary>
        /// Number of permanents that CAN be displayed
        /// </summary>
        private readonly int _orderedPermanentCount = Enum.GetNames(typeof(OrderedPermanent)).Length;
        /// <summary>
        /// Number of usables that CAN be displayed
        /// </summary>
        private readonly int _orderedUsableCount = Enum.GetNames(typeof(OrderedUsable)).Length;
        /// <summary>
        /// Number of items that CAN be displayed
        /// </summary>
        private readonly int _orderedItemCount = Enum.GetNames(typeof(OrderedPermanent)).Length + Enum.GetNames(typeof(OrderedUsable)).Length;

        public InventoryHUD(HUDSprites hudSprites, Vector2 position, Player player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _inventoryDisplayHUD = hudSprites.InventoryDisplay();
            _redFrameHUD = hudSprites.RedFrame();
            _position = position; // Position is determined by HUDManager
            _relativePosition = position - new Vector2(_inventoryDisplayHUD.Width, _inventoryDisplayHUD.Height) / 2;
            _player = player;

            _acquiredItems = [.. Enumerable.Repeat(false, _orderedItemCount)];
            _itemSprites = [.. Enumerable.Repeat<ISprite>(null, _orderedItemCount)];
            _itemSprites[(int)OrderedUsable.Bow] = hudSprites.Bow();
            _itemSprites[(int)OrderedPermanent.Map] = hudSprites.Map();
            _itemSprites[(int)OrderedUsable.Fire] = hudSprites.OrangeCandle();
            _itemSprites[(int)OrderedUsable.Boomerang] = hudSprites.NormalBoomerang();
            _itemSprites[(int)OrderedUsable.Bomb] = hudSprites.Bomb();
            _swordSprite = hudSprites.NormalSword();
        }

        public void Update(GameTime gameTime)
        {
            _acquiredItems[(int)OrderedUsable.Bow] = _player.InventoryCount(typeof(Bow)) > 0;
            _acquiredItems[(int)OrderedPermanent.Map] = _player.InventoryCount(typeof(MapItem)) > 0;
            _acquiredItems[(int)OrderedUsable.Fire] = _player.InventoryCount(typeof(Fire)) > 0;
            _acquiredItems[(int)OrderedUsable.Boomerang] = _player.InventoryCount(typeof(Boomerang)) > 0;
            _acquiredItems[(int)OrderedUsable.Bomb] = _player.InventoryCount(typeof(Bomb)) > 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            _inventoryDisplayHUD.Draw(_position + offset);
            _swordSprite.Draw(_selectedPositionA + _relativePosition + offset);
            _redFrameHUD.Draw(_itemPositions[_selectedIndex] + _relativePosition + offset);
            _itemSprites[_selectedIndex].Draw(_selectedPositionB + _relativePosition + offset);
            for (int i = 0; i < _orderedItemCount; i++)
            {
                if (_acquiredItems[i]) _itemSprites[i].Draw(_itemPositions[i] + _relativePosition + offset);
            }
        }
    }
}
