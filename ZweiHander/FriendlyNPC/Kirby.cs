using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Reflection.Metadata;
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
        private readonly ISprite _runningLeftSprite;
        private readonly ISprite _runningRightSprite;
        private readonly ISprite _attackLeftSprite;
        private readonly ISprite _attackRightSprite;
        private ISprite _kirbySprite;
        private readonly EnemyManager _enemyManager;
        private IEnemy _closestEnemy = null;
        private readonly KirbySprites _sprites;

        private static readonly Random rnd = new();
        private float rndAngleX;
        private float rndAngleY;

        private readonly float _followDistance = 30f;
        private readonly float _fearDistance = 60f;
        private readonly float _attackAnimationDistance = 20f;
        private readonly float _speed = 80f;

        private bool isUlting = false;
        private int hitCount = 0;
        private readonly int maxHits = 4;
        private float ultTimer = 0f;
        public Vector2 Position { get; set; }

        public Kirby(IPlayer player, EnemyManager enemyManager, KirbySprites kirbySprites, Vector2 startPosition, ContentManager sfxPlayer)
        {
            _player = player;
            _enemyManager = enemyManager;
            _sprites = kirbySprites;
            //enemyHurt = sfxPlayer.Load<SoundEffect>("Audio/EnemyHurt");
            //currentSFX = enemyHurt.CreateInstance();

            _runningLeftSprite = _sprites.KirbyRunningLeft();
            _runningRightSprite = _sprites.KirbyRunningRight();
            _attackLeftSprite = _sprites.KirbyAttackLeft();
            _attackRightSprite = _sprites.KirbyAttackRight();
            _runningLeftSprite.Scale = new Vector2(1f, 1f);
            _runningRightSprite.Scale = new Vector2(1f, 1f);
            _attackLeftSprite.Scale = new Vector2(1f, 1f);
            _attackRightSprite.Scale = new Vector2(1f, 1f);

            _kirbySprite = _runningLeftSprite;


            Position = startPosition;

        }
        public void StartUlt()
        {
            if (!isUlting)
            {
                isUlting = true;
                hitCount = 0;
                ultTimer = 0f;
            }
        }
        public void Update(GameTime gameTime)
        {
            if (!isUlting)
            {
                UpdateDefault(gameTime);
            }
            else
            {
                UpdateKirbyUlt(gameTime);
            }
            _kirbySprite.Update(gameTime);
        }

        public void UpdateKirbyUlt(GameTime gameTime)
        {
            FindClosestEnemy();
            if (hitCount >= maxHits||_closestEnemy==null)
            {
                isUlting = false;
                return;
            }

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ultTimer += dt;

            if(ultTimer > (hitCount+1)*0.5f)
            {
                hitCount++;
                //enemyHurt.Play();
                _closestEnemy.TakeDamage(2);
                Vector2 centerOfUlt = _closestEnemy.Position;
                rndAngleX = (float)((rnd.NextDouble() * 2.0) - 1.0);
                rndAngleY = (float)((rnd.NextDouble() * 1.5) - 0.5);
                Vector2 rndDirection = new(rndAngleX, rndAngleY);

                if (rndAngleX < 0) _kirbySprite = _attackRightSprite;
                else _kirbySprite = _attackLeftSprite;

                Position = centerOfUlt + (rndDirection * _attackAnimationDistance);
            }
        }

        //Sets _closestEnemy to the closest enemy
        public void FindClosestEnemy()
        {
            if (_enemyManager.CurrentEnemiesPub.Count != 0)
            {
                float distance;
                IEnemy _enemy = _enemyManager.CurrentEnemiesPub[0];
                float leastDistance = (_enemy.Position - Position).Length();
                _closestEnemy = _enemy;
                for (int x = 1; x < _enemyManager.CurrentEnemiesPub.Count; x++)
                {
                    _enemy = _enemyManager.CurrentEnemiesPub[x];
                    distance = (_enemy.Position - Position).Length();
                    if (distance < leastDistance)
                    {
                        leastDistance = distance;
                        _closestEnemy = _enemy;
                    }
                }
            }
            else
            {
                _closestEnemy = null;
            }
        }
        public void UpdateDefault(GameTime gameTime)
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

            foreach (IEnemy enemy in _enemyManager.CurrentEnemiesPub)
            {
                differenceVectorEnemy = enemy.Position - Position;
                distanceFromEnemy = differenceVectorEnemy.Length();
                if (_fearDistance > distanceFromEnemy)
                {
                    differenceVectorEnemy.Normalize();
                    moveVector = -differenceVectorEnemy * travelDistance;

                }
            }
            if (moveVector.X < 1)
            {
                _kirbySprite = _runningLeftSprite;
            }
            else
            {
                _kirbySprite = _runningRightSprite;
            }
            Position += moveVector;
        }

        public void Draw()
        {
            _kirbySprite.Draw(Position);
        }
    }
}
