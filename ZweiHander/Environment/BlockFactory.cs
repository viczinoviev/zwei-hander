using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Enemy;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Environment
{
    public class BlockFactory
    {
        private readonly int _tileSize; // Size of each block in pixels
        private readonly BlockSprites _blockSprites; // Reference to sprite storage for blocks
        //temp
        private readonly PlayerSprites _playerSprites;
        public List<Block> BlockMap { get; private set; } // Stores all blocks

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
        };
        /// <summary>
        /// Constructor initializes the factory with a tile size and block sprite storage
        ///</summary>
        public BlockFactory(int tileSize, BlockSprites blockSprites, PlayerSprites playerSprites)
        {
            _tileSize = tileSize; // Set tile size
            _blockSprites = blockSprites ?? throw new ArgumentNullException(nameof(blockSprites)); // Store sprite storage
            _playerSprites = playerSprites ?? throw new ArgumentNullException(nameof(playerSprites));
            BlockMap = new List<Block>(); // Initialize map list
        }

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
            ISprite sprite; // Will store the sprite for this block

            // Choose which sprite to use based on block name
            switch (name)
            {
                case BlockName.SolidCyanTile:
                    sprite = _blockSprites.SolidCyanTile();
                    break;

                case BlockName.BlockTile:
                    sprite = _blockSprites.BlockTile();
                    break;

                case BlockName.StatueTile1:
                    sprite = _blockSprites.StatueTile1();
                    break;

                case BlockName.StatueTile2:
                    sprite = _blockSprites.StatueTile2();
                    break;

                case BlockName.SolidBlackTile:
                    sprite = _blockSprites.SolidBlackTile();
                    break;

                case BlockName.TexturedTile:
                    sprite = _blockSprites.TexturedTile();
                    break;

                case BlockName.StairTile:
                    sprite = _blockSprites.StairTile();
                    break;

                case BlockName.BrickTile:
                    sprite = _blockSprites.BrickTile();
                    break;

                case BlockName.WhitePatternTile:
                    sprite = _blockSprites.WhitePatternTile();
                    break;

                case BlockName.FireTile:
                    sprite = _playerSprites.Fire();
                    break;

                case BlockName.LadderTile:
                    sprite = _playerSprites.Ladder();
                    break;

                default: 
                    sprite = _blockSprites.SolidCyanTile();
                    break;
            }

            // Create the block with its type, position, size, and sprite
            Block newBlock = new Block(name,blockType, gridPosition, _tileSize, sprite);

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
