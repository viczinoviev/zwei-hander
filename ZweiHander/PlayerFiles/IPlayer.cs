using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.PlayerFiles
{
    public interface IPlayer
    {
        Vector2 Position { get; set; }

        /// <summary>
        /// Current health in half-hearts (1 = half heart, 2 = full heart)
        /// </summary>
        int CurrentHealth { get; }

        /// <summary>
        /// Maximum health in half-hearts
        /// </summary>
        int MaxHealth { get; }

        void Update(GameTime gameTime);
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void Attack();
        void Idle();
        void Draw(SpriteBatch spriteBatch);

        void ForceUpdateCollisionBox();

        void SetUpdateEnabled(bool enabled);

        void clearSpawnedItems();
    }
}