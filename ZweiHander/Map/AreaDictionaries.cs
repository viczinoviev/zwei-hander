using System;
using System.Collections.Generic;
using ZweiHander.Environment;
namespace ZweiHander.Map
{
    public static class AreaDictionaries
    {
        public static readonly Dictionary<int, BlockName> idToBlockName = new()
        {
            { 0, BlockName.SolidCyanTile },
            { 1, BlockName.SolidBlackTile },
            { 2, BlockName.BlockTile },
            { 3, BlockName.BrickTile },
            { 4, BlockName.StatueTile1 },
            { 5, BlockName.StatueTile2 },
            { 6, BlockName.TexturedTile },
            { 7, BlockName.StairTile },
            { 8, BlockName.WhitePatternTile },
            { 9, BlockName.FireTile },
            { 10, BlockName.LadderTile }
        };

        public static readonly Dictionary<string, BorderName> tagToBorderName = new()
        {
			// Wall tiles - w.{direction}
			{ "n", BorderName.WallTileNorth },
            { "w", BorderName.WallTileWest },
            { "e", BorderName.WallTileEast },
            { "s", BorderName.WallTileSouth },

			// Inside corners - w.i{direction}{direction}
			{ "ine", BorderName.InsideCornerNortheast },
            { "ise", BorderName.InsideCornerSoutheast },
            { "isw", BorderName.InsideCornerSouthwest },
            { "inw", BorderName.InsideCornerNorthwest },

			// Outside corners - w.o{direction}{direction}
			{ "osw", BorderName.OutsideCornerSouthwest },
            { "onw", BorderName.OutsideCornerNorthwest },
            { "one", BorderName.OutsideCornerNortheast },
            { "ose", BorderName.OutsideCornerSoutheast },

			// Entrance tiles - w.e{direction}
			{ "en", BorderName.EntranceTileNorth },
            { "ew", BorderName.EntranceTileWest },
            { "ee", BorderName.EntranceTileEast },
            { "es", BorderName.EntranceTileSouth },

			// Locked doors - w.l{direction}
			{ "ln", BorderName.LockedDoorTileNorth },
            { "lw", BorderName.LockedDoorTileWest },
            { "le", BorderName.LockedDoorTileEast },
            { "ls", BorderName.LockedDoorTileSouth },

			// Doors - w.d{direction}
			{ "dn", BorderName.DoorTileNorth },
            { "dw", BorderName.DoorTileWest },
            { "de", BorderName.DoorTileEast },
            { "ds", BorderName.DoorTileSouth },

			// Holes in walls - w.h{direction}
			{ "hn", BorderName.HoleInWallNorth },
            { "hw", BorderName.HoleInWallWest },
            { "he", BorderName.HoleInWallEast },
            { "hs", BorderName.HoleInWallSouth }
        };

        // Useless, but keeping for consistency and futureproofing
        public static readonly Dictionary<String, String> enemyNameToEnemyName = new()
        {
            { "Darknut", "Darknut" },
            { "Gel", "Gel" },
            { "Goriya", "Goriya" },
            { "Keese", "Keese" },
            { "Stalfos", "Stalfos" },
            { "Rope", "Rope" },
            { "Wallmaster", "Wallmaster" },
            { "Zol", "Zol" },
            { "Dodongo", "Dodongo" },
            { "Aquamentus", "Aquamentus" },
            { "BladeTrap", "BladeTrap" },
            { "OldMan", "OldMan" },
            { "MovingBlock", "MovingBlock" }
        };

        public static readonly Dictionary<string, string> itemNameToItemType = new()
        {
            { "Heart", "Heart" },
            { "Bomb", "Bomb" },
            { "Arrow", "Arrow" },
            { "Key", "Key" },
            { "Compass", "Compass"},
            { "Map", "MapItem" },
            { "HeartContainer", "HeartContainer" },
            { "TriforcePiece", "Triforce" },
            { "Boomerang", "Boomerang" },
            { "Bow", "Bow" },
            { "Clock", "Clock" },
            { "Fairy", "Fairy" },
            { "Fire", "Fire" },
            { "Rupy", "Rupy" },
            { "BlueCandle", "BlueCandle" },
            { "BluePotion", "BluePotion" },
            { "RedCandle", "RedCandle" },
            { "CagedKirby", "CagedKirby" },
            {"BlueKey", "BlueKey"}
        };
    }
}