using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class Universe
    {
        private readonly Dictionary<string, Area> _areas;

        public Area CurrentArea { get; private set; }
        public Room CurrentRoom { get; private set; }
        public IPlayer Player { get; private set; }

        public Universe()
        {
            _areas = new Dictionary<string, Area>();
        }

        public void AddArea(Area area) => _areas[area.Name] = area;

        public Area GetArea(string areaName) => _areas.TryGetValue(areaName, out Area area) ? area : null;

        public void SetPlayer(IPlayer player) => Player = player;

        public void SetCurrentLocation(string areaName, int roomNumber)
        {
            Area area = GetArea(areaName);
            Room room = area?.GetRoom(roomNumber);
            
            if (area == null || room == null) return;

            CurrentRoom?.Unload();
            CurrentArea = area;
            CurrentRoom = room;
            CurrentRoom.Load();
        }

        public void Update(GameTime gameTime) => CurrentRoom?.Update(gameTime);
    }
}
