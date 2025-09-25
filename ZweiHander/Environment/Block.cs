using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZweiHander.Graphics;

namespace ZweiHander.Environment
{
    public enum BlockType
    {
        Solid,
        Pushable,
        Breakable,
        Decorative
    }
    public class Block
    {
        private BlockType _blockType;
        private Point _gridPosition;
        private int _gridSize;
        private bool collision = true;

        public Block(BlockType blockType, Point gridPosition, int gridSize)
        {
            _blockType = blockType;
            _gridPosition = gridPosition;
            _gridSize = gridSize;

        }

        public Vector2 getVectorPosition()
        {
            return new Vector2(_gridPosition.X * _gridSize, _gridPosition.Y * _gridSize);
        }



        public Rectangle getHitbox()
        {
            return new Rectangle(_gridPosition.X * _gridSize, _gridPosition.Y * _gridSize, _gridSize, _gridSize);
        }

        public bool isCollidable()
        {
            if (_blockType == BlockType.Decorative) { collision = false; }
            return collision;
        }
        
    }
}
