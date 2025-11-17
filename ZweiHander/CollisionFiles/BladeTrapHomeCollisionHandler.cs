using Microsoft.Xna.Framework;
using System;
using System.Collections;
using ZweiHander.Enemy.EnemyStorage;

namespace ZweiHander.CollisionFiles
{
    public class BladeTrapHomeCollisionHandler : CollisionHandlerAbstract
    {
        /// <summary>
        /// The enemy this handler manages
        /// </summary>
        private BladeTrap _enemy;

        private readonly string _axis;

        public BladeTrapHomeCollisionHandler(BladeTrap enemy, string axis)
        {
            _enemy = enemy;
            _axis = axis;
            Rectangle colbox = _enemy.GetCollisionBox();
            if (_axis == "yu")
            {
                collisionBox = new Rectangle(colbox.X, colbox.Y - 100, colbox.Width, 100);
            }
            else if(_axis == "yd")
            {
                collisionBox = new Rectangle(colbox.X, colbox.Y + colbox.Height + 1, colbox.Width, colbox.Height + 100);
            }
            else if(_axis == "xl")
            {
                collisionBox = new Rectangle(colbox.X - 100, colbox.Y, 100, colbox.Height);
            }
            else
            {
                collisionBox = new Rectangle(colbox.X + colbox.Width, colbox.Y, colbox.Width + 100, colbox.Height);
            }
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            //Player collision
            if (other is PlayerCollisionHandler)
            {
                if (_enemy.Thrower == 0)
                {
                    _enemy.attackTime = 1;
                    _enemy.Thrower = 1;
                    switch (_axis)
                    {
                        case "yu":
                        _enemy.Face = 0;
                        break;
                        case "xr":
                        _enemy.Face = 1;
                        break;
                        case "yd":
                        _enemy.Face = 2;
                        break;
                        default:
                        _enemy.Face = 3;
                        break;
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