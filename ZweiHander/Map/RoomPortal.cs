using Microsoft.Xna.Framework;
using ZweiHander.CollisionFiles;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class RoomPortal : IPortal
    {
        public int PortalId { get; }
        public Vector2 Position { get; }
        public Rectangle TriggerArea { get; }
        public Room ParentRoom { get; }
        public Area ParentArea { get; }
        public RoomPortalCollisionHandler CollisionHandler { get; }

        public RoomPortal(int portalId, Vector2 position, Room parentRoom, Area parentArea, Universe universe, IPlayer player, Camera.Camera camera)
        {
            PortalId = portalId;
            Position = position;
            ParentRoom = parentRoom;
            ParentArea = parentArea;
            TriggerArea = new Rectangle((int)position.X + 12, (int)position.Y + 12, 8, 8);
            CollisionHandler = new RoomPortalCollisionHandler(this, parentArea, universe, player, camera);
            CollisionManager.Instance.AddCollider(CollisionHandler);
        }
    }
}
