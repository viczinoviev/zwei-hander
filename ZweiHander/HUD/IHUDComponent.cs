using Microsoft.Xna.Framework;

namespace ZweiHander.HUD
{
    public interface IHUDComponent
    {
        void Update(GameTime gameTime);

        void Draw(Vector2 offset);
    }
}
