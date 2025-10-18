using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	public interface ICollisionManager
	{
		void CheckCollisions(GameTime gameTime);
		void AddCollider(ICollisionHandler collider);
		void RemoveCollider(ICollisionHandler collider);
	}
}