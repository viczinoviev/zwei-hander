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
        void UseItem1();
        void UseItem2();
        void UseItem3();
        void UseItem4();
        void Idle();
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Gets the count of a specific item type in the player's inventory
        /// </summary>
        /// <param name="itemType">The type of item to count</param>
        /// <returns>The number of items of the specified type, or 0 if none</returns>
        int InventoryCount(Type itemType);

        void ForceUpdateCollisionBox();

        void SetUpdateEnabled(bool enabled);

        void ClearSpawnedItems();
    }
}