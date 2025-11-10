using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;

namespace ZweiHander.Environment
{
    public class Border
    {
        private BorderType _borderType;
        private readonly Vector2 _position; // Upper-left corner position (covers 2x2 grid cells)
        private readonly int _tileSize; // 32 pixels
        private bool collision = true;

        private ISprite _sprite;
        private readonly BorderCollisionHandler _collisionHandler;

        public BorderType BorderType => _borderType;
        public BorderName Name { get; private set; }
        public BorderCollisionHandler CollisionHandler => _collisionHandler;
        public Border(BorderName name, BorderType borderType, Vector2 position, int tileSize, ISprite sprite)
        {

            Name = name;
            _borderType = borderType;
            _position = position;
            _tileSize = tileSize;
            _sprite = sprite;

            // Only create collision handler for collidable borders
            if (IsCollidable())
            {
                _collisionHandler = new BorderCollisionHandler(this);
            }
        }

        public void UnsubscribeFromCollisions()
        {
            if (_collisionHandler != null)
            {
                CollisionManager.Instance.RemoveCollider(_collisionHandler);
            }
        }

        // Returns the position for drawing (upper-left corner)
        public Vector2 GetPosition()
        {
            return _position;
        }

        public void Draw()
        {
            _sprite.Draw(_position);
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
        }

        // Returns the hitbox rectangle for collision detection (full 32x32)
        public Rectangle GetBorderHitbox()
        {
            return new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _tileSize,
                _tileSize
            );
        }

        // Determines if the border should be collidable
        public bool IsCollidable()
        {
            // Decorative borders do not collide
            if (_borderType == BorderType.Decorative) { collision = false; }
            return collision;
        }
    }
}
