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
public class FlippingSprites : IdleSprite
{
    /// <summary>
    /// Time elapsed since last frame update
    /// </summary>
    private TimeSpan _elapsed;

    private TimeSpan _delay = TimeSpan.FromMilliseconds(200);

    private Boolean _anchor = false;
    private Vector2 _offset = Vector2.Zero;

    private IdleSprite _sprite;

    public FlippingSprites(TextureRegion region, SpriteBatch spriteBatch, bool centered = true) : base(region, spriteBatch, centered)
    {
    }

    public override void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= _delay)
        {
            _elapsed -= _delay;

            if (this.Effects == SpriteEffects.None)
            {
                this.Effects = SpriteEffects.FlipHorizontally;
            } else
            {
                this.Effects = SpriteEffects.None;
            }
        }
    }
}
