using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.Map;
namespace ZweiHander.Environment
{
	/// <summary>
	/// Constructor initializes the factory with a tile size and block sprite storage
	/// </summary>
	public class BorderManager(int tileSize, BlockSprites blockSprites)
	{
        private readonly int _tileSize = tileSize; // 32 pixels for borders
        private readonly BlockSprites _blockSprites = blockSprites; // Borders use the same sprite sheet as blocks
		public List<Border> BorderMap { get; private set; } = [];

		/// <summary>
		/// Maps border names to their logic type
		/// </summary>
		private static readonly Dictionary<BorderName, BorderType> BorderNameToType = new()
        {
            // Wall tiles - all solid
            { BorderName.WallTileNorth, BorderType.Solid },
            { BorderName.WallTileWest, BorderType.Solid },
            { BorderName.WallTileEast, BorderType.Solid },
            { BorderName.WallTileSouth, BorderType.Solid },
            
            // Inside corners - all solid
            { BorderName.InsideCornerNortheast, BorderType.Solid },
            { BorderName.InsideCornerSoutheast, BorderType.Solid },
            { BorderName.InsideCornerSouthwest, BorderType.Solid },
            { BorderName.InsideCornerNorthwest, BorderType.Solid },
            
            // Outside corners - all solid
            { BorderName.OutsideCornerSouthwest, BorderType.Solid },
            { BorderName.OutsideCornerNorthwest, BorderType.Solid },
            { BorderName.OutsideCornerNortheast, BorderType.Solid },
            { BorderName.OutsideCornerSoutheast, BorderType.Solid },
            
            // Entrance tiles - with 24x24 cut-outs
            { BorderName.EntranceTileNorth, BorderType.EntranceUp },
            { BorderName.EntranceTileWest, BorderType.EntranceRight },
            { BorderName.EntranceTileEast, BorderType.EntranceLeft },
            { BorderName.EntranceTileSouth, BorderType.EntranceDown },
            
            // Locked doors - solid (until unlocked in future)
            { BorderName.LockedDoorTileNorth, BorderType.Solid },
            { BorderName.LockedDoorTileWest, BorderType.Solid },
            { BorderName.LockedDoorTileEast, BorderType.Solid },
            { BorderName.LockedDoorTileSouth, BorderType.Solid },
            
            // Doors - decorative (passable)
            { BorderName.DoorTileNorth, BorderType.EntranceUp },
            { BorderName.DoorTileWest, BorderType.EntranceRight },
            { BorderName.DoorTileEast, BorderType.EntranceLeft },
            { BorderName.DoorTileSouth, BorderType.EntranceDown },
            
            // Holes in walls - decorative
            { BorderName.HoleInWallNorth, BorderType.Decorative },
            { BorderName.HoleInWallWest, BorderType.Decorative },
            { BorderName.HoleInWallEast, BorderType.Decorative },
            { BorderName.HoleInWallSouth, BorderType.Decorative },
        };

		/// <summary>
		/// Creates a new border given its name and upper-left corner position
		/// Border covers 32x32 pixels
		/// </summary>
		public Border CreateBorder(BorderName name, Vector2 position)
        {

            Vector2 offsetPosition = new(position.X + 16, position.Y + 16);
            

            BorderType borderType = BorderNameToType[name];
			ISprite sprite = name switch
			{
				// Wall tiles
				BorderName.WallTileNorth => _blockSprites.WallTileNorth(),
				BorderName.WallTileWest => _blockSprites.WallTileWest(),
				BorderName.WallTileEast => _blockSprites.WallTileEast(),
				BorderName.WallTileSouth => _blockSprites.WallTileSouth(),
				// Inside corners
				BorderName.InsideCornerNortheast => _blockSprites.InsideCornerNortheast(),
				BorderName.InsideCornerSoutheast => _blockSprites.InsideCornerSoutheast(),
				BorderName.InsideCornerSouthwest => _blockSprites.InsideCornerSouthwest(),
				BorderName.InsideCornerNorthwest => _blockSprites.InsideCornerNorthwest(),
				// Outside corners
				BorderName.OutsideCornerSouthwest => _blockSprites.OutsideCornerSouthwest(),
				BorderName.OutsideCornerNorthwest => _blockSprites.OutsideCornerNorthwest(),
				BorderName.OutsideCornerNortheast => _blockSprites.OutsideCornerNortheast(),
				BorderName.OutsideCornerSoutheast => _blockSprites.OutsideCornerSoutheast(),
				// Entrance tiles
				BorderName.EntranceTileNorth => _blockSprites.EntranceTileNorth(),
				BorderName.EntranceTileWest => _blockSprites.EntranceTileWest(),
				BorderName.EntranceTileEast => _blockSprites.EntranceTileEast(),
				BorderName.EntranceTileSouth => _blockSprites.EntranceTileSouth(),
				// Locked doors
				BorderName.LockedDoorTileNorth => _blockSprites.LockedDoorTileNorth(),
				BorderName.LockedDoorTileWest => _blockSprites.LockedDoorTileWest(),
				BorderName.LockedDoorTileEast => _blockSprites.LockedDoorTileEast(),
				BorderName.LockedDoorTileSouth => _blockSprites.LockedDoorTileSouth(),
				// Doors
				BorderName.DoorTileNorth => _blockSprites.DoorTileNorth(),
				BorderName.DoorTileWest => _blockSprites.DoorTileWest(),
				BorderName.DoorTileEast => _blockSprites.DoorTileEast(),
				BorderName.DoorTileSouth => _blockSprites.DoorTileSouth(),
				// Holes in walls
				BorderName.HoleInWallNorth => _blockSprites.HoleInWallNorth(),
				BorderName.HoleInWallWest => _blockSprites.HoleInWallWest(),
				BorderName.HoleInWallEast => _blockSprites.HoleInWallEast(),
				BorderName.HoleInWallSouth => _blockSprites.HoleInWallSouth(),
				_ => _blockSprites.WallTileNorth(),
			};
			Border newBorder = new(name, borderType, offsetPosition, _tileSize, sprite);

            // Store borders in a map
            BorderMap.Add(newBorder);

            return newBorder;
        }

        public void Draw()
        {
            foreach (Border border in BorderMap)
            {
                border.Draw();
            }
        }

        public void Clear()
        {
            foreach (Border border in BorderMap)
            {
                border.UnsubscribeFromCollisions();
            }
            BorderMap.Clear();
        }

        public bool HasBorder(Border border)
        {
            return BorderMap.Contains(border);
        }
    }
}
