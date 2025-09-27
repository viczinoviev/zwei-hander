using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Environment
{
    public class BlockFactory
    {
        private int _tileSize;
        BlockSprites _blockSprites;
        public List<Rectangle> BlockMap { get; private set; }

        public BlockFactory(int tileSize, BlockSprites blockSprites)
        {
            _tileSize = tileSize;
            _blockSprites = blockSprites;
            BlockMap = new List<Rectangle>();
        }

        public Block CreateBlock(string name, Point gridPosition)
        {
            BlockType blockType;
            ISprite sprite;

            // Map type string to BlockType and sprite
            switch (name)
            {
                case "SolidBlock":
                    blockType = BlockType.Solid;
                    sprite = _blockSprites.SolidBlackTile();
                    break;

                //case "PushableBlock":
                //    blockType = BlockType.Pushable;
                //    sprite = _sprites["PushableBlock"];
                //    break;

                //case "BreakableBlock":
                //    blockType = BlockType.Breakable;
                //    sprite = _sprites["BreakableBlock"];
                //    break;

                //case "DecorativeBlock":
                //    blockType = BlockType.Decorative;
                //    sprite = _sprites["DecorativeBlock"];
                //    break;

                default:
                    blockType = BlockType.Solid;
                    sprite = _blockSprites.SolidBlackTile();
                    break;
            }

            Block newBlock = new Block(blockType, gridPosition, _tileSize, sprite);

            // Store the hitbox of the block 
            BlockMap.Add(newBlock.getBlockHitbox());

            return newBlock;
        }
    }
}
