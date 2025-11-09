using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Commands;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class Area
    {
        private readonly Dictionary<int, Room> _rooms;
        private readonly Dictionary<int, List<IPortal>> _portalRegistry;

        public string Name { get; }

        public Area(string name)
        {
            Name = name;
            _rooms = new Dictionary<int, Room>();
            _portalRegistry = new Dictionary<int, List<IPortal>>();
        }

        public void AddRoom(int roomNumber, Room room) => _rooms[roomNumber] = room;

        public Room GetRoom(int roomNumber) => _rooms.TryGetValue(roomNumber, out Room room) ? room : null;

        public IEnumerable<Room> GetAllRooms() => _rooms.Values;

        public void RegisterPortal(IPortal portal)
        {
            if (!_portalRegistry.ContainsKey(portal.PortalId))
                _portalRegistry[portal.PortalId] = new List<IPortal>();
            
            _portalRegistry[portal.PortalId].Add(portal);
        }

        public IPortal FindConnectedPortal(RoomPortal sourcePortal)
        {
            if (!_portalRegistry.ContainsKey(sourcePortal.PortalId))
                return null;

            foreach (var portal in _portalRegistry[sourcePortal.PortalId])
            {
                if (portal != sourcePortal)
                    return portal;
            }
            return null;
        }
    }
}
