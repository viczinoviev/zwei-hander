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
    // The texture region containing the sprite
    private TextureRegion _region;
    // The SpriteBatch to draw to
    private SpriteBatch _spriteBatch;

    // Default parameters for draw
    private Color Color = Color.White;
    private float Rotation = 0.0f;
    private Vector2 Origin = Vector2.Zero;
    private Vector2 Scale = Vector2.One;
    private SpriteEffects Effects = SpriteEffects.None;
    private float LayerDepth = 0.0f;

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
