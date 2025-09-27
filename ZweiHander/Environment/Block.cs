using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZweiHander.Graphics;

namespace ZweiHander.Environment
{
    // Enum to define different types of blocks
    public enum BlockType
    {
        Solid,      // Blocks that are solid and cannot be moved
        Pushable,   // Blocks that can be pushed by the player
        Breakable,  // Blocks that can be broken/destroyed
        Decorative  // Non-collidable decorative blocks
    }

    public class Block
    {
        private BlockType _blockType;   // Type of the block
        private Point _gridPosition;    // Position in the grid (row, column)
        private int _gridSize;          // Size of the block in pixels
        private bool collision = true;  // Whether the block collides with other objects

        private ISprite _sprite;        // Sprite for the block

        // Constructor: creates a new block with given type, position, size, and sprite
        public Block(BlockType blockType, Point gridPosition, int gridSize, ISprite sprite)
        {
            _blockType = blockType;
            _gridPosition = gridPosition;
            _gridSize = gridSize;
            _sprite = sprite;
        }

        // Converts grid position into world position in pixels
        public Vector2 getVectorPosition()
        {
            return new Vector2(_gridPosition.X * _gridSize, _gridPosition.Y * _gridSize);
        }

        // Changes the type and sprite of the block at runtime
        // newType: the new block type
        // newSprite: the sprite to display for the new block type
        public void changeBlock(BlockType newType, ISprite newSprite)
        {
            _blockType = newType;
            _sprite = newSprite;
        }

        // Returns the hitbox rectangle for collision detection
        public Rectangle getBlockHitbox()
        {
            return new Rectangle(
                _gridPosition.X * _gridSize, 
                _gridPosition.Y * _gridSize, 
                _gridSize, 
                _gridSize
            );
        }

        // Determines if the block should be collidable
        public bool isCollidable()
        {
            // Decorative blocks do not collide
            if (_blockType == BlockType.Decorative) { collision = false; }
            return collision;
        }
    }
}
