using Microsoft.Xna.Framework;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander.CollisionFiles
{
    public class RoomPortalCollisionHandler : CollisionHandlerAbstract
    {
        private readonly RoomPortal _portal;
        private readonly Area _area;
        private readonly IPlayer _player;

        public RoomPortalCollisionHandler(RoomPortal portal, Area area, IPlayer player)
        {
            _portal = portal;
            _area = area;
            _player = player;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            if (other is PlayerCollisionHandler)
            {
                IPortal targetPortal = _area.FindConnectedPortal(_portal);
                if (targetPortal != null)
                {
                    _area.LoadRoom(targetPortal.ParentRoom.RoomNumber, targetPortal.Position, _player);
                }
            }
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = _portal.TriggerArea;
        }
    }
}
