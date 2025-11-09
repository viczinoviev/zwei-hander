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
    public class HeadsUpHUD : IHUDComponent
    {
        private readonly ISprite _headsUpDisplayHUD;
        private readonly Vector2 _position;
        
        public HeadsUpHUD(HUDSprites hudSprites, Vector2 position)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _headsUpDisplayHUD = hudSprites.HeadsUpHUD();
            _position = position; // Position is determined by HUDManager
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _headsUpDisplayHUD.Draw(_position);
        }
    }
}
