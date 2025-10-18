using Microsoft.Xna.Framework;

namespace ZweiHander.CollisionFiles
{
    public struct CollisionInfo
    {
        // The normal direction of the collision (which side of the object was hit)
        public Direction Normal { get; }

        // The center point of the collision intersection
        public Vector2 CollisionPosition { get; }

        // The offset that should remove the object from inside the collision
        public Vector2 ResolutionOffset { get; }

        public CollisionInfo(Direction normal, Vector2 collisionPosition, Vector2 resolutionOffset)
        {
            Normal = normal;
            CollisionPosition = collisionPosition;
            ResolutionOffset = resolutionOffset;
        }
    }
}