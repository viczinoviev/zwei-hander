using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	public class CollisionManager : ICollisionManager
	{
		/// <summary>
		/// Singleton pattern for one global collision manager
		/// </summary>
		private static CollisionManager _instance;
		private static readonly object _lock = new();

		/// <summary>
		/// All the things that can collide with each other
		/// </summary>
		private readonly List<ICollisionHandler> colliders = [];

		private CollisionManager() { }

		// Gets the one and only collision manager
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

        /// <summary>
        /// Called every frame to check for collisions
        /// </summary>
        /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
        public void Update(GameTime gameTime)
		{
			CheckCollisions(gameTime);
		}

        public void CheckCollisions(GameTime gameTime)
		{
			// Remove empty colliders
			for (int i = colliders.Count - 1; i >= 0; i--)
			{
				if (colliders[i] == null)
				{
					colliders.RemoveAt(i);
				}
			}

			// Check for any collisions
			for (int i = 0; i < colliders.Count; i++)
			{
				for (int j = i + 1; j < colliders.Count; j++)
				{
					if (colliders[i].CollisionBox.Intersects(colliders[j].CollisionBox))
					{
						CollisionInfo collisionInfoI = CalculateCollisionInfo(colliders[i].CollisionBox, colliders[j].CollisionBox);
						CollisionInfo collisionInfoJ = CalculateCollisionInfo(colliders[j].CollisionBox, colliders[i].CollisionBox);

						colliders[i].OnCollision(colliders[j], collisionInfoI);
						colliders[j].OnCollision(colliders[i], collisionInfoJ);
					}
				}
			}
		}

		/// <summary>
		/// Figures out collision details like which direction to push things
		/// </summary>
		/// <param name="movingRect">What is causing the collision</param>
		/// <param name="staticRect">What is being collided with</param>
		/// <returns>The information about this collision</returns>
		private static CollisionInfo CalculateCollisionInfo(Rectangle movingRect, Rectangle staticRect)
		{
			Rectangle intersection = Rectangle.Intersect(movingRect, staticRect);
			Vector2 intersectionCenter = new Vector2(
				intersection.X + intersection.Width / 2f,
				intersection.Y + intersection.Height / 2f
			);

			Direction normal;
			Vector2 resolutionOffset;

			// How much the rectangles overlap in each direction
			int leftOverlap = (movingRect.Right) - staticRect.Left;
			int rightOverlap = staticRect.Right - movingRect.Left;
			int topOverlap = movingRect.Bottom - staticRect.Top;
			int bottomOverlap = staticRect.Bottom - movingRect.Top;

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
			if (collider != null)
			{
				colliders.Add(collider);
			}
		}

		public void RemoveCollider(ICollisionHandler collider)
		{
			if (collider != null && colliders.Contains(collider))
			{
				colliders.Remove(collider);
			}
		}

	}

}