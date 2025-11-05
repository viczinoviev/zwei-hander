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
    public class HealthDisplay : IHUDComponent
    {
        private readonly IPlayer _player;
        private readonly HUDSprites _hudSprites;
        private readonly Vector2 _position;
        private const float HEART_SPACING = 16f; // Space between hearts, not gaps!
        
        public HealthDisplay(IPlayer player, HUDSprites hudSprites, Vector2 position)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _position = position; // Position is determined by HUDManager
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_player == null) return;
            
            float remainingHealthToDisplay = _player.CurrentHealth;

            int heartNum = 0;
            while (remainingHealthToDisplay > 0)
            {
                Vector2 heartPosition = _position + new Vector2(heartNum * HEART_SPACING, 0);
                ISprite heartSprite;

                if (remainingHealthToDisplay >= 2)
                {
                    heartSprite =  _hudSprites.HeartFull();
                    remainingHealthToDisplay -= 2;

                }
                else if (remainingHealthToDisplay >= 1)
                {
                    heartSprite =  _hudSprites.HeartHalf();
                    remainingHealthToDisplay -= 1;
                }
                else
                {
                    break;
                }
                
                heartSprite.Draw(heartPosition);
                heartNum++;
            }
        }
    }
}
