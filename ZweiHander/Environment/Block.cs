using Microsoft.Xna.Framework;
using System;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;

namespace ZweiHander.Environment
{
    public class Block : IBlock
    {
        private BlockType _blockType;   // Type of the block
        private readonly Point _gridPosition;    // Position in the grid (row, column)
        private bool collision = true;  // Whether the block collides with other objects

        public ISprite _sprite;        // Sprite for the block

        public BlockType BlockType => _blockType;
        public int GridSize { get; }
        public BlockName Name { get; }
        public BlockCollisionHandler CollisionHandler { get; }


        // Constructor: creates a new block with given type, position, size, and sprite
        public Block(BlockName name, BlockType blockType, Point gridPosition, int gridSize, ISprite sprite)
        {
            Name = name;
            _blockType = blockType;
            _gridPosition = gridPosition;
            GridSize = gridSize;
            _sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));

            // Only create collision handler for collidable blocks
            if (IsCollidable())
            {
                CollisionHandler = new BlockCollisionHandler(this);
            }
        }

        public void UnsubscribeFromCollisions()
        {
            if (CollisionHandler != null)
            {
                CollisionManager.Instance.RemoveCollider(CollisionHandler);
            }
        }

        // Converts grid position into world position in pixels
        public Vector2 GetVectorPosition()
        {
            return new Vector2(_gridPosition.X * GridSize, _gridPosition.Y * GridSize);
        }

        // Changes the type and sprite of the block at runtime
        // newType: the new block type
        // newSprite: the sprite to display for the new block type
        public void ChangeBlock(BlockType newType, ISprite newSprite)
        {
            _blockType = newType;
            _sprite = newSprite ?? throw new ArgumentNullException(nameof(newSprite));

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
                (_gridPosition.X * GridSize) - (GridSize / 2),
                (_gridPosition.Y * GridSize) - (GridSize / 2),
                GridSize,
                GridSize
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
