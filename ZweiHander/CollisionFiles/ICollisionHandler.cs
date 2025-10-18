using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	public interface ICollisionHandler
	{
		Rectangle collisionBox { get; set; }
		void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo);
	}
}