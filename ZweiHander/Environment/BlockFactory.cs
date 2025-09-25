using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZweiHander.Environment
{
    public class BlockFactory
    {
        private int _tileSize;
        private BlockSprites _blockSprites;

        public BlockFactory(int tileSize, BlockSprites blockSprites)
        {
            _tileSize = tileSize;
            _blockSprites = blockSprites;
        }

        public Block CreateBlock(string type, Point gridPosition)
        {
            bool isSolid = true;
            string spriteKey = type;

            switch (type)
            {
                case "StoneBlock":
                    isSolid = true;
                    spriteKey = "StoneBlock";
                    break;
                case "PushableBlock":
                    isSolid = false; // pushable
                    spriteKey = "PushableBlock";
                    break;
                case "DecorativeBlock":
                    isSolid = false;
                    spriteKey = "DecorativeBlock";
                    break;
            }

            return new Block(gridPosition, spriteKey, isSolid, _tileSize);
        }
    }
}
