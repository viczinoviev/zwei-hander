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
        private Vector2 _borderPosition;
        public List<Border> Borders { get; private set; } // Stores all borders
        public BorderManager(BlockSprites blockSprites, Vector2 borderPosition) 
        {
            _blockSprites = blockSprites;
            _borderPosition = borderPosition;
            Borders = new List<Border>();
        }

        public Border CreateWall(WallName name)
        {
            ISprite sprite;
            Vector2 wallPosition = new Vector2(0, 0);
            int x = (int)_borderPosition.X;
            int y = (int)_borderPosition.Y;
            switch (name)
            {
                // Wall corners
                case WallName.WallNorthLeft:
                    sprite = _blockSprites.WallNorthLeft();
                    wallPosition = new Vector2(x+112, y);//159 175
                    break;  
                case WallName.WallNorthRight:
                    sprite = _blockSprites.WallNorthRight();
                    wallPosition = new Vector2(x+336, y);//383 175
                    break;
                case WallName.WallSouthLeft:
                    sprite = _blockSprites.WallSouthLeft();
                    wallPosition = new Vector2(x+112, y+288);//159 463
                    break;
                case WallName.WallSouthRight:
                    sprite = _blockSprites.WallSouthRight();
                    wallPosition = new Vector2(x+336, y+288);//383 463
                    break;
                case WallName.WallWestTop:
                    sprite = _blockSprites.WallWestTop();
                    wallPosition = new Vector2(x, y+40);//47 215
                    break;
                case WallName.WallWestBottom:
                    sprite = _blockSprites.WallWestBottom();
                    wallPosition = new Vector2(x, y+248);//47 423
                    break;
                case WallName.WallEastTop:
                    sprite = _blockSprites.WallEastTop();
                    wallPosition = new Vector2(x+448, y+40);//495 215
                    break;
                case WallName.WallEastBottom:
                    sprite = _blockSprites.WallEastBottom();
                    wallPosition = new Vector2(x+448, y+248);//495 423
                    break;

                // Wall tile (center) variations
                case WallName.WallTileNorth:
                    sprite = _blockSprites.WallTileNorth();
                    wallPosition = new Vector2(x+224, y);//271 175
                    break;
                case WallName.WallTileWest:
                    sprite = _blockSprites.WallTileWest();
                    wallPosition = new Vector2(x, y+144);//47 319
                    break;
                case WallName.WallTileEast:
                    sprite = _blockSprites.WallTileEast();
                    wallPosition = new Vector2(x+448, y+144);//495 319
                    break;
                case WallName.WallTileSouth:
                    sprite = _blockSprites.WallTileSouth();
                    wallPosition = new Vector2(x+224, y+288);//271 463
                    break;

                // Entrance tiles
                case WallName.EntranceTileNorth:    
                    sprite = _blockSprites.EntranceTileNorth();
                    wallPosition = new Vector2(x+224, y);//271 175
                    break;
                case WallName.EntranceTileWest:
                    sprite = _blockSprites.EntranceTileWest();
                    wallPosition = new Vector2(x, y+144);//47 319
                    break;
                case WallName.EntranceTileEast:
                    sprite = _blockSprites.EntranceTileEast();
                    wallPosition = new Vector2(x+448, y+144);//495 319
                    break;
                case WallName.EntranceTileSouth:
                    sprite = _blockSprites.EntranceTileSouth();
                    wallPosition = new Vector2(x+224, y+288);//271 463
                    break;

                // Locked door tiles
                case WallName.LockedDoorTileNorth:
                    sprite = _blockSprites.LockedDoorTileNorth();
                    wallPosition = new Vector2(x+224, y);//271 175
                    break;
                case WallName.LockedDoorTileWest:
                    sprite = _blockSprites.LockedDoorTileWest();
                    wallPosition = new Vector2(x, y + 144);//47 319
                    break;
                case WallName.LockedDoorTileEast:
                    sprite = _blockSprites.LockedDoorTileEast();
                    wallPosition = new Vector2(x + 448, y + 144);//495 319
                    break;
                case WallName.LockedDoorTileSouth:
                    sprite = _blockSprites.LockedDoorTileSouth();
                    wallPosition = new Vector2(x + 224, y + 288);//271 463
                    break;

                // Door tiles
                case WallName.DoorTileNorth:
                    sprite = _blockSprites.DoorTileNorth();
                    wallPosition = new Vector2(271, 175);//271 175
                    break;
                case WallName.DoorTileWest:
                    sprite = _blockSprites.DoorTileWest();
                    wallPosition = new Vector2(x, y + 144);//47 319
                    break;
                case WallName.DoorTileEast:
                    sprite = _blockSprites.DoorTileEast();
                    wallPosition = new Vector2(x + 448, y + 144);//495 319
                    break;
                case WallName.DoorTileSouth:
                    sprite = _blockSprites.DoorTileSouth();
                    wallPosition = new Vector2(x + 224, y + 288);//271 463
                    break;

                // Hole-in-wall tiles
                case WallName.HoleInWallNorth:
                    sprite = _blockSprites.HoleInWallNorth();
                    wallPosition = new Vector2(x + 224, y);//271 175
                    break;
                case WallName.HoleInWallWest:
                    sprite = _blockSprites.HoleInWallWest();
                    wallPosition = new Vector2(x, y + 144);//47 319
                    break;
                case WallName.HoleInWallEast:
                    sprite = _blockSprites.HoleInWallEast();
                    wallPosition = new Vector2(x + 448, y + 144);//495 319
                    break;
                case WallName.HoleInWallSouth:
                    sprite = _blockSprites.HoleInWallSouth();
                    wallPosition = new Vector2(x + 224, y + 288);//271 463
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(name), $"Unhandled wall name: {name}");
            }
            Border newWall = new Border(name, WallType.Solid, wallPosition, sprite);
            Borders.Add(newWall);

            return newWall;
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
