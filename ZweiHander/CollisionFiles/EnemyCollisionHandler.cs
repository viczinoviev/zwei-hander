using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Enemy;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;
using ZweiHander.Commands;

namespace ZweiHander.CollisionFiles
{
    public class EnemyCollisionHandler : CollisionHandlerAbstract
    {
        public readonly IEnemy _enemy;
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
                if(((ItemCollisionHandler)other).Item.HasProperty(ItemProperty.CanDamageEnemy)){
                    _enemy.Hitpoints -= 5;
                    if (_enemy.Hitpoints <= 0)
                    {
                        //this.Unsubscribe();
                    }
                }
            }
            if (other is PlayerCollisionHandler)
            {
                if (((PlayerCollisionHandler)other)._player.CurrentState == PlayerState.Attacking)
                {
                    _enemy.Hitpoints -= 5;
                    if (_enemy.Hitpoints <= 0)
                    {
                        //this.Unsubscribe();
                    }
                } 
                
            }
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = _enemy.GetCollisionBox();
        }
    }
}