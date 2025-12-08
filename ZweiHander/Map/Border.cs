using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Environment;
using ZweiHander.Graphics;
namespace ZweiHander.Map
{
    public class Border
    {
        private readonly Vector2 _position; // Upper-left corner position (covers 2x2 grid cells)
        private readonly int _tileSize; // 32 pixels
        private bool collision = true;

        private readonly ISprite _sprite;
        private readonly List<BlockCollisionHandler> _collisionHandlers;

        public BorderType BorderType { get; }
        public BorderName Name { get; }

        public Border(BorderName name, BorderType borderType, Vector2 position, int tileSize, ISprite sprite)
        {
            Name = name;
            BorderType = borderType;
            _position = position;
            _tileSize = tileSize;
            _sprite = sprite;
            _collisionHandlers = [];

            // Create collision handlers based on border type
            if (IsCollidable())
            {
                CreateCollisionHandlers();
            }
        }

        private void CreateCollisionHandlers()
        {
            int x = (int)_position.X;
            int y = (int)_position.Y;

            switch (BorderType)
            {
                case BorderType.Solid:
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y - 32, 64, 64)));
                    break;

                case BorderType.EntranceLeft:
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y - 32, 64, 16)));      // Top-left corner
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y + 16, 64, 16))); // Bottom-left corner
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x, y - 16, 32, 32))); // Right side
                    break;

                case BorderType.EntranceRight:
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y - 32, 64, 16)));
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y + 16, 64, 16)));
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y - 16, 32, 32)));
                    break;

                case BorderType.EntranceUp:
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y - 32, 16, 64)));
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x + 16, y - 32, 16, 64)));
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 16, y - 32, 32, 32)));
                    break;

                case BorderType.EntranceDown:
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 32, y - 32, 16, 64)));
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x + 16, y - 32, 16, 64)));
                    _collisionHandlers.Add(new BlockCollisionHandler(new Rectangle(x - 16, y, 32, 32)));
                    break;
            }
        }

        public void UnsubscribeFromCollisions()
        {
            foreach (var handler in _collisionHandlers)
            {
                CollisionManager.Instance.RemoveCollider(handler);
            }
        }

        // Returns the position for drawing (upper-left corner)
        public Vector2 GetPosition()
        {
            return _position;
        }

        public Rectangle GetHitBox()
        {
            return new Rectangle((int)_position.X - 32, (int)_position.Y - 32, 64, 64);
        }
        public void Draw()
        {
            _sprite.Draw(_position);
        }

        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
        }

        // Determines if the border should be collidable
        public bool IsCollidable()
        {
            // Decorative borders do not collide
            if (BorderType == BorderType.Decorative) { collision = false; }
            return collision;
        }
    }
}

