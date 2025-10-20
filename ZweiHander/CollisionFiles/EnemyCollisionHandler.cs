using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Enemy;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;

namespace ZweiHander.CollisionFiles
{
    public class EnemyCollisionHandler : CollisionHandlerAbstract
    {
        private readonly IEnemy _enemy;
        private const int COLLISION_SIZE = 24;

        public EnemyCollisionHandler(IEnemy enemy)
        {
            _enemy = enemy;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            if (other is BlockCollisionHandler)
            {

                Vector2 newPosition = _enemy.Position + collisionInfo.ResolutionOffset;
                _enemy.Position = newPosition;

                UpdateCollisionBox();
            }
            if (other is ItemCollisionHandler)
            {
                //if enemy projectile, nothing happens
                //if player projectile, take damage
            }
            if (other is PlayerCollisionHandler)
            {
                //if colliding with sword, take damage
                //else damage player
            }
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = _enemy.GetCollisionBox();
        }
    }
}