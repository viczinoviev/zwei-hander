using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Environment;
using ZweiHander.Enemy;
using ZweiHander.Items;
using ZweiHander.CollisionFiles;
using System;
using System.Linq;

namespace ZweiHander.Map
{
    public class Room(int roomNumber, Vector2 position, Vector2 size, Universe universe)
	{
		public int RoomNumber { get; } = roomNumber;
		public Vector2 Position { get; } = position;
		public Vector2 Size { get; } = size;
		public bool IsLoaded { get; set; }

        // Not required in a room, but allows for predetermined spawn location
        public Vector2 PlayerSpawnPoint = new(0,0);
        
        // Minimap data
        public Point MapPosition { get; set; } = new(-1, -1);
        public string MapConnections { get; set; } = "";

        // Stored data for recreation
        private readonly List<(BlockName blockName, Point gridPosition)> _blockData = [];
        private readonly List<(BorderName borderName, Vector2 position, Border borderPointer)> _borderData = [];
        private readonly List<(string enemyName, Vector2 position, IEnemy enemyPointer)> _enemyData = [];
        private readonly List<(string itemType, Vector2 position, IItem itemPointer)> _itemData = [];
        private readonly List<(int portalId, Vector2 position)> _portalData = [];
        private readonly List<(int portalId, Vector2 position)> _lockedEntranceData = [];

        private readonly Universe _universe = universe;
        private Rectangle Bounds = new((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

        public Boolean hasTriforce()
        {
            foreach (var (itemType, position, itemPointer) in _itemData)
            {
                if (itemType == "Triforce")
                {
                    return true;
                }
            }
            return false;
        }
		public void AddBlock(BlockName blockName, Point gridPosition)
        {
            _blockData.Add((blockName, gridPosition));
        }
        
        public void AddBorder(BorderName borderName, Vector2 position)
        {
            _borderData.Add((borderName, position, null));
        }
        
        public void AddEnemy(string enemyName, Vector2 position)
        {
            _enemyData.Add((enemyName, position, null));
        }
        
        public void AddItem(string itemType, Vector2 position)
        {
            _itemData.Add((itemType, position, null));
        }
        
        public void AddPortal(int portalId, Vector2 position)
        {
            _portalData.Add((portalId, position));
        }

        public void AddLockedEntrance(int portalId, Vector2 position)
        {
            _lockedEntranceData.Add((portalId, position));
        }

        public IEnumerable<(int portalId, Vector2 position)> GetPortalData() => _portalData;
        public IEnumerable<(int portalId, Vector2 position)> GetLockedEntranceData() => _lockedEntranceData;

        public Vector2 GetPlayerSpawnPoint()
        {
            return RoomSpawnHelper.GetPlayerSpawnPoint(
                PlayerSpawnPoint,
                _borderData,
                Bounds,
                _universe.TileSize,
                RoomNumber
            );
        }

        public void Load(bool excludePortals = false, bool excludeLockedEntrance = false, Vector2 offsetInTiles = default)
        {

            IsLoaded = true;
            
            // Create fresh instances
            foreach (var (blockName, gridPosition) in _blockData)
            {
                Point adjustedGridPosition = new(gridPosition.X + (int)offsetInTiles.X, gridPosition.Y + (int)offsetInTiles.Y);
                _universe.BlockFactory.CreateBlock(blockName, adjustedGridPosition);
            }
            
            for (int i = 0; i < _borderData.Count; i++)
            {
                var (borderName, position, borderPointer) = _borderData[i];
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                borderPointer = _universe.BorderFactory.CreateBorder(borderName, adjustedPosition);
                _borderData[i] = (borderName, position, borderPointer);
            }
            
            for (int i = 0; i < _enemyData.Count; i++)
            {
				var (enemyName, position, _) = _enemyData[i];
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
				IEnemy enemyPointer = _universe.EnemyManager.GetEnemy(enemyName, adjustedPosition);
				_enemyData[i] = (enemyName, position, enemyPointer);
            }
            
           for (int i = 0; i < _itemData.Count; i++)
            {
				var (itemType, position, itemPointer) = _itemData[i];
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                itemPointer = _universe.ItemManager.GetItem(itemType, -1, adjustedPosition);
                switch (itemType)
                {
                    case "Fairy":
                        itemPointer.Velocity = new(0, 10);
                        break;
                }
                _itemData[i] = (itemType, position, itemPointer);
            }
 
            if (excludePortals) return;
            foreach (var (portalId, position) in _portalData)
            {
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                _universe.PortalManager.CreatePortal(portalId, adjustedPosition, this, _universe.CurrentArea);
            }
            if (excludeLockedEntrance) return;
            foreach (var (portalId, position) in _lockedEntranceData)
            {
                Vector2 adjustedPosition = position + new Vector2(offsetInTiles.X * _universe.TileSize, offsetInTiles.Y * _universe.TileSize);
                _universe.LockedEntranceManager.CreateLockedEntrance(portalId, adjustedPosition, this, _universe.CurrentArea);
            }

        }

        public void PersistentRemoveItemAndEnemy(ItemManager itemManager, EnemyManager enemyManager, BorderManager borderFactory)
        {
            _itemData.RemoveAll(data => !itemManager.HasItem(data.itemPointer));
            _enemyData.RemoveAll(data => !enemyManager.HasThisEnemyInstance(data.enemyPointer));
            _borderData.RemoveAll(data => !borderFactory.HasBorder(data.borderPointer));
        }

        public bool Contains(Vector2 position) => Bounds.Contains(position);
    }
}
