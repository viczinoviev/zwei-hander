using Microsoft.Xna.Framework;
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

        private const int CollisionBoxOffset = 100;
        private const int Down = 2;
        private const int Left = 3;

        public BladeTrapHomeCollisionHandler(BladeTrap enemy, string axis)
        {
            _enemy = enemy;
            _axis = axis;
            Rectangle colbox = _enemy.GetCollisionBox();
            if (_axis == "yu")
            {
                collisionBox = new Rectangle(colbox.X, colbox.Y - CollisionBoxOffset, colbox.Width, CollisionBoxOffset);
            }
            else if(_axis == "yd")
            {
                collisionBox = new Rectangle(colbox.X, colbox.Y + colbox.Height + 1, colbox.Width, CollisionBoxOffset);
            }
            else if(_axis == "xl")
            {
                collisionBox = new Rectangle(colbox.X - CollisionBoxOffset, colbox.Y, CollisionBoxOffset, colbox.Height);
            }
            else
            {
                collisionBox = new Rectangle(colbox.X + colbox.Width, colbox.Y, CollisionBoxOffset, colbox.Height);
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
                        _enemy.Face = Down;
                        break;
                        default:
                        _enemy.Face = Left;
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