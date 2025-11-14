using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Environment;
using ZweiHander.Enemy;
using ZweiHander.Items;
using ZweiHander.CollisionFiles;
using System;

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
        private readonly List<(BorderName borderName, Vector2 position)> _borderData;
        private readonly List<(string enemyName, Vector2 position)> _enemyData;
        private readonly List<(string itemType, Vector2 position)> _itemData;
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
            _borderData = new List<(BorderName, Vector2)>();
            _enemyData = new List<(string, Vector2)>();
            _itemData = new List<(string, Vector2)>();
            _portalData = new List<(int, Vector2)>();
        }

        public void AddBlock(BlockName blockName, Point gridPosition)
        {
            _blockData.Add((blockName, gridPosition));
        }
        
        public void AddBorder(BorderName borderName, Vector2 position)
        {
            _borderData.Add((borderName, position));
        }
        
        public void AddEnemy(string enemyName, Vector2 position)
        {
            _enemyData.Add((enemyName, position));
        }
        
        public void AddItem(string itemType, Vector2 position)
        {
            _itemData.Add((itemType, position));
        }
        
        public void AddPortal(int portalId, Vector2 position)
        {
            _portalData.Add((portalId, position));
        }

        public IEnumerable<(int portalId, Vector2 position)> GetPortalData() => _portalData;

        public void Load(bool excludePortals = false, Vector2 offsetInTiles = default)
        {

            IsLoaded = true;
            
            // Create fresh instances - collision handlers auto-register in their constructors
            foreach (var (blockName, gridPosition) in _blockData)
            {
                Point adjustedGridPosition = new Point(gridPosition.X + (int)offsetInTiles.X, gridPosition.Y + (int)offsetInTiles.Y);
                _universe.BlockFactory.CreateBlock(blockName, adjustedGridPosition);
            }
            
            foreach (var (borderName, position) in _borderData)
            {
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                _universe.BorderFactory.CreateBorder(borderName, adjustedPosition);
            }
            
            foreach (var (enemyName, position) in _enemyData)
            {
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                _universe.EnemyManager.GetEnemy(enemyName, adjustedPosition);
            }
            
            foreach (var (itemType, position) in _itemData)
            {
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                _universe.ItemManager.GetItem(itemType, -1, adjustedPosition);
            }
            
            if (excludePortals) return;
            foreach (var (portalId, position) in _portalData)
            {
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                RoomPortal portal = _universe.PortalManager.CreatePortal(portalId, adjustedPosition, this, _universe.CurrentArea);
                portal.OnRoomLoad();
            }
            
        }

        public bool Contains(Vector2 position) => Bounds.Contains(position);
    }
}
