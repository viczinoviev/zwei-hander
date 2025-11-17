using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZweiHander.Graphics;

/// <summary>
/// Immobile sprite with animation.
/// </summary>
public class AnimatedSprite : AbstractSprite
{

    /// <summary>
    /// Frame number of current frame
    /// </summary>
    private int _currentFrame;

    /// <summary>
    /// Time elapsed since last frame update
    /// </summary>
    private TimeSpan _elapsed;

    /// <summary>
    /// Animation object containing animation info for the sprite
    /// </summary>
    readonly Animation _animation;

    private Boolean _anchor = false;
    private Vector2 _offset = Vector2.Zero;


    /// <summary>
    /// Creates a new animated sprite with the specified animation.
    /// </summary>
    /// <param name="spriteBatch">The spritebatch instance used for batching draw calls.</param>
    /// <param name="animation">The animation for this animated sprite.</param>
    /// <param name="centered">Whether the origin of the sprite should be centered.</param>
    public AnimatedSprite(SpriteBatch spriteBatch, Animation animation, Boolean centered = true)
    {
        _animation = animation;
        _region = _animation.Frames[0];
        _spriteBatch = spriteBatch;

        if (centered)
        {
            Origin = new Vector2(
                _region.Width * 0.5f,
                _region.Height * 0.5f
                );
        }
    }

    
    public override void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            if (_currentFrame >= _animation.Frames.Count)
            {
                _currentFrame = 0;
            }

            _region = _animation.Frames[_currentFrame];

            if (_anchor)
            {
                AnchorBottomRight(_offset);
            }

        }
    }

    public void AnchorBottomRight(Vector2 offset)
    {
        Origin = new Vector2(_region.Width - offset.X, _region.Height - offset.Y);
        _offset = offset;
        _anchor = true;
    }

}
