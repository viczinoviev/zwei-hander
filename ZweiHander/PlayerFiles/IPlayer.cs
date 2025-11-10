using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.PlayerFiles
{
    public interface IPlayer
    {
        Vector2 Position { get; set; }
        void Update(GameTime gameTime);
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void Attack();
        void Idle();
        void Draw(SpriteBatch spriteBatch);

        void ForceUpdateCollisionBox();
    }
}