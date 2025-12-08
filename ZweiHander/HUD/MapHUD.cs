using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Map;

namespace ZweiHander.HUD
{
    public class MapHUD : IHUDComponent
    {
        private readonly ISprite _mapDisplayHUD;
        private readonly Vector2 _position;
        private readonly HUDSprites _hudSprites;
        private Universe _universe;

        private readonly HashSet<int> _exploredRoomNumbers = [];
        private readonly HashSet<Point> _allRoomPositions = [];

        private MapTeleport _mapTeleport;
        private Vector2 _lastDrawOffset = Vector2.Zero;

        // Minimap layout constants
        private const int MAP_CELL_SIZE = 16;
        private readonly Vector2 MAP_OFFSET = new(8, 48);

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
            _mapTeleport = new MapTeleport(this, _universe, _universe.Camera);
        }

        public void SetDebugRenderer(DebugRenderer debugRenderer)
        {
            _mapTeleport?.SetDebugRenderer(debugRenderer);
        }


        public void Update(GameTime gameTime)
        {
            if (_universe?.CurrentRoom != null)
            {
                _exploredRoomNumbers.Add(_universe.CurrentRoom.RoomNumber);
            }

            _mapTeleport?.Update(_position, _lastDrawOffset);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            _lastDrawOffset = offset;
            Vector2 basePos = _position + offset;
            _mapDisplayHUD.Draw(basePos);

            if (_universe?.CurrentArea == null) return;

            if (mapItemGotten)
            {
                DrawMinimap(basePos);
                DrawIcon(_hudSprites.Map(), basePos + mapIconOffset);
            }

            if (compassItemGotten)
            {
                DrawIcon(_hudSprites.Compass(), basePos + compassIconOffset);
            }

            foreach (var room in _universe.CurrentArea.GetAllRooms())
            {
                if (_exploredRoomNumbers.Contains(room.RoomNumber) || mapItemGotten)
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

        private static void DrawIcon(ISprite icon, Vector2 position)
        {
            icon.Draw(position);
        }

        private void DrawMinimap(Vector2 basePos)
        {
            foreach (var position in _allRoomPositions)
            {
                ISprite nodeSprite;
                if (position.Y % 2 == 0)
                {
                    bool hasRoomAbove = _allRoomPositions.Contains(new Point(position.X, position.Y + 1));
                    nodeSprite = hasRoomAbove ? _hudSprites.MinimapBoth() : _hudSprites.MinimapLower();
                }
                else
                {
                    bool hasRoomBelow = _allRoomPositions.Contains(new Point(position.X, position.Y - 1));
                    if (hasRoomBelow) continue;
                    nodeSprite = _hudSprites.MinimapUpper();
                }

                Vector2 nodePos = basePos + MINIMAP_OFFSET + new Vector2(position.X * MAP_CELL_SIZE, -(position.Y / 2) * MAP_CELL_SIZE);
                nodeSprite.Draw(nodePos);
            }
        }

        private void DrawMapNode(Vector2 basePos, Room room)
        {
            Vector2 nodePos = GetMapNodePosition(room, basePos);
            ISprite nodeSprite = _hudSprites.MinimapNode(room.MapConnections);
            nodeSprite.Draw(nodePos);
        }

        private void DrawPlayerPosition(Vector2 basePos, Room currentRoom)
        {
            _hudSprites.MapPlayer().Draw(GetMapNodePosition(currentRoom, basePos) + new Vector2(-1, 2));
        }

        private void DrawMinimapPlayerPosition(Vector2 basePos, Room currentRoom)
        {
            _hudSprites.MinimapPlayer().Draw(GetMinimapNodePosition(currentRoom, basePos) + new Vector2(1, -1));
        }

        private void DrawMinimapTriforce(Vector2 basePos, Room room)
        {
            if (room.HasTriforce())
            {
                _hudSprites.MinimapTriforce().Draw(GetMinimapNodePosition(room, basePos) + new Vector2(1, -1));
            }
        }

        public Vector2 GetMinimapNodePosition(Room room, Vector2 basePos)
        {
            int screenX = room.MapPosition.X;
            int screenY = -(room.MapPosition.Y / 2);
            float yOffset = (room.MapPosition.Y % 2 != 0) ? -(MAP_CELL_SIZE / 2) : 0;
            return basePos + MINIMAP_OFFSET + new Vector2(screenX * MAP_CELL_SIZE, (screenY * MAP_CELL_SIZE) + yOffset);
        }

        public Vector2 GetMinimapNodeSpritePosition(Room room, Vector2 basePos)
        {
            return basePos + MINIMAP_OFFSET + new Vector2(room.MapPosition.X * MAP_CELL_SIZE, -(room.MapPosition.Y / 2) * MAP_CELL_SIZE);
        }

        public Vector2 GetMapNodePosition(Room room, Vector2 basePos)
        {
            return basePos + MAP_OFFSET + new Vector2(room.MapPosition.X * MAP_CELL_SIZE, -room.MapPosition.Y * MAP_CELL_SIZE);
        }

        public static int GetCellSize()
        {
            return MAP_CELL_SIZE;
        }
    }
}
