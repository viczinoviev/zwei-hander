using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZweiHander.Environment
{
    public static class CsvMapHandler
    {
        private static readonly Dictionary<int, BlockName> IdToBlockName = new()
        {
            { 0, BlockName.SolidCyanTile },
            { 1, BlockName.SolidBlackTile },
            { 2, BlockName.BlockTile },
            { 3, BlockName.BrickTile },
            { 4, BlockName.StatueTile1 },
            { 5, BlockName.StatueTile2 },
            { 6, BlockName.TexturedTile },
            { 7, BlockName.StairTile },
            { 8, BlockName.WhitePatternTile },
            { 9, BlockName.FireTile },
            { 10, BlockName.LadderTile }
        };

        /// <summary>
        /// Loads a CSV file into a map(blockList)
        /// </summary>
        public static List<Block> LoadMap(string filePath, BlockFactory factory)
        {
            var blocks = new List<Block>();

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Invalid address: {filePath}");

            string[] lines = File.ReadAllLines(filePath);

            for (int y = 0; y < lines.Length; y++)
            {
                string[] cells = lines[y].Split(',');

                for (int x = 0; x < cells.Length; x++)
                {
                    string cell = cells[x].Trim();
                    if (string.IsNullOrEmpty(cell)) continue;

                    if (!int.TryParse(cell, out int id) || !IdToBlockName.TryGetValue(id, out BlockName name))
                        throw new Exception($"Not a valid block ID '{cell}' at ({x},{y}) in CSV.");

                    Block block = factory.CreateBlock(name, new Point(x, y));
                    blocks.Add(block);
                }
            }

            return blocks;
        }

    }
}
