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
        }

        public override void UpdateCollisionBox()
        {

            
            CollisionBox = new Rectangle(
                (int)(_enemy.Position.X - COLLISION_SIZE / 2),
                (int)(_enemy.Position.Y - COLLISION_SIZE / 2),
                COLLISION_SIZE,
                COLLISION_SIZE
            );
        }
    }
}