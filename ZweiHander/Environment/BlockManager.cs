using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Environment
{
    /// <summary>
    /// Constructor initializes the factory with a tile size and block sprite storage
    ///</summary>
    public class BlockManager(int tileSize, BlockSprites blockSprites, PlayerSprites playerSprites)
    {
        private readonly BlockSprites _blockSprites = blockSprites ?? throw new ArgumentNullException(nameof(blockSprites)); // Reference to sprite storage for blocks
        //temp
        private readonly PlayerSprites _playerSprites = playerSprites ?? throw new ArgumentNullException(nameof(playerSprites));
        public List<Block> BlockMap { get; } = []; // Initialize map list

        /// <summary>
        /// Maps block names to their logic type (Solid, Pushable, Breakable, Decorative)
        /// </summary>
        private static readonly Dictionary<BlockName, BlockType> BlockNameToType = new()
        {
            { BlockName.SolidCyanTile, BlockType.Decorative },
            { BlockName.SolidBlackTile, BlockType.Solid },
            { BlockName.BlockTile, BlockType.Pushable },
            { BlockName.BrickTile, BlockType.Breakable },
            { BlockName.StatueTile1, BlockType.Decorative },
            { BlockName.StatueTile2, BlockType.Decorative },
            { BlockName.TexturedTile, BlockType.Decorative },
            { BlockName.StairTile, BlockType.Decorative },
            { BlockName.WhitePatternTile, BlockType.Decorative },
            { BlockName.FireTile, BlockType.Decorative },
            { BlockName.LadderTile, BlockType.Decorative },
            { BlockName.TunnelTile, BlockType.Decorative }
        };

        /// <summary>
        /// Creates a new block given its name and position on the grid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gridPosition"></param>
        /// <returns></returns>
        public Block CreateBlock(BlockName name, Point gridPosition)
        {
            // Lookup the BlockType from the dictionary
            BlockType blockType = BlockNameToType[name];
            ISprite sprite = name switch
            {
                BlockName.SolidCyanTile => _blockSprites.SolidCyanTile(),
                BlockName.BlockTile => _blockSprites.BlockTile(),
                BlockName.StatueTile1 => _blockSprites.StatueTile1(),
                BlockName.StatueTile2 => _blockSprites.StatueTile2(),
                BlockName.SolidBlackTile => _blockSprites.SolidBlackTile(),
                BlockName.TexturedTile => _blockSprites.TexturedTile(),
                BlockName.StairTile => _blockSprites.StairTile(),
                BlockName.BrickTile => _blockSprites.BrickTile(),
                BlockName.WhitePatternTile => _blockSprites.WhitePatternTile(),
                BlockName.FireTile => _playerSprites.Fire(),
                BlockName.LadderTile => _playerSprites.Ladder(),
                BlockName.TunnelTile => _blockSprites.TunnelTile(),
                _ => _blockSprites.SolidCyanTile(),
            };

            // Create the block with its type, position, size, and sprite
            Block newBlock = new(name, blockType, gridPosition, tileSize, sprite);

            // Store blocks in a map
            BlockMap.Add(newBlock);

            return newBlock; // Return the created block
        }
        public void Draw()
        {
            foreach (Block _block in BlockMap)
            {
                _block.Draw();
            }
        }

        public void Clear()
        {
            foreach (Block block in BlockMap)
            {
                block.UnsubscribeFromCollisions();
            }
            BlockMap.Clear();
        }
    }
}
