using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZweiHander.Commands;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander.HUD
{
    public class MapTeleport
    {
        private MouseState _previousMouseState;
        private MouseState _currentMouseState;
        
        private readonly MapHUD _mapHUD;
        private readonly Universe _universe;
        private DebugRenderer _debugRenderer;
        
        public MapTeleport(MapHUD mapHUD, Universe universe)
        {
            _mapHUD = mapHUD;
            _universe = universe;
            _previousMouseState = Mouse.GetState();
            _currentMouseState = _previousMouseState;
        }
        
        public void SetDebugRenderer(DebugRenderer debugRenderer)
        {
            _debugRenderer = debugRenderer;
        }
        
        public void Update(Vector2 hudPosition, Vector2 hudOffset)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            
            if (_debugRenderer != null && _debugRenderer.ShowClickableRectangles)
            {
                RenderClickableAreasForDebug(hudPosition, hudOffset);
            }
            
            if (_currentMouseState.LeftButton == ButtonState.Pressed && 
                _previousMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 mousePosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);
                HandleMapClick(mousePosition, hudPosition, hudOffset);
            }
        }
        
        private void RenderClickableAreasForDebug(Vector2 hudPosition, Vector2 hudOffset)
        {
            _debugRenderer.ClearClickableRectangles();
            if (_universe?.CurrentArea == null) return;
            
            Vector2 basePos = hudPosition + hudOffset;
            int cellSize = _mapHUD.GetCellSize();
            int halfCell = cellSize / 2;
            
            foreach (var room in _universe.CurrentArea.GetAllRooms())
            {
                Vector2 nodePos = _mapHUD.GetMinimapNodeSpritePosition(room, basePos);
                int yPos = room.MapPosition.Y % 2 == 0 ? (int)nodePos.Y : (int)nodePos.Y - halfCell;
                _debugRenderer.AddClickableRectangle(new Rectangle((int)nodePos.X - halfCell, yPos, cellSize, halfCell));
                
                nodePos = _mapHUD.GetMapNodePosition(room, basePos);
                _debugRenderer.AddClickableRectangle(new Rectangle((int)nodePos.X - halfCell, (int)nodePos.Y - halfCell, cellSize, cellSize));
            }
        }
        
        private void HandleMapClick(Vector2 mousePosition, Vector2 hudPosition, Vector2 hudOffset)
        {
            if (_universe?.CurrentArea == null) return;
            
            Vector2 basePos = hudPosition + hudOffset;
            
            Room clickedRoom = FindMinimapNodeAtMousePosition(mousePosition, basePos);
            if (clickedRoom != null)
            {
                TeleportPlayerToRoom(clickedRoom);
                return;
            }
            
            Room clickedRoomOnMap = FindMainMapNodeAtMousePosition(mousePosition, basePos);
            if (clickedRoomOnMap != null)
            {
                TeleportPlayerToRoom(clickedRoomOnMap);
            }
        }
        
        private Room FindMinimapNodeAtMousePosition(Vector2 mousePosition, Vector2 basePos)
        {
            int cellSize = _mapHUD.GetCellSize();
            int halfCell = cellSize / 2;
            
            foreach (var room in _universe.CurrentArea.GetAllRooms())
            {
                Vector2 nodePos = _mapHUD.GetMinimapNodeSpritePosition(room, basePos);
                int yPos = room.MapPosition.Y % 2 == 0 ? (int)nodePos.Y : (int)nodePos.Y - halfCell;
                Rectangle nodeRect = new Rectangle((int)nodePos.X - halfCell, yPos, cellSize, halfCell);
                
                if (nodeRect.Contains(mousePosition)) return room;
            }
            return null;
        }
        
        private Room FindMainMapNodeAtMousePosition(Vector2 mousePosition, Vector2 basePos)
        {
            int cellSize = _mapHUD.GetCellSize();
            int halfCell = cellSize / 2;
            
            foreach (var room in _universe.CurrentArea.GetAllRooms())
            {
                Vector2 nodePos = _mapHUD.GetMapNodePosition(room, basePos);
                Rectangle nodeRect = new Rectangle((int)nodePos.X - halfCell, (int)nodePos.Y - halfCell, cellSize, cellSize);
                if (nodeRect.Contains(mousePosition)) return room;
            }
            return null;
        }
        
        private void TeleportPlayerToRoom(Room targetRoom)
        {
            new PlayerTeleportCommand(_universe.Player, targetRoom.GetPlayerSpawnPoint()).Execute();
            _universe.ForceChangeRoom(targetRoom.RoomNumber);
            new SetCameraCommand(_universe.Camera, _universe.Player).Execute();
        }
    }
}
