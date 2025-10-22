using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using ZweiHander.Environment;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Map
{
    public class BorderManager
    {
        private readonly BlockSprites _blockSprites;
        public List<Border> Borders { get; private set; } // Stores all borders
        public BorderManager(BlockSprites blockSprites) 
        {
            _blockSprites = blockSprites;
            Borders = new List<Border>();
        }

        public Border CreateBorder(WallName name)
        {
            ISprite sprite;
            Vector2 position = new Vector2(0, 0);
            switch (name)
            {
                // Wall corners
                case WallName.WallNorthLeft:
                    sprite = _blockSprites.WallNorthLeft();         
                    position = new Vector2(159, 175);
                    break;  
                case WallName.WallNorthRight:
                    sprite = _blockSprites.WallNorthRight();
                    position = new Vector2(383, 175);
                    break;
                case WallName.WallSouthLeft:
                    sprite = _blockSprites.WallSouthLeft();
                    position = new Vector2(159, 463);
                    break;
                case WallName.WallSouthRight:
                    sprite = _blockSprites.WallSouthRight();
                    position = new Vector2(383, 463);
                    break;
                case WallName.WallWestTop:
                    sprite = _blockSprites.WallWestTop();
                    position = new Vector2(47, 215);
                    break;
                case WallName.WallWestBottom:
                    sprite = _blockSprites.WallWestBottom();
                    position = new Vector2(47, 423);
                    break;
                case WallName.WallEastTop:
                    sprite = _blockSprites.WallEastTop();
                    position = new Vector2(495, 215);
                    break;
                case WallName.WallEastBottom:
                    sprite = _blockSprites.WallEastBottom();
                    position = new Vector2(495, 423);
                    break;

                // Wall tile (center) variations
                case WallName.WallTileNorth:
                    sprite = _blockSprites.WallTileNorth();
                    position = new Vector2(271, 175);
                    break;
                case WallName.WallTileWest:
                    sprite = _blockSprites.WallTileWest();
                    position = new Vector2(47, 319);
                    break;
                case WallName.WallTileEast:
                    sprite = _blockSprites.WallTileEast();
                    break;
                case WallName.WallTileSouth:
                    sprite = _blockSprites.WallTileSouth();
                    break;

                // Entrance tiles
                case WallName.EntranceTileNorth:    
                    sprite = _blockSprites.EntranceTileNorth();
                    position = new Vector2(271, 175);
                    break;
                case WallName.EntranceTileWest:
                    sprite = _blockSprites.EntranceTileWest();
                    position = new Vector2(47, 319);
                    break;
                case WallName.EntranceTileEast:
                    sprite = _blockSprites.EntranceTileEast();
                    break;
                case WallName.EntranceTileSouth:
                    sprite = _blockSprites.EntranceTileSouth();
                    break;

                // Locked door tiles
                case WallName.LockedDoorTileNorth:
                    sprite = _blockSprites.LockedDoorTileNorth();
                    position = new Vector2(271, 175);
                    break;
                case WallName.LockedDoorTileWest:
                    sprite = _blockSprites.LockedDoorTileWest();
                    position = new Vector2(47, 319);
                    break;
                case WallName.LockedDoorTileEast:
                    sprite = _blockSprites.LockedDoorTileEast();
                    position = new Vector2(495, 319);
                    break;
                case WallName.LockedDoorTileSouth:
                    sprite = _blockSprites.LockedDoorTileSouth();
                    position = new Vector2(271, 463);
                    break;

                // Door tiles
                case WallName.DoorTileNorth:
                    sprite = _blockSprites.DoorTileNorth();
                    position = new Vector2(271, 175);
                    break;
                case WallName.DoorTileWest:
                    sprite = _blockSprites.DoorTileWest();
                    position = new Vector2(47, 319);
                    break;
                case WallName.DoorTileEast:
                    sprite = _blockSprites.DoorTileEast();
                    break;
                case WallName.DoorTileSouth:
                    sprite = _blockSprites.DoorTileSouth();
                    break;

                // Hole-in-wall tiles
                case WallName.HoleInWallNorth:
                    sprite = _blockSprites.HoleInWallNorth();
                    position = new Vector2(271, 175);
                    break;
                case WallName.HoleInWallWest:
                    sprite = _blockSprites.HoleInWallWest();
                    position = new Vector2(47, 319);
                    break;
                case WallName.HoleInWallEast:
                    sprite = _blockSprites.HoleInWallEast();
                    break;
                case WallName.HoleInWallSouth:
                    sprite = _blockSprites.HoleInWallSouth();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(name), $"Unhandled wall name: {name}");
            }
            Border newBorder = new Border(name, WallType.Solid, position, sprite);
            Borders.Add(newBorder);

            return newBorder;
        }
        public void Draw()
        {
            foreach (Border _border in Borders)
            {
                _border.Draw();
            }
        }

    }

}
