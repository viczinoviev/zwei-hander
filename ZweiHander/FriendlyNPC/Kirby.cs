using Microsoft.Xna.Framework;
using System;
using ZweiHander.Enemy;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.FriendlyNPC
{
    public class Kirby : IKirby
    {
        private readonly IPlayer _player;
        private readonly ISprite _kirbySprite;
        EnemyManager _enemyManager;

        private readonly float _followDistance = 30f;
        private readonly float _fearDistance = 60f;
        private readonly float _speed = 80f;

        public Vector2 Position { get; set; }

        public Kirby(IPlayer player, EnemyManager enemyManager, KirbySprites kirbySprites, Vector2 startPosition)
        {
            _player = player;
            _enemyManager = enemyManager;
            _kirbySprite = kirbySprites.Kirby();
            _kirbySprite.Scale = new Vector2(1.25f, 1.25f);
            Position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 differenceVectorPlayer = _player.Position - Position;
            float distanceFromPlayer = differenceVectorPlayer.Length();
            float travelDistance = dt * _speed;

            Vector2 differenceVectorEnemy;
            float distanceFromEnemy;

            Vector2 moveVector = Vector2.Zero;

            if (_followDistance < distanceFromPlayer)
            {
                differenceVectorPlayer.Normalize();
                moveVector = differenceVectorPlayer * travelDistance;
            }

            foreach (IEnemy enemy in _enemyManager.currentEnemiesPub) 
            {
                differenceVectorEnemy = enemy.Position - Position;
                distanceFromEnemy = differenceVectorEnemy.Length();
                if (_fearDistance > distanceFromEnemy)
                {
                    differenceVectorEnemy.Normalize();
                    moveVector = -differenceVectorEnemy * travelDistance;
                }
             }      
            Position += moveVector;
            _kirbySprite.Update(gameTime);
        }

        public void Draw()
        {
            _kirbySprite.Draw(Position);
        }
    }
}
