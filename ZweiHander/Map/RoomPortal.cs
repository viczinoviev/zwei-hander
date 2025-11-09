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
        
        private readonly Universe _universe;
        private readonly IPlayer _player;
        private readonly Camera.Camera _camera;
        private readonly RoomPortalCollisionHandler _collisionHandler;
        
        private float _cooldownTimer = 0f;
        private const float COOLDOWN_TIME = 1f;

        public RoomPortal(int portalId, Vector2 position, Room parentRoom, Area parentArea, Universe universe, IPlayer player, Camera.Camera camera)
        {
            PortalId = portalId;
            Position = position;
            ParentRoom = parentRoom;
            ParentArea = parentArea;
            TriggerArea = new Rectangle((int)position.X-4, (int)position.Y-4, 8, 8);
            
            _universe = universe;
            _player = player;
            _camera = camera;
            _collisionHandler = new RoomPortalCollisionHandler(this);
        }

        public void OnRoomLoad()
        {
            _cooldownTimer = COOLDOWN_TIME;
            CollisionManager.Instance.AddCollider(_collisionHandler);
        }

        public void OnRoomUnload()
        {
            _collisionHandler.Unsubscribe();
        }

        public void Update(GameTime gameTime)
        {
            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public bool CanTeleport()
        {
            return _cooldownTimer <= 0f && ParentRoom.IsLoaded;
        }

        public void Teleport()
        {
            IPortal targetPortal = ParentArea.FindConnectedPortal(this);
            if (targetPortal != null)
            {
                Vector2 spawnPosition = targetPortal.Position - new Vector2(16, 16);
                _universe.LoadRoom(targetPortal.ParentRoom.RoomNumber, spawnPosition, _camera);
            }
        }
    }
}
