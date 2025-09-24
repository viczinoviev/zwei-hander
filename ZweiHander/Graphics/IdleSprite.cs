using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Graphics;
public class IdleSprite : ISprite
{
    /// <summary>
    /// The texture region containing the sprite
    /// </summary>
    private TextureRegion _region;

    /// <summary>
    /// The SpriteBatch to draw to
    /// </summary>
    private SpriteBatch _spriteBatch;

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
    /// Creates a new idle sprite.
    /// </summary>
    /// <param name="region">The texture region containing the sprite.</param>
    /// <param name="spriteBatch">The spritebatch instance used for batching draw calls.</param>
    public IdleSprite(TextureRegion region, SpriteBatch spriteBatch)
    {
        _region = region;
        _spriteBatch = spriteBatch;
    }
    public void Update(GameTime gameTime)
    {
        // No update logic for idle sprite
    }

    public void Draw(Vector2 position)
    {
        _region.Draw(_spriteBatch, position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }

}
