using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;

namespace ZweiHander.Environment
{
    public class Block : IBlock
    {
        private BlockType _blockType;   // Type of the block
        private readonly Point _gridPosition;    // Position in the grid (row, column)
        private readonly int _gridSize;          // Size of the block in pixels
        private bool collision = true;  // Whether the block collides with other objects

        private ISprite _sprite;        // Sprite for the block
        private readonly BlockCollisionHandler _collisionHandler;

        // Constructor: creates a new block with given type, position, size, and sprite
        public Block(BlockType blockType, Point gridPosition, int gridSize, ISprite sprite)
        {
            if (sprite == null) throw new ArgumentNullException(nameof(sprite));

            _blockType = blockType;
            _gridPosition = gridPosition;
            _gridSize = gridSize;
            _sprite = sprite;
            
            // Only create collision handler for collidable blocks
            if (IsCollidable())
            {
                _collisionHandler = new BlockCollisionHandler(this);
            }
        }

        // Converts grid position into world position in pixels
        public Vector2 GetVectorPosition()
        {
            return new Vector2(_gridPosition.X * _gridSize, _gridPosition.Y * _gridSize);
        }

        // Changes the type and sprite of the block at runtime
        // newType: the new block type
        // newSprite: the sprite to display for the new block type
        public void ChangeBlock(BlockType newType, ISprite newSprite)
        {
            _blockType = newType;
            _sprite = newSprite ?? throw new ArgumentNullException(nameof(newSprite)); ;
            
        }

        public void Draw()
        {
            _sprite.Draw(this.GetVectorPosition());
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
        }
        // Returns the hitbox rectangle for collision detection
        public Rectangle GetBlockHitbox()
        {
            return new Rectangle(
                _gridPosition.X * _gridSize - _gridSize / 2, 
                _gridPosition.Y * _gridSize - _gridSize / 2, 
                _gridSize, 
                _gridSize
            );
        }

        // Determines if the block should be collidable
        public bool IsCollidable()
        {
            // Decorative blocks do not collide
            if (_blockType == BlockType.Decorative) { collision = false; }
            return collision;
        }


    }
}
