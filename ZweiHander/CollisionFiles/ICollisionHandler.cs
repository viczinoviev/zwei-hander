using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
    /// <summary>
    /// Basic rules for anything that can be part of collisions
    /// </summary>
    public interface ICollisionHandler
	{
        /// <summary>
        /// Where this thing can be hit
        /// </summary>
        Rectangle CollisionBox { get; set; }

        /// <summary>
        /// What happens when this thing hits something else, action can depend on the type of the other collider as well as collision information
        /// </summary>
        /// <param name="other">What is being collided with.</param>
        /// <param name="collisionInfo">Info related to the collision.</param>
        public void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo);

        /// <summary>
        /// Updates where this thing can be hit.
        /// </summary>
        public void UpdateCollisionBox();

        /// <summary>
        /// Call this to stop this thing from being part of collisions.
        /// </summary>
        public void Unsubscribe();
    }
}