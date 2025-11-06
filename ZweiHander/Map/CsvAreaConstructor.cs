using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using ZweiHander.Items;
using ZweiHander.Enemy;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Environment
{
    /// <summary>
    /// Constructs areas (dungeons) from CSV files
    /// </summary>
    public class CsvAreaConstructor
    {
        private const int CELL_SIZE = 16;

        private readonly BlockFactory _blockFactory;
        private readonly ItemManager _itemManager;
        private readonly EnemyManager _enemyManager;

        private Room _currentRoom;
        private Area _currentArea;
        private IPlayer _player;

        public CsvAreaConstructor(BlockFactory blockFactory, ItemManager itemManager, EnemyManager enemyManager)
        {
            _blockFactory = blockFactory;
            _itemManager = itemManager;
            _enemyManager = enemyManager;
        }

        public Area LoadArea(string filePath, IPlayer player, string areaName = null)
        {
            _player = player;

            string[] lines = File.ReadAllLines(filePath);
            Area area = new Area(areaName);
            _currentArea = area;

            int lineIndex = 0;
            while (lineIndex < lines.Length)
            {
                string line = lines[lineIndex].Trim();

                if (line.StartsWith("room."))
                {
                    string[] parts = line.Split('.');
                    if (parts.Length >= 2 && int.TryParse(parts[1].Split(',')[0], out int roomNumber))
                    {
                        Room room = ParseRoom(lines, ref lineIndex, roomNumber);
                        area.AddRoom(roomNumber, room);
                    }
                }
                lineIndex++;
            }

            _currentArea = null;
            return area;
        }

        private Room ParseRoom(string[] lines, ref int lineIndex, int roomNumber)
        {
            lineIndex++;

            int roomStartLine = lineIndex;
            int roomEndLine = lineIndex;

            while (roomEndLine < lines.Length)
            {
                string line = lines[roomEndLine].Trim();
                if (line.StartsWith("end.room"))
                {
                    break;
                }
                roomEndLine++;
            }

            int roomHeight = roomEndLine - roomStartLine;
            int roomWidth = 0;

            for (int y = roomStartLine; y < roomEndLine; y++)
            {
                string[] cells = lines[y].Split(',');
                
                if (cells.Length > roomWidth)
                    roomWidth = cells.Length;
            }

            Vector2 roomSize = new Vector2((roomWidth - 1) * CELL_SIZE, roomHeight * CELL_SIZE);
            Room room = new Room(roomNumber, Vector2.Zero, roomSize);
            _currentRoom = room;

            for (int y = roomStartLine; y < roomEndLine; y++)
            {
                string[] cells = lines[y].Split(',');
                
                if (cells.Length > roomWidth)
                    roomWidth = cells.Length;

                for (int x = 1; x < cells.Length; x++)
                {
                    string cell = cells[x].Trim();
                    if (string.IsNullOrEmpty(cell)) continue;

                    int cellX = x - 1;
                    int cellY = y - roomStartLine;
                    Vector2 position = new Vector2(cellX * CELL_SIZE, cellY * CELL_SIZE);

                    string[] objects = cell.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    foreach (string obj in objects)
                    {
                        string objTrimmed = obj.Trim();
                        CreateObject(objTrimmed, position, new Point(cellX, cellY));
                    }
                }
            }

            lineIndex = roomEndLine;
            _currentRoom = null;

            return room;
        }

        private void CreateObject(string objectId, Vector2 position, Point gridPosition)
        {
            string[] parts = objectId.Split('.');
            string prefix = parts[0];
            string name = parts[1];

            switch (prefix)
            {
                case "b":
                    CreateBlock(name, gridPosition);
                    break;

                case "e":
                    CreateEnemy(name, position);
                    break;

                case "i":
                    CreateItem(name, position);
                    break;

                case "p":
                    CreatePortal(name, position);
                    break;
            }
        }

        private void CreateBlock(string blockId, Point gridPosition)
        {
            int id = int.Parse(blockId);
            BlockName blockName = AreaDictionaries.idToBlockName[id];
            Block block = _blockFactory.CreateBlock(blockName, gridPosition);
            _currentRoom.AddBlock(block);
        }

        private void CreateEnemy(string enemyName, Vector2 position)
        {
            if (AreaDictionaries.enemyNameToEnemyName.ContainsKey(enemyName))
            {
                IEnemy enemy = _enemyManager.GetEnemy(enemyName, position);
                _currentRoom.AddEnemy(enemy);
            }
        }

        private void CreateItem(string itemName, Vector2 position)
        {
            string cleanName = itemName.EndsWith("Item") ? itemName.Substring(0, itemName.Length - 4) : itemName;

            if (AreaDictionaries.itemNameToItemType.TryGetValue(cleanName, out ItemType itemType))
            {
                IItem item = _itemManager.GetItem(itemType, position: position);
                _currentRoom.AddItem(item);
            }
        }

        private void CreatePortal(string portalId, Vector2 position)
        {
            int id = int.Parse(portalId);
            RoomPortal portal = new RoomPortal(id, position, _currentRoom, _currentArea, _player);
            _currentRoom.AddPortal(portal);
            _currentArea.RegisterPortal(portal);
        }
    }
}
