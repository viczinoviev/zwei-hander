using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IPlayer _player;
        // Ordering enum for Usables
        private enum OrderedUsable
        {
            Sword, Bow
        }
        private readonly List<Vector2> _usablePositions = [
            new(144, 62),
            new(193, 62)
        ];
        private readonly List<bool> AcquiredUsables;

        private readonly List<ISprite> _sprites;

        private readonly int OrderedUsableCount = Enum.GetNames(typeof(OrderedUsable)).Length;

        public InventoryHUD(HUDSprites hudSprites, Vector2 position, IPlayer player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _inventoryDisplayHUD = hudSprites.InventoryDisplay();
            _position = position; // Position is determined by HUDManager
            _relativePosition = position - new Vector2(_inventoryDisplayHUD.Width, _inventoryDisplayHUD.Height) / 2;
            _player = player;
            AcquiredUsables = [.. Enumerable.Repeat(false, OrderedUsableCount)];
            _sprites = [.. Enumerable.Repeat<ISprite>(null, OrderedUsableCount)];
            AcquiredUsables[(int) OrderedUsable.Sword] = true;
            AcquiredUsables[(int)OrderedUsable.Bow] = true;
            _sprites[(int)OrderedUsable.Sword] = hudSprites.NormalSword();
            _sprites[(int)OrderedUsable.Bow] = hudSprites.Bow();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _inventoryDisplayHUD.Draw(_position);
            for (int i = 0; i < OrderedUsableCount; i++)
            {
                if (AcquiredUsables[i]) _sprites[i].Draw(_usablePositions[i] + _relativePosition);
            }
        }
    }
}
