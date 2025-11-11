using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class PortalManager
    {
        private readonly List<RoomPortal> _activePortals;
        private readonly Universe _universe;
        private readonly IPlayer _player;
        private readonly Camera.Camera _camera;

        public PortalManager(Universe universe, IPlayer player, Camera.Camera camera)
        {
            _activePortals = new List<RoomPortal>();
            _universe = universe;
            _player = player;
            _camera = camera;
        }

        public RoomPortal CreatePortal(int portalId, Vector2 position, Room parentRoom, Area parentArea)
        {
            RoomPortal portal = new RoomPortal(portalId, position, parentRoom, parentArea, _universe, _player, _camera);
            _activePortals.Add(portal);
            return portal;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var portal in _activePortals)
            {
                portal.Update(gameTime);
            }
        }

        public void Clear()
        {
            foreach (var portal in _activePortals)
            {
                portal.OnRoomUnload();
            }
            _activePortals.Clear();
        }
    }
}
