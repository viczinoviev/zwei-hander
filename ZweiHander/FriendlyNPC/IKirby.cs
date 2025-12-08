using Microsoft.Xna.Framework;

namespace ZweiHander.FriendlyNPC
{
    public interface IKirby
    {
        Vector2 Position { get; set; }
        void Update(GameTime gameTime);
        void Draw();

    }
}
