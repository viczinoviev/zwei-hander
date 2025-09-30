using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Graphics;

/// <summary>
/// Completely immobile sprite.
/// </summary>
public class IdleSprite : AbstractSprite
{
    /// <summary>
    /// Creates a new idle sprite.
    /// </summary>
    /// <param name="region">The texture region containing the sprite.</param>
    /// <param name="spriteBatch">The spritebatch instance used for batching draw calls.</param>
    public IdleSprite(TextureRegion region, SpriteBatch spriteBatch, Boolean centered = true)    {
        _region = region;
        _spriteBatch = spriteBatch;

        if (centered)
        {
            Origin = new Vector2(
                _region.Width * 0.5f,
                _region.Height * 0.5f
                );
        }
    }
}
