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
        private readonly Vector2 _position;
        private readonly Vector2 _relativePosition;
        private readonly Player _player;

        private enum OrderedUsable
        {
            Sword, Bow
        }
        private readonly List<Vector2> _usablePositions = [
            new(144, 62),
            new(193, 62)
        ];
        private readonly List<bool> _acquiredUsables;
        private readonly List<ISprite> _usableSprites;
        private readonly int _orderedUsableCount = Enum.GetNames(typeof(OrderedUsable)).Length;

        public InventoryHUD(HUDSprites hudSprites, Vector2 position, Player player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _inventoryDisplayHUD = hudSprites.InventoryDisplay();
            _position = position; // Position is determined by HUDManager
            _relativePosition = position - new Vector2(_inventoryDisplayHUD.Width, _inventoryDisplayHUD.Height) / 2;
            _player = player;

            _acquiredUsables = [.. Enumerable.Repeat(false, _orderedUsableCount)];
            _usableSprites = [.. Enumerable.Repeat<ISprite>(null, _orderedUsableCount)];
            _acquiredUsables[(int) OrderedUsable.Sword] = true;
            _acquiredUsables[(int)OrderedUsable.Bow] = true;
            _usableSprites[(int)OrderedUsable.Sword] = hudSprites.NormalSword();
            _usableSprites[(int)OrderedUsable.Bow] = hudSprites.Bow();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            _inventoryDisplayHUD.Draw(_position + offset);
            for (int i = 0; i < _orderedUsableCount; i++)
            {
                if (_acquiredUsables[i]) _usableSprites[i].Draw(_usablePositions[i] + _relativePosition + offset);
            }
        }
    }
}
