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
    public class MapHUD : IHUDComponent
    {
        private readonly ISprite _mapDisplayHUD;
        private readonly Vector2 _position;

        public MapHUD(HUDSprites hudSprites, Vector2 position)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _mapDisplayHUD = hudSprites.MapDisplay();
            _position = position; // Position is determined by HUDManager
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            _mapDisplayHUD.Draw(_position + offset);
        }
    }
}
