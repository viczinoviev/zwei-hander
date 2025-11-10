using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Environment
{
    public class BorderFactory
    {
        private readonly int _tileSize; // 32 pixels for borders
        private readonly BlockSprites _blockSprites; // Borders use the same sprite sheet as blocks
        public List<Border> BorderMap { get; private set; } // Stores all borders

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
            { BorderName.DoorTileNorth, BorderType.Decorative },
            { BorderName.DoorTileWest, BorderType.Decorative },
            { BorderName.DoorTileEast, BorderType.Decorative },
            { BorderName.DoorTileSouth, BorderType.Decorative },
            
            // Holes in walls - decorative
            { BorderName.HoleInWallNorth, BorderType.Decorative },
            { BorderName.HoleInWallWest, BorderType.Decorative },
            { BorderName.HoleInWallEast, BorderType.Decorative },
            { BorderName.HoleInWallSouth, BorderType.Decorative },
        };

        /// <summary>
        /// Constructor initializes the factory with a tile size and block sprite storage
        /// </summary>
        public BorderFactory(int tileSize, BlockSprites blockSprites)
        {
            _tileSize = tileSize;
			_blockSprites = blockSprites;
            BorderMap = new List<Border>();
        }

        /// <summary>
        /// Creates a new border given its name and upper-left corner position
        /// Border covers 32x32 pixels
        /// </summary>
        public Border CreateBorder(BorderName name, Vector2 position)
        {

            Vector2 offsetPosition = new Vector2(position.X + 16, position.Y + 16);
            

            BorderType borderType = BorderNameToType[name];
            ISprite sprite;


            switch (name)
            {
                // Wall tiles
                case BorderName.WallTileNorth:
                    sprite = _blockSprites.WallTileNorth();
                    break;
                case BorderName.WallTileWest:
                    sprite = _blockSprites.WallTileWest();
                    break;
                case BorderName.WallTileEast:
                    sprite = _blockSprites.WallTileEast();
                    break;
                case BorderName.WallTileSouth:
                    sprite = _blockSprites.WallTileSouth();
                    break;

                // Inside corners
                case BorderName.InsideCornerNortheast:
                    sprite = _blockSprites.InsideCornerNortheast();
                    break;
                case BorderName.InsideCornerSoutheast:
                    sprite = _blockSprites.InsideCornerSoutheast();
                    break;
                case BorderName.InsideCornerSouthwest:
                    sprite = _blockSprites.InsideCornerSouthwest();
                    break;
                case BorderName.InsideCornerNorthwest:
                    sprite = _blockSprites.InsideCornerNorthwest();
                    break;

                // Outside corners
                case BorderName.OutsideCornerSouthwest:
                    sprite = _blockSprites.OutsideCornerSouthwest();
                    break;
                case BorderName.OutsideCornerNorthwest:
                    sprite = _blockSprites.OutsideCornerNorthwest();
                    break;
                case BorderName.OutsideCornerNortheast:
                    sprite = _blockSprites.OutsideCornerNortheast();
                    break;
                case BorderName.OutsideCornerSoutheast:
                    sprite = _blockSprites.OutsideCornerSoutheast();
                    break;

                // Entrance tiles
                case BorderName.EntranceTileNorth:
                    sprite = _blockSprites.EntranceTileNorth();
                    break;
                case BorderName.EntranceTileWest:
                    sprite = _blockSprites.EntranceTileWest();
                    break;
                case BorderName.EntranceTileEast:
                    sprite = _blockSprites.EntranceTileEast();
                    break;
                case BorderName.EntranceTileSouth:
                    sprite = _blockSprites.EntranceTileSouth();
                    break;

                // Locked doors
                case BorderName.LockedDoorTileNorth:
                    sprite = _blockSprites.LockedDoorTileNorth();
                    break;
                case BorderName.LockedDoorTileWest:
                    sprite = _blockSprites.LockedDoorTileWest();
                    break;
                case BorderName.LockedDoorTileEast:
                    sprite = _blockSprites.LockedDoorTileEast();
                    break;
                case BorderName.LockedDoorTileSouth:
                    sprite = _blockSprites.LockedDoorTileSouth();
                    break;

                // Doors
                case BorderName.DoorTileNorth:
                    sprite = _blockSprites.DoorTileNorth();
                    break;
                case BorderName.DoorTileWest:
                    sprite = _blockSprites.DoorTileWest();
                    break;
                case BorderName.DoorTileEast:
                    sprite = _blockSprites.DoorTileEast();
                    break;
                case BorderName.DoorTileSouth:
                    sprite = _blockSprites.DoorTileSouth();
                    break;

                // Holes in walls
                case BorderName.HoleInWallNorth:
                    sprite = _blockSprites.HoleInWallNorth();
                    break;
                case BorderName.HoleInWallWest:
                    sprite = _blockSprites.HoleInWallWest();
                    break;
                case BorderName.HoleInWallEast:
                    sprite = _blockSprites.HoleInWallEast();
                    break;
                case BorderName.HoleInWallSouth:
                    sprite = _blockSprites.HoleInWallSouth();
                    break;

                default:
                    sprite = _blockSprites.WallTileNorth();
                    break;
            }


            Border newBorder = new Border(name, borderType, offsetPosition, _tileSize, sprite);

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
    }
}
