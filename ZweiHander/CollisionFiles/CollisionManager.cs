using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	public class CollisionManager : ICollisionManager
	{
		private static CollisionManager _instance;
		private static readonly object _lock = new object();

		private readonly List<ICollisionHandler> colliders = new List<ICollisionHandler>();

		private CollisionManager() { }

		public static CollisionManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
							_instance = new CollisionManager();
					}
				}
				return _instance;
			}
		}

		public void Update(GameTime gameTime)
		{
			CheckCollisions(gameTime);
		}

		public void CheckCollisions(GameTime gameTime)
		{
			for (int i = 0; i < colliders.Count; i++)
			{
				for (int j = i + 1; j < colliders.Count; j++)
				{
					if (colliders[i].collisionBox.Intersects(colliders[j].collisionBox))
					{
						Rectangle intersection = Rectangle.Intersect(colliders[i].collisionBox, colliders[j].collisionBox);
						System.Console.WriteLine($"{colliders[i].GetType().Name} hit {colliders[j].GetType().Name}, overlap: {intersection.Width}x{intersection.Height}");
						
						// Calculate collision information for both objects
						CollisionInfo collisionInfoI = CalculateCollisionInfo(colliders[i].collisionBox, colliders[j].collisionBox);
						CollisionInfo collisionInfoJ = CalculateCollisionInfo(colliders[j].collisionBox, colliders[i].collisionBox);

						colliders[i].OnCollision(colliders[j], collisionInfoI);
						colliders[j].OnCollision(colliders[i], collisionInfoJ);
					}
				}
			}
		}

		private CollisionInfo CalculateCollisionInfo(Rectangle movingRect, Rectangle staticRect)
		{
			// Calculate intersection rectangle
			Rectangle intersection = Rectangle.Intersect(movingRect, staticRect);
			Vector2 intersectionCenter = new Vector2(
				intersection.X + intersection.Width / 2f,
				intersection.Y + intersection.Height / 2f
			);

			Direction normal;
			Vector2 resolutionOffset;

			// Overlaps in each direction, assumes that "our" collider is the moving one
			int leftOverlap = (movingRect.Right) - staticRect.Left;
			int rightOverlap = staticRect.Right - movingRect.Left;
			int topOverlap = movingRect.Bottom - staticRect.Top;
			int bottomOverlap = staticRect.Bottom - movingRect.Top;

			// Find the smallest overlap to determine the collision normal
			int minOverlap = Math.Min(Math.Min(leftOverlap, rightOverlap), Math.Min(topOverlap, bottomOverlap));

			if (minOverlap == leftOverlap)
			{
				normal = Direction.Left;
				resolutionOffset = new Vector2(-leftOverlap, 0);
			}
			else if (minOverlap == rightOverlap)
			{
				normal = Direction.Right;
				resolutionOffset = new Vector2(rightOverlap, 0);
			}
			else if (minOverlap == topOverlap)
			{
				normal = Direction.Up;
				resolutionOffset = new Vector2(0, -topOverlap);
			}
			else
			{
				normal = Direction.Down;
				resolutionOffset = new Vector2(0, bottomOverlap);
			}

			return new CollisionInfo(normal, intersectionCenter, resolutionOffset);
		}

		public void AddCollider(ICollisionHandler collider)
		{
			colliders.Add(collider);
		}

		public void RemoveCollider(ICollisionHandler collider)
		{
			colliders.Remove(collider);
		}

	}

}