using Microsoft.Xna.Framework;
using ZweiHander.Environment;

namespace ZweiHander.CollisionFiles
{
    public class BorderCollisionHandler : CollisionHandlerAbstract
    {
        private readonly Border _border;

        public BorderCollisionHandler(Border border)
        {
            _border = border;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {

        }

        public override void UpdateCollisionBox()
        {
            collisionBox = _border.GetBorderHitbox();
        }
    }
}
