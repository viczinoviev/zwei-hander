using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.Environment;

namespace ZweiHander.Map
{
    public static class RoomSpawnHelper
    {
        private static readonly BorderName[] EastEntrances =
		[
			BorderName.DoorTileEast,
            BorderName.HoleInWallEast,
            BorderName.EntranceTileEast,
            BorderName.LockedDoorTileEast
        ];

        private static readonly BorderName[] WestEntrances = 
        [
            BorderName.DoorTileWest,
            BorderName.HoleInWallWest,
            BorderName.EntranceTileWest,
            BorderName.LockedDoorTileWest
        ];

        private static readonly BorderName[] NorthEntrances =
		[
			BorderName.DoorTileNorth,
            BorderName.HoleInWallNorth,
            BorderName.EntranceTileNorth,
            BorderName.LockedDoorTileNorth
        ];

        private static readonly BorderName[] SouthEntrances =
		[
			BorderName.DoorTileSouth,
            BorderName.HoleInWallSouth,
            BorderName.EntranceTileSouth,
            BorderName.LockedDoorTileSouth
        ];
        /// <summary>
        /// Calculates the player spawn point for a room based on its borders and predefined spawn point.
        /// If a spawn point is set, uses that. Otherwise, finds the first entrance border and spawns near it.
        /// </summary>
        public static Vector2 GetPlayerSpawnPoint(
            Vector2 predefinedSpawnPoint,
            IEnumerable<(BorderName borderName, Vector2 position)> borderData,
            Rectangle roomBounds,
            int tileSize,
            int roomNumber)
        {
            if (predefinedSpawnPoint != Vector2.Zero)
            {
                return predefinedSpawnPoint;
            }

            foreach (var (borderName, position) in borderData)
            {
                if (EastEntrances.Contains(borderName))
                {
                    return position + new Vector2(-tileSize, 0);
                }
                if (WestEntrances.Contains(borderName))
                {
                    return position + new Vector2(2 * tileSize, 0);
                }
                if (NorthEntrances.Contains(borderName))
                {
                    return position + new Vector2(0, 2 * tileSize);
                }
                if (SouthEntrances.Contains(borderName))
                {
                    return position + new Vector2(0, -tileSize);
                }
            }

            Console.WriteLine($"No spawn point or entrance in room {roomNumber}");
            return roomBounds.Center.ToVector2();
        }
    }
}
