using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	// Basic rules for managing all the collisions
	public interface ICollisionManager
	{
		// Check what's hitting what this frame
		void CheckCollisions(GameTime gameTime);
		// Add something new to collision checking
		void AddCollider(ICollisionHandler collider);
		// Remove something from collision checking
		void RemoveCollider(ICollisionHandler collider);
	}
}