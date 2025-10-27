using Microsoft.Xna.Framework;

namespace ZweiHander.Map
{
    /// <summary>
    /// Represents the room in the game's dungeon
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Standard width of a room
        /// </summary>
        public const int ROOM_WIDTH = 512;

        /// <summary>
        /// Standard height of a room
        /// </summary>
        public const int ROOM_HEIGHT = 320;

        /// <summary>
        /// top-left position of this room
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// width and height of this room
        /// </summary>
        public Vector2 Size { get; }

        /// <summary>
        /// Bounding rectangle of this room
        /// </summary>
        private Rectangle Bounds;

        /// <summary>
        /// Creates a new Room
        /// </summary>
        /// <param name="position">Top-left position of the room</param>
        /// <param name="size">width and height of the room</param>
        public Room(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            Bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)size.X,
                (int)size.Y
            );
        }

        /// <summary>
        /// Checks if a given position is inside the room
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <returns>True if the position is within the room bounds</returns>
        public bool Contains(Vector2 position)
        {
            return Bounds.Contains(position);
        }
    }
}
