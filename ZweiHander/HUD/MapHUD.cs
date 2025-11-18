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
        
        // Minimap layout constants
        private const int MINIMAP_CELL_SIZE = 16; // 8px sprite * 2 scale
        private const int MINIMAP_GRID_WIDTH = 8;
        private const int MINIMAP_GRID_HEIGHT = 4;
        private readonly Vector2 MINIMAP_OFFSET = new(8, 0); // Offset from HUD background position

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
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Vector2 basePos = _position + offset;
            _mapDisplayHUD.Draw(basePos);
            
            if (_universe?.CurrentArea == null) return;
            
            foreach (var room in _universe.CurrentArea.GetAllRooms())
            {
                if (room.MinimapPosition.X >= 0 && room.MinimapPosition.Y >= 0)
                {
                    DrawMinimapNode(basePos, room);
                }
            }
            
            if (_universe.CurrentRoom?.MinimapPosition.X >= 0)
            {
                DrawPlayerPosition(basePos, _universe.CurrentRoom);
            }
        }
        
        private void DrawMinimapNode(Vector2 basePos, Room room)
        {
            // Convert minimap grid position to screen position
            // (0,0) is lower-left in grid
            int screenX = room.MinimapPosition.X;
            int screenY = (MINIMAP_GRID_HEIGHT - 1) - room.MinimapPosition.Y;
            
            Vector2 nodePos = basePos + MINIMAP_OFFSET + new Vector2(
                screenX * MINIMAP_CELL_SIZE,
                screenY * MINIMAP_CELL_SIZE
            );
            
            ISprite nodeSprite = _hudSprites.MinimapNode(room.MinimapConnections);
            nodeSprite.Draw(nodePos);
        }
        
        private void DrawPlayerPosition(Vector2 basePos, Room currentRoom)
        {
            int screenX = currentRoom.MinimapPosition.X;
            int screenY = (MINIMAP_GRID_HEIGHT - 1) - currentRoom.MinimapPosition.Y;
            
            Vector2 playerPos = basePos + MINIMAP_OFFSET + new Vector2(
                screenX * MINIMAP_CELL_SIZE,
                screenY * MINIMAP_CELL_SIZE + 2
            );
            
            ISprite playerSprite = _hudSprites.MinimapPlayer();
            playerSprite.Draw(playerPos);
        }
    }
}
