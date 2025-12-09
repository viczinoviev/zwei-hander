using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;

namespace ZweiHander.HUD
{
    /// <summary>
    /// Displays the player's health as heart containers
    /// </summary>
    public class HealthDisplay(IPlayer player, HUDSprites hudSprites, Vector2 position) : IHUDComponent
    {
        private readonly IPlayer _player = player ?? throw new ArgumentNullException(nameof(player));
        private readonly HUDSprites _hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
        private const int HEART_SPACING = 16; // Space between hearts, not gaps!

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(Vector2 offset)
        {
            if (_player == null) return;

            int heartsToDisplay = (int)Math.Ceiling(_player.MaxHealth / 2.0f);
            int remainingHalfHearts = _player.CurrentHealth;

            for (int heartNum = 0; heartNum < heartsToDisplay; heartNum++)
            {
                Vector2 heartPosition = position + new Vector2(heartNum * HEART_SPACING, 0) + offset;
                ISprite heartSprite;

                if (remainingHalfHearts >= 2)
                {
                    heartSprite = _hudSprites.HeartFull();
                    remainingHalfHearts -= 2;

                }
                else if (remainingHalfHearts >= 1)
                {
                    heartSprite = _hudSprites.HeartHalf();
                    remainingHalfHearts--;
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
