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
        private readonly ISprite _blueFrameHUD;
        private readonly Vector2 _position;
        private readonly Vector2 _relativePosition;
        private readonly Player _player;

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < 0 || value >= _orderedItemCount) return;
                _selectedIndex = value;
            }
        }

        public void SelectNext()
        {
            // Simple wrap-around
            _selectedIndex = (_selectedIndex + 1) % _orderedItemCount;
        }

        public void SelectPrevious()
        {
            _selectedIndex = (_selectedIndex - 1 + _orderedItemCount) % _orderedItemCount;
        }

        /// <summary>
        /// The items in the inventory, ordered in this enum
        /// </summary>
        private enum OrderedItem
        {
            Map,
            Sword, Boomerang, Bow, Fire
        }

        /// <summary>
        /// Position of each item relative to inventory sprite, ordered by enum
        /// </summary>
        private readonly List<Vector2> _itemPositions = [
            new(271, 63),
            new(272, 106), new(320, 106), new(368, 106), new(416, 106),
            new(272, 146), new(320, 146), new(368, 146), new(416, 146),
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
        /// Number of items that CAN be displayed
        /// </summary>
        private readonly int _orderedItemCount = Enum.GetNames(typeof(OrderedItem)).Length;

        public InventoryHUD(HUDSprites hudSprites, Vector2 position, Player player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _inventoryDisplayHUD = hudSprites.InventoryDisplay();
            _blueFrameHUD = hudSprites.BlueFrame();
            _position = position; // Position is determined by HUDManager
            _relativePosition = position - new Vector2(_inventoryDisplayHUD.Width, _inventoryDisplayHUD.Height) / 2;
            _player = player;

            _acquiredItems = [.. Enumerable.Repeat(false, _orderedItemCount)];
            _itemSprites = [.. Enumerable.Repeat<ISprite>(null, _orderedItemCount)];
            _itemSprites[(int)OrderedItem.Sword] = hudSprites.NormalSword();
            _itemSprites[(int)OrderedItem.Bow] = hudSprites.Bow();
            _itemSprites[(int)OrderedItem.Map] = hudSprites.Map();
            _itemSprites[(int)OrderedItem.Fire] = hudSprites.OrangeCandle();
            _itemSprites[(int)OrderedItem.Boomerang] = hudSprites.NormalBoomerang();
        }

        public void Update(GameTime gameTime)
        {
            _acquiredItems[(int)OrderedItem.Sword] = _player.InventoryCount(typeof(Sword)) > 0;
            _acquiredItems[(int)OrderedItem.Bow] = _player.InventoryCount(typeof(Bow)) > 0;
            _acquiredItems[(int)OrderedItem.Map] = _player.InventoryCount(typeof(MapItem)) > 0;
            _acquiredItems[(int)OrderedItem.Fire] = _player.InventoryCount(typeof(Fire)) > 0;
            _acquiredItems[(int)OrderedItem.Boomerang] = _player.InventoryCount(typeof(Boomerang)) > 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            _inventoryDisplayHUD.Draw(_position + offset);
            for (int i = 0; i < _orderedItemCount; i++)
            {
                if (_acquiredItems[i]) _itemSprites[i].Draw(_itemPositions[i] + _relativePosition + offset);
            }
            if(_acquiredItems[_selectedIndex]) _blueFrameHUD.Draw(_usablePositions[_selectedIndex] + _relativePosition + offset);
        }
    }
}
