using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Environment
{
    public class BlockFactory
    {
        private int _tileSize;
        private Dictionary<string, ISprite> _sprites; 
        public List<Rectangle> BlockMap { get; private set; }

        public BlockFactory(int tileSize, Dictionary<string, ISprite> sprites)
        {
            _tileSize = tileSize;
            _sprites = sprites;
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
                    sprite = _sprites["SolidBlock"];
                    break;

                case "PushableBlock":
                    blockType = BlockType.Pushable;
                    sprite = _sprites["PushableBlock"];
                    break;

                case "BreakableBlock":
                    blockType = BlockType.Breakable;
                    sprite = _sprites["BreakableBlock"];
                    break;

                case "DecorativeBlock":
                    blockType = BlockType.Decorative;
                    sprite = _sprites["DecorativeBlock"];
                    break;

                default:
                    blockType = BlockType.Solid;
                    sprite = _sprites["SolidBlock"];
                    break;
            }

            Block newBlock = new Block(blockType, gridPosition, _tileSize, sprite);

            // Store the hitbox of the block 
            BlockMap.Add(newBlock.getBlockHitbox());

            return newBlock;
        }
    }
}
