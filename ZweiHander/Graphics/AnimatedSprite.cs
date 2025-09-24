using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZweiHander.Graphics;

/// <summary>
/// Immobile sprite with animation.
/// </summary>
public class AnimatedSprite : ISprite
{

    /// <summary>
    /// The texture region containing the sprite
    /// </summary>
    private TextureRegion _region;

    /// <summary>
    /// The SpriteBatch to draw to
    /// </summary>
    private SpriteBatch _spriteBatch;

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
    private Animation _animation;


    // Default parameters for draw

    /// <summary>
    /// The color mask to apply when drawing this sprite on screen
    /// </summary>
    private Color Color = Color.White;

    /// <summary>
    /// The amount of rotation, in radians, to apply when drawing this sprite on screen
    /// </summary>
    private float Rotation = 0.0f;

    /// <summary>
    /// The center of rotation, scaling, and position when drawing this sprite on screen
    /// </summary>
    private Vector2 Origin = Vector2.Zero;

    /// <summary>
    /// The amount of scaling to apply to the x- and y-axes when drawing this sprite on screen
    /// </summary>
    private Vector2 Scale = Vector2.One;

    /// <summary>
    /// Specifies if this sprite should be flipped horizontally, vertically, or both when drawing on screen
    /// </summary>
    private SpriteEffects Effects = SpriteEffects.None;

    /// <summary>
    /// The depth of the layer to use when drawing this sprite on screen
    /// </summary>
    private float LayerDepth = 0.0f;


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
