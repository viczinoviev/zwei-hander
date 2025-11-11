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
        private const int HEART_SPACING = 16; // Space between hearts, not gaps!
        
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

            int heartsToDisplay = (int)Math.Ceiling(_player.MaxHealth / 2.0f);
            int remainingHalfHearts = _player.CurrentHealth;

            for (int heartNum = 0; heartNum < heartsToDisplay; heartNum++)
            {
                Vector2 heartPosition = _position + new Vector2(heartNum * HEART_SPACING, 0);
                ISprite heartSprite;

                if (remainingHalfHearts >= 2)
                {
                    heartSprite =  _hudSprites.HeartFull();
                    remainingHalfHearts -= 2;

                }
                else if (remainingHalfHearts >= 1)
                {
                    heartSprite =  _hudSprites.HeartHalf();
                    remainingHalfHearts -= 1;
                }
                else
                {
                    heartSprite = _hudSprites.HeartEmpty();
                }

                heartSprite.Draw(heartPosition);
            }
        }
    }
}
