using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class LockedEntranceManager(Universe universe, IPlayer player, Camera.Camera camera)
    {
        private readonly List<RoomLockedEntrance> _activeLockedEntrance = [];
        private readonly Universe _universe = universe;
        private readonly IPlayer _player = player;
        private readonly Camera.Camera _camera = camera;

        public RoomLockedEntrance CreateLockedEntrance(int portalId, Vector2 position, Room parentRoom, Area parentArea)
        {
            RoomLockedEntrance portal = new(portalId, position, parentRoom, parentArea, _universe, _player, _camera);
            _activeLockedEntrance.Add(portal);
            return portal;
        }

        public void Clear()
        {
            foreach (var portal in _activeLockedEntrance)
            {
                portal.OnRoomUnload();
            }
            _activeLockedEntrance.Clear();
        }
    }
}
