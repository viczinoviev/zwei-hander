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
        private float COOLDOWN_TIME = -1f;

        private Vector2 TRIGGER_AREA = new Vector2(16, 16);

        private const int TELEPORT_OFFSET = 20;

        public RoomPortal(int portalId, Vector2 position, Room parentRoom, Area parentArea, Universe universe, IPlayer player, Camera.Camera camera)
        {
            PortalId = portalId;
            Position = position;
            ParentRoom = parentRoom;
            ParentArea = parentArea;
            TriggerArea = new Rectangle((int)position.X + universe.TileSize / 2 - (int)TRIGGER_AREA.X / 2, (int)position.Y + universe.TileSize / 2 - (int)TRIGGER_AREA.Y / 2, (int)TRIGGER_AREA.X, (int)TRIGGER_AREA.Y);
            
            _universe = universe;
            _player = player;
            _camera = camera;
            _collisionHandler = new RoomPortalCollisionHandler(this);
            COOLDOWN_TIME = universe.TransitionTime;
        }

        public void OnRoomLoad()
        {
            _cooldownTimer = COOLDOWN_TIME;
        }

        public void OnRoomUnload()
        {
            _collisionHandler.Dead = true;
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

        public void Teleport(Direction direction = Direction.None)
        {
            var connectedPortalData = ParentArea.FindConnectedPortalData(PortalId, ParentRoom.RoomNumber);
            if (connectedPortalData.HasValue)
            {
                Vector2 offsetDirection = Vector2.Zero;
                switch(direction)
                {
                    case Direction.Up:
                        offsetDirection = new Vector2(0, -TELEPORT_OFFSET);
                        break;
                    case Direction.Down:
                        offsetDirection = new Vector2(0, TELEPORT_OFFSET);
                        break;
                    case Direction.Left:
                        offsetDirection = new Vector2(-TELEPORT_OFFSET, 0);
                        break;
                        break;
                    case Direction.Right:
                        offsetDirection = new Vector2(TELEPORT_OFFSET, 0);
                        break;
                    case Direction.None:
                    default:
                        break;
                }
                Vector2 spawnPosition = connectedPortalData.Value.position - new Vector2(_universe.TileSize / 2, _universe.TileSize / 2) + offsetDirection;
                _universe.LoadRoom(connectedPortalData.Value.roomNumber, spawnPosition, _camera, Position, connectedPortalData.Value.position, direction);
            }
        }
    }
}
