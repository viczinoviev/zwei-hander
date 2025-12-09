using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZweiHander.Graphics;

/// <summary>
/// Immobile sprite with animation.
/// </summary>
/// <remarks>
/// Creates a new animated sprite with the specified animation.
/// </remarks>
public class FlippingSprites(TextureRegion region, SpriteBatch spriteBatch, bool centered = true) : IdleSprite(region, spriteBatch, centered)
{
    /// <summary>
    /// Time elapsed since last frame update
    /// </summary>
    private TimeSpan _elapsed;

    private readonly TimeSpan _delay = TimeSpan.FromMilliseconds(200);

    public override void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= _delay)
        {
            _elapsed -= _delay;

            if (this.Effects == SpriteEffects.None)
            {
                this.Effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                this.Effects = SpriteEffects.None;
            }
        }
    }
}
