using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using System;

namespace ZweiHander.HUD
{
    /// <summary>
    /// Displays the player's health as heart containers
    /// </summary>
    public class InventoryHUD : IHUDComponent
    {
        private readonly ISprite _inventoryDisplayHUD;
        private readonly Vector2 _position;

        public InventoryHUD(HUDSprites hudSprites, Vector2 position)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _inventoryDisplayHUD = hudSprites.InventoryDisplay();
            _position = position; // Position is determined by HUDManager
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _inventoryDisplayHUD.Draw(_position);
        }
    }
}
