using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Graphics;

public abstract class AbstractSprite : ISprite
{
    /// <summary>
    /// The texture region containing the sprite
    /// </summary>
    protected TextureRegion _region;

    private int UnrotatedHeight { get => (int) ((float) _region.Height * Scale.Y); }
    private int UnrotatedWidth { get => (int)((float)_region.Width * Scale.X); }

    public int Height { get => _sideways ?
            UnrotatedWidth : UnrotatedHeight;
    }

    public int Width
    {
        get => _sideways ?
            UnrotatedHeight : UnrotatedWidth;
    }
    /// <summary>
    /// The SpriteBatch to draw to
    /// </summary>
    protected SpriteBatch _spriteBatch;

    // Default parameters for draw

    /// <summary>
    /// The color mask to apply when drawing this sprite on screen
    /// </summary>
    public Color Color { get; set; } = Color.White;

    private float _rotation = 0f;

    /// <summary>
    /// The amount of rotation, in radians, to apply when drawing this sprite on screen
    /// </summary>
    public float Rotation { get => _rotation; set {
            _rotation = value;
            _sideways = Math.Abs(Math.Cos(value)) < Math.Abs(Math.Sin(value));
        } }

    private bool _sideways = false;

    /// <summary>
    /// The center of rotation, scaling, and position when drawing this sprite on screen
    /// </summary>
    public Vector2 Origin { get; set; } = Vector2.Zero;

    /// <summary>
    /// The amount of scaling to apply to the x- and y-axes when drawing this sprite on screen
    /// </summary>
    public Vector2 Scale { get; set; } = new Vector2(2, 2);

    /// <summary>
    /// Specifies if this sprite should be flipped horizontally, vertically, or both when drawing on screen
    /// </summary>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    /// The depth of the layer to use when drawing this sprite on screen
    /// </summary>
    protected float LayerDepth = 0.0f;

    public virtual void Update(GameTime time)
    {
        // Default implementation: No op
    }

    public virtual void Draw(Vector2 position)
    {
        _region.Draw(_spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}