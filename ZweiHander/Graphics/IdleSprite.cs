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
    private TextureRegion _region;
    private SpriteBatch _spriteBatch;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0.0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.0f;

    public IdleSprite(TextureRegion region, SpriteBatch spriteBatch)
    {
        _region = region;
        Origin = new Vector2(_region.Width / 2, _region.Height / 2);
    }
    public void Update(GameTime gameTime)
    {
        // No update logic for idle sprite
    }

    public void Draw()
    {
        _region.Draw(_spriteBatch, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }

}
