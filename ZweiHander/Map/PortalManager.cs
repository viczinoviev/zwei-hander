using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class PortalManager(Universe universe, IPlayer player, Camera.Camera camera)
    {
        private readonly List<RoomPortal> _activePortals = [];
        private readonly Universe _universe = universe;
        private readonly IPlayer _player = player;
        private readonly Camera.Camera _camera = camera;

        public RoomPortal CreatePortal(int portalId, Vector2 position, Room parentRoom, Area parentArea)
        {
            RoomPortal portal = new(portalId, position, parentRoom, parentArea, _universe, _player, _camera);
            _activePortals.Add(portal);
            return portal;
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
