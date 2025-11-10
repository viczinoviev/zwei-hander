using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Environment;
using ZweiHander.Enemy;
using ZweiHander.Items;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Map
{
    public class Room
    {
        public int RoomNumber { get; }
        public Vector2 Position { get; }
        public Vector2 Size { get; }
        public bool IsLoaded { get; set; }

        // Stored data for recreation
        private readonly List<(BlockName blockName, Point gridPosition)> _blockData;
        private readonly List<(string enemyName, Vector2 position)> _enemyData;
        private readonly List<(ItemType itemType, Vector2 position)> _itemData;
        private readonly List<(int portalId, Vector2 position)> _portalData;
        
        private readonly Universe _universe;
        private Rectangle Bounds;

        public Room(int roomNumber, Vector2 position, Vector2 size, Universe universe)
        {
            RoomNumber = roomNumber;
            Position = position;
            Size = size;
            Bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            
            _universe = universe;
            
            _blockData = new List<(BlockName, Point)>();
            _enemyData = new List<(string, Vector2)>();
            _itemData = new List<(ItemType, Vector2)>();
            _portalData = new List<(int, Vector2)>();
        }

        public void AddBlock(BlockName blockName, Point gridPosition)
        {
            _blockData.Add((blockName, gridPosition));
        }
        
        public void AddEnemy(string enemyName, Vector2 position)
        {
            _enemyData.Add((enemyName, position));
        }
        
        public void AddItem(ItemType itemType, Vector2 position)
        {
            _itemData.Add((itemType, position));
        }
        
        public void AddPortal(int portalId, Vector2 position)
        {
            _portalData.Add((portalId, position));
        }

        public IEnumerable<(int portalId, Vector2 position)> GetPortalData() => _portalData;

        public void Load()
        {
            IsLoaded = true;
            
            // Create fresh instances - collision handlers auto-register in their constructors
            foreach (var (blockName, gridPosition) in _blockData)
            {
                _universe.BlockFactory.CreateBlock(blockName, gridPosition);
            }
            
            foreach (var (enemyName, position) in _enemyData)
            {
                _universe.EnemyManager.GetEnemy(enemyName, position);
            }
            
            foreach (var (itemType, position) in _itemData)
            {
                _universe.ItemManager.GetItem(itemType, position: position);
            }
            
            // Create portals from data
            foreach (var (portalId, position) in _portalData)
            {
                RoomPortal portal = _universe.PortalManager.CreatePortal(portalId, position, this, _universe.CurrentArea);
                portal.OnRoomLoad();
            }
            
            // Debug: Print all colliders after room loads
            System.Console.WriteLine($"Room {RoomNumber} loaded:");
            CollisionManager.Instance.PrintAllColliders();
        }

        public bool Contains(Vector2 position) => Bounds.Contains(position);
    }
}
