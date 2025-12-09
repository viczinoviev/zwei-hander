using Microsoft.Xna.Framework;
namespace ZweiHander.CollisionFiles
{
    /// <summary>
    /// Basic rules for managing all the collisions
    /// </summary>
    public interface ICollisionManager
    {
        /// <summary>
        /// Check what's hitting what this frame
        /// </summary>
        /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
        void CheckCollisions(GameTime gameTime);

        /// <summary>
        /// Add something new to collision checking
        /// </summary>
        /// <param name="collider">New collidable to add.</param>
        void AddCollider(ICollisionHandler collider);

        /// <summary>
        /// Remove something from collision checking
        /// </summary>
        /// <param name="collider">Collidable to remove.</param>
        void RemoveCollider(ICollisionHandler collider);
    }
}