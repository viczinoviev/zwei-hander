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
                collisionBox = new Rectangle(colbox.X, colbox.Y, colbox.Width, 100);
            }
            else
            {
                collisionBox = new Rectangle(colbox.X, colbox.Y, 100, colbox.Height);
            }
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {

            Console.WriteLine(other);
            //Player collision
            if (other is PlayerCollisionHandler)
            {
                Console.WriteLine("howdy!");
                if (_enemy.Thrower == 0)
                {
                    _enemy.attackTime = 1;
                    _enemy.Thrower = 1;
                    //_enemy.Face = (int)collisionInfo.Normal;
                    _enemy.Face = 2;
                }
            }
        }

        public override void UpdateCollisionBox()
        {
            //unused, collisionBox is static
        }
    }
}