using Microsoft.Xna.Framework;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy.EnemyStorage
{
    public class MovingBlock : AbstractEnemy
    {
        protected override int EnemyStartHealth => 1;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _moveTime;
        private float _currentTime;
        private bool _movingToEnd;
        private bool _isConfigured;
        public int Thrower { get; set; }
        //unsued enemy!
        public MovingBlock(EnemySprites enemySprites, Vector2 startPos, Vector2 endPos, float moveTime)
            : base(null, null, startPos)
        {
            _startPosition = startPos;
            _endPosition = endPos;
            _moveTime = moveTime;
            _currentTime = 0;
            _movingToEnd = true;
            _isConfigured = true;

            Position = _startPosition;
            Face = 0;
            Thrower = 0;

            Sprite = enemySprites.Gel();
        }

        public void SetMovement(Vector2 startPos, Vector2 endPos, float timeToMove)
        {
            _startPosition = startPos;
            _endPosition = endPos;
            _moveTime = timeToMove;
            _isConfigured = true;
            Position = _startPosition;
        }

        public override void Update(GameTime gameTime)
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

            Sprite.Update(gameTime);
        }
        public override void Draw()
        {
            if (!_isConfigured)
                return;

            Sprite.Draw(Position);
        }
    }
}