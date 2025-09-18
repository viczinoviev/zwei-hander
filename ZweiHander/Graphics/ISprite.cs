using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Graphics;

public interface ISprite
{
    public void Draw();

    public void Update(GameTime gameTime);
}
