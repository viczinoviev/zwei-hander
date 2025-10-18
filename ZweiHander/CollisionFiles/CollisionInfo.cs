using Microsoft.Xna.Framework;

namespace ZweiHander.CollisionFiles
{
    // Holds all the info about a collision that just happened
    public struct CollisionInfo
    {
        // Which direction the collision came from
        public Direction Normal { get; }

        // Where the collision happened in the world
        public Vector2 CollisionPosition { get; }

        // How much to move to get out of the collision
        public Vector2 ResolutionOffset { get; }

        public CollisionInfo(Direction normal, Vector2 collisionPosition, Vector2 resolutionOffset)
        {
            Normal = normal;
            CollisionPosition = collisionPosition;
            ResolutionOffset = resolutionOffset;
        }
    }
}