using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using ZweiHander.Items;
using ZweiHander.Enemy;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;
using System.Diagnostics;
using ZweiHander.Environment;
namespace ZweiHander.Map
{
    public class CsvAreaConstructor
    {
        private const int CELL_SIZE = 32;

        private Room _currentRoom;
        private Area _currentArea;
        private Universe _universe;
        private Camera.Camera _camera;

        public Area LoadArea(string filePath, Universe universe, Camera.Camera camera, string areaName = null)
        {
            _universe = universe;
            _camera = camera;

            string[] lines = File.ReadAllLines(filePath);
            Area area = new(areaName);
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
            int maxCellX = 0;

            for (int y = roomStartLine; y < roomEndLine; y++)
            {
                string[] cells = ParseCsvLine(lines[y]);
                for (int x = 1; x < cells.Length; x++)
                {
                    string cell = cells[x].Trim();
                    if (!string.IsNullOrEmpty(cell))
                    {
                        int cellX = x - 1;
                        if (cellX > maxCellX)
                            maxCellX = cellX;
                    }
                }
            }

            int roomWidth = maxCellX + 1;
            Vector2 roomSize = new(roomWidth * CELL_SIZE, (roomHeight-1) * CELL_SIZE);
            Room room = new(roomNumber, Vector2.Zero, roomSize, _universe);
            _currentRoom = room;

            for (int y = roomStartLine; y < roomEndLine; y++)
            {
                string[] cells = ParseCsvLine(lines[y]);

                for (int x = 1; x < cells.Length; x++)
                {
                    string cell = cells[x].Trim();
                    if (string.IsNullOrEmpty(cell)) continue;

                    int cellX = x-1;
                    int cellY = y - roomStartLine;
                    Vector2 position = new(cellX * CELL_SIZE, cellY * CELL_SIZE);

                    string[] objects = cell.Split([','], StringSplitOptions.RemoveEmptyEntries);
                    
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

        private static string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            int startIndex = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (line[i] == ',' && !inQuotes)
                {
                    result.Add(line[startIndex..i].Trim('"').Trim());
                    startIndex = i + 1;
                }
            }

            result.Add(line[startIndex..].Trim('"').Trim());
            return [.. result];
        }

        private void CreateObject(string objectId, Vector2 position, Point gridPosition)
        {
            string[] parts = objectId.Split('.');
            string prefix = parts[0];
            String name = "";
            if (parts.Length > 1)
            {
                name = parts[1];
            }

            switch (prefix)
            {
                case "b":
                    CreateBlock(name, gridPosition);
                    break;

                case "w":
                    CreateBorder(name, position);
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

                case "spawn":
                    _currentRoom.PlayerSpawnPoint = position;
                    break;
            }
        }

        private void CreateBlock(string blockId, Point gridPosition)
        {
            int id = int.Parse(blockId);
            BlockName blockName = AreaDictionaries.idToBlockName[id];
            _currentRoom.AddBlock(blockName, gridPosition);
        }

        private void CreateBorder(string borderTag, Vector2 position)
        {
            BorderName borderName = AreaDictionaries.tagToBorderName[borderTag];
            _currentRoom.AddBorder(borderName, position);
        }

        private void CreateEnemy(string enemyName, Vector2 position)
        {
            if (AreaDictionaries.enemyNameToEnemyName.ContainsKey(enemyName))
            {
                _currentRoom.AddEnemy(enemyName, position);
            }
        }

        private void CreateItem(string itemName, Vector2 position)
        {
            string cleanName = itemName.EndsWith("Item") ? itemName[..^4] : itemName;

            if (AreaDictionaries.itemNameToItemType.TryGetValue(cleanName, out string itemType))
            {
                _currentRoom.AddItem(itemType, position);
            } 
            else
            {
                Debug.WriteLine("WARNING: No item with name " +  cleanName);
            }
        }

        private void CreatePortal(string portalId, Vector2 position)
        {
            int id = int.Parse(portalId);
            Vector2 centeredPosition = new(position.X, position.Y);
            _currentRoom.AddPortal(id, centeredPosition);
            _currentArea.RegisterPortalData(id, _currentRoom.RoomNumber, centeredPosition);
        }
    }
}
