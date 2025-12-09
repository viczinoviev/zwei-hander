using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.HUD
{
    public interface IHUDComponent
    {
        void Update(GameTime gameTime);

        void Draw(Vector2 offset);
    }
}
