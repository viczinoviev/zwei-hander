using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ZweiHander.Map
{
    /// <summary>
    /// Hold the rooms defined in the dungeon
    /// </summary>
    public class Dungeon
    {
        /// <summary>
        /// List of rooms in dungeon
        /// </summary>
        private readonly List<Room> _rooms;

        public Dungeon()
        {
            _rooms = new List<Room>();
        }

        /// <summary>
        /// Adds a room to the dungeon
        /// </summary>
        /// <param name="room">The room to add</param>
        public void AddRoom(Room room)
        {
            _rooms.Add(room);
        }

        /// <summary>
        /// Returns the room that contains the given position.
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <returns>The room containing the position, null if none found</returns>
        public Room GetRoomAtPosition(Vector2 position)
        {
            foreach (Room room in _rooms)
            {
                if (room.Contains(position))
                {
                    return room;
                }
            }
            return null;
        }

    }
}
