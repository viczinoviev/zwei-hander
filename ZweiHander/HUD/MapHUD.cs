using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using ZweiHander.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ZweiHander.HUD
{
    /// <summary>
    /// Displays the minimap with room nodes and player position
    /// </summary>
    public class MapHUD : IHUDComponent
    {
        private readonly ISprite _mapDisplayHUD;
        private readonly Vector2 _position;
        private readonly HUDSprites _hudSprites;
        private Universe _universe;

        private readonly HashSet<int> _exploredRoomNumbers = new();
        private readonly HashSet<Point> _allRoomPositions = new();
        
        // Minimap layout constants
        private const int MAP_CELL_SIZE = 16; // 8px sprite * 2 scale
        private const int MAP_GRID_WIDTH = 8;
        private const int MAP_GRID_HEIGHT = 8;
        private readonly Vector2 MAP_OFFSET = new(8, 48); // Offset from HUD background position

        private readonly Vector2 MINIMAP_OFFSET = new(-216, 211);

        private readonly Vector2 mapIconOffset = new(-152, -24);
        private readonly Vector2 compassIconOffset = new(-154, 60);

        public bool mapItemGotten = false;
        public bool compassItemGotten = false;

        public MapHUD(HUDSprites hudSprites, Vector2 position)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _hudSprites = hudSprites;
            _mapDisplayHUD = hudSprites.MapDisplay();
            _position = position;
        }
        
        public void SetUniverse(Universe universe)
        {
            _universe = universe;
            foreach (var room in _universe?.CurrentArea.GetAllRooms())
            {
                _allRoomPositions.Add(room.MapPosition);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_universe?.CurrentRoom != null)
            {
                _exploredRoomNumbers.Add(_universe.CurrentRoom.RoomNumber);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            

            Vector2 basePos = _position + offset;
            _mapDisplayHUD.Draw(basePos);
            
            if (_universe?.CurrentArea == null) return;
            
            if (mapItemGotten)
            {
                DrawMinimap(basePos);

                ISprite mapIcon = _hudSprites.Map();
                Vector2 mapIconPos = basePos + mapIconOffset;
                mapIcon.Draw(mapIconPos);
            }

            if(compassItemGotten)
            {
                ISprite compassIcon = _hudSprites.Compass();
                Vector2 compassIconPos = basePos + compassIconOffset;
                compassIcon.Draw(compassIconPos);
            }

            foreach (var room in _universe.CurrentArea.GetAllRooms())
            {
                if (_exploredRoomNumbers.Contains(room.RoomNumber))
                {
                    DrawMapNode(basePos, room);
                }

                if (compassItemGotten)
                {
                    DrawMinimapTriforce(basePos, room);
                }

            }

            DrawMinimapPlayerPosition(basePos, _universe.CurrentRoom);
            
            if (_universe.CurrentRoom?.MapPosition.X >= 0)
            {
                DrawPlayerPosition(basePos, _universe.CurrentRoom);
            }
        }

        private void DrawMinimap(Vector2 basePos)
        {
            foreach (var position in _allRoomPositions)
            {
                int screenX = position.X;
                bool hasRoomAbove = _allRoomPositions.Contains(new Point(position.X, position.Y + 1));
                bool hasRoomBelow = _allRoomPositions.Contains(new Point(position.X, position.Y - 1));
                
                ISprite nodeSprite;
                int screenY;
                
                if (position.Y % 2 == 0)
                {
                    screenY = -(position.Y / 2);
                    
                    if (hasRoomAbove)
                    {
                        nodeSprite = _hudSprites.MinimapBoth();
                    }
                    else
                    {
                        nodeSprite = _hudSprites.MinimapLower();
                    }
                    
                    Vector2 nodePos = basePos + MINIMAP_OFFSET + new Vector2(
                        screenX * MAP_CELL_SIZE,
                        screenY * MAP_CELL_SIZE
                    );
                    nodeSprite.Draw(nodePos);
                }
                else
                {
                    if (!hasRoomBelow)
                    {
                        screenY = -(position.Y / 2);
                        nodeSprite = _hudSprites.MinimapUpper();
                        
                        Vector2 nodePos = basePos + MINIMAP_OFFSET + new Vector2(
                            screenX * MAP_CELL_SIZE,
                            screenY * MAP_CELL_SIZE
                        );
                        nodeSprite.Draw(nodePos);
                    }
                }
            }
        }
        
        private void DrawMapNode(Vector2 basePos, Room room)
        {
            // Convert minimap grid position to screen position
            // (0,0) is lower-left in grid
            int screenX = room.MapPosition.X;
            int screenY = -room.MapPosition.Y;
            
            Vector2 nodePos = basePos + MAP_OFFSET + new Vector2(
                screenX * MAP_CELL_SIZE,
                screenY * MAP_CELL_SIZE
            );
            
            ISprite nodeSprite = _hudSprites.MinimapNode(room.MapConnections);
            nodeSprite.Draw(nodePos);
        }
        
        private void DrawPlayerPosition(Vector2 basePos, Room currentRoom)
        {
            int screenX = currentRoom.MapPosition.X;
            int screenY = -currentRoom.MapPosition.Y;
            
            Vector2 playerPos = basePos + MAP_OFFSET + new Vector2(
                screenX * MAP_CELL_SIZE -1,
                screenY * MAP_CELL_SIZE + 2
            );
            
            ISprite playerSprite = _hudSprites.MapPlayer();
            playerSprite.Draw(playerPos);
        }
        
        private void DrawMinimapPlayerPosition(Vector2 basePos, Room currentRoom)
        {
            int screenX = currentRoom.MapPosition.X;
            int screenY = -(currentRoom.MapPosition.Y / 2);
            
            float yOffset = (currentRoom.MapPosition.Y % 2 != 0) ? -(MAP_CELL_SIZE / 2) : 0;
            
            Vector2 playerPos = basePos + MINIMAP_OFFSET + new Vector2(
                screenX * MAP_CELL_SIZE +1,
                screenY * MAP_CELL_SIZE + yOffset -1
            );
            
            ISprite playerSprite = _hudSprites.MinimapPlayer();
            playerSprite.Draw(playerPos);
        }

        private void DrawMinimapTriforce(Vector2 basePos, Room room)
        {
            if(room.hasTriforce() == false)
            {
                return;
            }

            int screenX = room.MapPosition.X;
            int screenY = -(room.MapPosition.Y / 2);
            
            float yOffset = (room.MapPosition.Y % 2 != 0) ? -(MAP_CELL_SIZE / 2) : 0;
            
            Vector2 triforcePos = basePos + MINIMAP_OFFSET + new Vector2(
                screenX * MAP_CELL_SIZE +1,
                screenY * MAP_CELL_SIZE + yOffset -1
            );
            
            ISprite triforceSprite = _hudSprites.MinimapTriforce();
            triforceSprite.Draw(triforcePos);
        }
    }
}
