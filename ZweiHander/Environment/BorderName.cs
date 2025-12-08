namespace ZweiHander.Environment;

/// <summary>
/// Enum for each border's Name (32x32 wall tiles)
/// </summary>
public enum BorderName
{
    // Wall tiles
    WallTileNorth,
    WallTileWest,
    WallTileEast,
    WallTileSouth,

    // Inside corners
    InsideCornerNortheast,
    InsideCornerSoutheast,
    InsideCornerSouthwest,
    InsideCornerNorthwest,

    // Outside corners
    OutsideCornerSouthwest,
    OutsideCornerNorthwest,
    OutsideCornerNortheast,
    OutsideCornerSoutheast,

    // Entrance tiles
    EntranceTileNorth,
    EntranceTileWest,
    EntranceTileEast,
    EntranceTileSouth,

    // Locked door tiles
    LockedDoorTileNorth,
    LockedDoorTileWest,
    LockedDoorTileEast,
    LockedDoorTileSouth,

    // Door tiles
    DoorTileNorth,
    DoorTileWest,
    DoorTileEast,
    DoorTileSouth,

    // Hole-in-wall tiles
    HoleInWallNorth,
    HoleInWallWest,
    HoleInWallEast,
    HoleInWallSouth
}
