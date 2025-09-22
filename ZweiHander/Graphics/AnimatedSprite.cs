using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZweiHander.Graphics;
public class AnimatedSprite : ISprite
{

    // The texture region containing the sprite
    private TextureRegion _region;
    // The SpriteBatch to draw to
    private SpriteBatch _spriteBatch;

    // Frame number of current frame
    private int _currentFrame;
    // Time elapsed since last frame update
    private TimeSpan _elapsed;
    // Animation object containing animation info for the sprite
    private Animation _animation;

    // Default parameters for draw
    private Color Color = Color.White;
    private float Rotation = 0.0f;
    private Vector2 Origin = Vector2.Zero;
    private Vector2 Scale = Vector2.One;
    private SpriteEffects Effects = SpriteEffects.None;
    private float LayerDepth = 0.0f;


    /// <summary>
    /// Creates a new animated sprite with the specified frames and delay.
    /// </summary>
    /// <param name="animation">The animation for this animated sprite.</param>
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

    /// <summary>
    /// Updates this animated sprite.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime)
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
        }
    }

    public void Draw(Vector2 position)
    {
        _region.Draw(_spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }

}
