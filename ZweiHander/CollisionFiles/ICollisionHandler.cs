using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	// Basic rules for anything that can be part of collisions
	public interface ICollisionHandler
	{
		// Where this thing can be hit
		Rectangle collisionBox { get; set; }
		// What happens when this thing hits something else, action can depend on the type of the other collider as well as collision information
		void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo);
	}
}