using Microsoft.Xna.Framework;
using ZweiHander.Map;

namespace ZweiHander.CollisionFiles
{
    public class RoomPortalCollisionHandler : CollisionHandlerAbstract
    {
        private readonly RoomPortal _portal;

        public RoomPortalCollisionHandler(RoomPortal portal)
        {
            _portal = portal;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            if (other is PlayerCollisionHandler && _portal.CanTeleport())
            {
                _portal.Teleport();
            }
        }

        public override void UpdateCollisionBox()
        {
            collisionBox = _portal.TriggerArea;
        }
    }
}
