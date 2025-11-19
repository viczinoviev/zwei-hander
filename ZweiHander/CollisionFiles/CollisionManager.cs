using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZweiHander.CollisionFiles
{
	public class CollisionManager : ICollisionManager
	{
		private static CollisionManager _instance;
		private static readonly object _lock = new();

		readonly private List<ICollisionHandler> colliders = [];

		private CollisionManager() { }

		public static CollisionManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						_instance ??= new CollisionManager();
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
			for (int i = colliders.Count - 1; i >= 0; i--)
			{
				if (colliders[i].Dead == true || colliders[i] == null)
				{
					colliders.RemoveAt(i);
				}
			}

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

		private static CollisionInfo CalculateCollisionInfo(Rectangle movingRect, Rectangle staticRect)
		{
			Rectangle intersection = Rectangle.Intersect(movingRect, staticRect);
			Vector2 intersectionCenter = new (
				intersection.X + intersection.Width / 2f,
				intersection.Y + intersection.Height / 2f
			);

			Direction normal;
			Vector2 resolutionOffset;

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

		/// <summary>
		/// Adds specified collider
		/// </summary>
		/// <param name="collider">Collider to add</param>
		public void AddCollider(ICollisionHandler collider)
		{
			if (collider != null)
			{
				colliders.Add(collider);
			}
		}

		/// <summary>
		/// Removes specified collider
		/// </summary>
		/// <param name="collider">Collider to remove</param>
		public void RemoveCollider(ICollisionHandler collider)
		{
			if (collider != null && colliders.Contains(collider))
			{
				colliders.Remove(collider);
			}
		}

		/// <summary>
		/// Removes all colliders
		/// </summary>
		public void ClearAllColliders()
		{
			colliders.Clear();
		}

		/// <summary>
		/// Remove all dead and null colliders
		/// </summary>
		public void RemoveDeadColliders()
		{
			for (int i = colliders.Count - 1; i >= 0; i--)
			{
				if (colliders[i] == null || colliders[i].Dead)
				{
					colliders.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// Gets all colliders
		/// </summary>
		/// <returns>All colliders in this</returns>
		public IEnumerable<ICollisionHandler> GetAllColliders()
		{
			return colliders;
		}

		/// <summary>
		/// Prints number of colliders, then type and location of each
		/// </summary>
		public void PrintAllColliders()
		{
			System.Console.WriteLine($"=== CollisionManager has {colliders.Count} colliders ===");
			foreach (var collider in colliders)
			{
				string type = collider.GetType().Name;
				System.Console.WriteLine($"  - {type} at {collider.CollisionBox}");
			}
		}

		/// <summary>
		/// Finds any collisions with a test rectangle.
		/// </summary>
		/// <param name="testBox">Rectangle to check for collisions with</param>
		/// <returns>List of all collisions as (handler, info)</returns>
		public List<(ICollisionHandler handler, CollisionInfo info)> CheckCollisionsForOne(Rectangle testBox)
		{
			List<(ICollisionHandler, CollisionInfo)> collisions = [];

			foreach (var collider in colliders)
			{
				if (collider == null || collider.Dead)
					continue;

				if (testBox.Intersects(collider.CollisionBox))
				{
					CollisionInfo info = CalculateCollisionInfo(testBox, collider.CollisionBox);
					collisions.Add((collider, info));
				}
			}

			return collisions;
		}

	}

}