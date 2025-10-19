using Microsoft.Xna.Framework;

namespace ZweiHander.CollisionFiles
{
    /// <summary>
    /// Holds all the info about a collision that just happened.
    /// </summary>
    public readonly struct CollisionInfo(Direction normal, Vector2 collisionPosition, Vector2 resolutionOffset)
    {
        /// <summary>
        /// Which direction the collision came from.
        /// </summary>
        public Direction Normal { get; } = normal;

        /// <summary>
        /// Where the collision happened in the world.
        /// </summary>
        public Vector2 CollisionPosition { get; } = collisionPosition;

        /// <summary>
        /// How much to move to get out of the collision.
        /// </summary>
        public Vector2 ResolutionOffset { get; } = resolutionOffset;
    }
}