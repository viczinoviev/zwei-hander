using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.Commands;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class Area
    {
        private readonly Dictionary<int, Room> _rooms;
        // Store portal connection data: portalId -> list of (roomNumber, position)
        private readonly Dictionary<int, List<(int roomNumber, Vector2 position)>> _portalData;

        public string Name { get; }

        public Area(string name)
        {
            Name = name;
            _rooms = new Dictionary<int, Room>();
            _portalData = new Dictionary<int, List<(int, Vector2)>>();
        }

        public void AddRoom(int roomNumber, Room room) => _rooms[roomNumber] = room;

        public Room GetRoom(int roomNumber) => _rooms.TryGetValue(roomNumber, out Room room) ? room : null;

        public IEnumerable<Room> GetAllRooms() => _rooms.Values;

        public void RegisterPortalData(int portalId, int roomNumber, Vector2 position)
        {
            if (!_portalData.ContainsKey(portalId))
                _portalData[portalId] = new List<(int, Vector2)>();
            
            _portalData[portalId].Add((roomNumber, position));
        }

        public (int roomNumber, Vector2 position)? FindConnectedPortalData(int portalId, int sourceRoomNumber)
        {
            if (!_portalData.ContainsKey(portalId))
                return null;

            // Find the OTHER portal with this ID (not in the source room)
            var connectedPortal = _portalData[portalId].FirstOrDefault(p => p.roomNumber != sourceRoomNumber);
            
            if (connectedPortal != default)
                return connectedPortal;
            
            return null;
        }
    }
}
