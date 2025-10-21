using Microsoft.Xna.Framework;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using System;

namespace ZweiHander.Enemy.EnemyStorage
{
    public class MovingBlock : IEnemy
    {
        protected ISprite _sprite;
        private readonly EnemySprites _enemySprites;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _moveTime;
        private float _currentTime;
        private bool _movingToEnd;
        private bool _isConfigured;

        public Vector2 Position { get; set; }
        public int Face { get; set; }
        public int Thrower { get; set; }

        public MovingBlock(EnemySprites enemySprites)
        {
            _enemySprites = enemySprites;
            _currentTime = 0;
            _movingToEnd = true;
            _isConfigured = false;

            Position = Vector2.Zero;
            Face = 0;
            Thrower = 0;

            _sprite = _enemySprites.Gel();
        }

        public MovingBlock(EnemySprites enemySprites, Vector2 startPos, Vector2 endPos, float moveTime)
        {
            _enemySprites = enemySprites;
            _startPosition = startPos;
            _endPosition = endPos;
            _moveTime = moveTime;
            _currentTime = 0;
            _movingToEnd = true;
            _isConfigured = true;

            Position = _startPosition;
            Face = 0;
            Thrower = 0;

            _sprite = _enemySprites.Gel();
        }

        public void SetMovement(Vector2 startPos, Vector2 endPos, float timeToMove)
        {
            _startPosition = startPos;
            _endPosition = endPos;
            _moveTime = timeToMove;
            _isConfigured = true;
            Position = _startPosition;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isConfigured)
                return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentTime += deltaTime;

            float progress = _currentTime / _moveTime;

            if (progress >= 1.0f)
            {
                _currentTime = 0;
                _movingToEnd = !_movingToEnd;
                progress = 0;
            }

            if (_movingToEnd)
            {
                Position = Vector2.Lerp(_startPosition, _endPosition, progress);
            }
            else
            {
                Position = Vector2.Lerp(_endPosition, _startPosition, progress);
            }

            _sprite.Update(gameTime);
        }

        public void Draw()
        {
            if (!_isConfigured)
                return;

            _sprite.Draw(Position);
        }
    }
}