using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Graphics;

public abstract class AbstractSprite : ISprite
{
    /// <summary>
    /// The texture region containing the sprite
    /// </summary>
    protected TextureRegion _region;

    /// <summary>
    /// The SpriteBatch to draw to
    /// </summary>
    protected SpriteBatch _spriteBatch;

    // Default parameters for draw

    /// <summary>
    /// The color mask to apply when drawing this sprite on screen
    /// </summary>
    protected Color Color = Color.White;

    /// <summary>
    /// The amount of rotation, in radians, to apply when drawing this sprite on screen
    /// </summary>
    protected float Rotation = 0.0f;

    /// <summary>
    /// The center of rotation, scaling, and position when drawing this sprite on screen
    /// </summary>
    protected Vector2 Origin = Vector2.Zero;

    /// <summary>
    /// The amount of scaling to apply to the x- and y-axes when drawing this sprite on screen
    /// </summary>
    public Vector2 Scale = Vector2.One;

    /// <summary>
    /// Specifies if this sprite should be flipped horizontally, vertically, or both when drawing on screen
    /// </summary>
    public SpriteEffects Effects = SpriteEffects.None;

    /// <summary>
    /// The depth of the layer to use when drawing this sprite on screen
    /// </summary>
    protected float LayerDepth = 0.0f;

    public virtual void Update(GameTime time)
    {
        // Default implementation: No op
    }

    public void Draw(Vector2 position)
    {
        _region.Draw(_spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}