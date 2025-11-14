using Microsoft.Xna.Framework;
using System;
using ZweiHander.Enemy.EnemyStorage;

namespace ZweiHander.CollisionFiles
{
    public class BladeTrapHomeCollisionHandler : CollisionHandlerAbstract
    {
        /// <summary>
        /// The enemy this handler manages
        /// </summary>
        private BladeTrap _enemy;

        private readonly char _axis;

        public BladeTrapHomeCollisionHandler(BladeTrap enemy, char axis)
        {
            _enemy = enemy;
            _axis = axis;
            Rectangle colbox = _enemy.GetCollisionBox();
            if (_axis == 'y')
            {
                collisionBox = new Rectangle(colbox.X, colbox.Y - 100, colbox.Width, 200 + colbox.Height);
            }
            else
            {
                collisionBox = new Rectangle(colbox.X - 100, colbox.Y, 200 + colbox.Width, colbox.Height);
            }
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {

            //Player collision
            if (other is PlayerCollisionHandler playerCollisionHandler)
            {
                if (_enemy.Thrower == 0)
                {
                    _enemy.attackTime = 1;
                    _enemy.Thrower = 1;
                    float yDiff = playerCollisionHandler._player.Position.Y - _enemy.Position.Y;
                    float xDiff = playerCollisionHandler._player.Position.X - _enemy.Position.X;
                    if (Math.Abs(yDiff) > Math.Abs(xDiff))
                    {
                        if (yDiff > 0)
                        {
                            _enemy.Face = 2;
                        }
                        else
                        {
                            _enemy.Face = 0;
                        }
                    }
                    else
                    {
                        if (xDiff > 0)
                        {
                            _enemy.Face = 1;
                        }
                        else
                        {
                            _enemy.Face = 3;
                        }
                    }
                }
            }
        }

        public override void UpdateCollisionBox()
        {
            //unused, collisionBox is static
        }
    }
}