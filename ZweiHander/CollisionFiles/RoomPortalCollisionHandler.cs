using Microsoft.Xna.Framework;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander.CollisionFiles
{
    public class RoomPortalCollisionHandler : CollisionHandlerAbstract
    {
        private readonly RoomPortal _portal;
        private readonly Area _area;
        private readonly Universe _universe;
        private readonly IPlayer _player;
        private readonly Camera.Camera _camera;

        public RoomPortalCollisionHandler(RoomPortal portal, Area area, Universe universe, IPlayer player, Camera.Camera camera)
        {
            _portal = portal;
            _area = area;
            _universe = universe;
            _player = player;
            _camera = camera;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            if (other is PlayerCollisionHandler && _portal.ParentRoom.IsLoaded)
            {
                IPortal targetPortal = _area.FindConnectedPortal(_portal);
                if (targetPortal != null)
                {
                    _universe.LoadRoom(targetPortal.ParentRoom.RoomNumber, targetPortal.Position, _camera);
                }
            }
        }

        public void Update()
        {
        }

        public override void UpdateCollisionBox()
        {
            collisionBox = _portal.TriggerArea;
        }
    }
}
