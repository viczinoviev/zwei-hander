using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class RoomLockedEntrance : IPortal
    {
        public int PortalId { get; }
        public Vector2 Position { get; }
        public Rectangle TriggerArea { get; }
        public Room ParentRoom { get; }
        public Area ParentArea { get; }

        private readonly Universe _universe;
        private readonly IPlayer _player;
        private readonly Camera.Camera _camera;
        private readonly RoomLockedEntranceCollisionHandler _collisionHandler;

        

        public RoomLockedEntrance(int portalId, Vector2 position, Room parentRoom, Area parentArea, Universe universe, IPlayer player, Camera.Camera camera)
        {
            PortalId = portalId;
            Position = position;
            ParentRoom = parentRoom;
            ParentArea = parentArea;
            int expand = 32;

            TriggerArea = new Rectangle(
                (int)(position.X - expand),
                (int)(position.Y - expand),
                universe.TileSize + expand * 2,
                universe.TileSize + expand * 2
            );

            _universe = universe;
            _player = player;
            _camera = camera;
            _collisionHandler = new RoomLockedEntranceCollisionHandler(this);
        }


        public void OnRoomUnload()
        {
            _collisionHandler.Dead = true;
        }

        public void Update(GameTime gameTime)
        {

        }


        private static readonly Dictionary<BorderName, BorderName> LockedBorderToDoorBorder = new()
        {
			// Wall tiles - w.{direction}
			{ BorderName.LockedDoorTileNorth, BorderName.EntranceTileNorth },
            { BorderName.LockedDoorTileWest, BorderName.EntranceTileWest },
            { BorderName.LockedDoorTileEast, BorderName.EntranceTileEast },
            { BorderName.LockedDoorTileSouth, BorderName.EntranceTileSouth}
        };
        public void ReplaceWithUnlockedEntrance(Direction direction = Direction.None)
        {
            string doorTag = direction switch
            {
                Direction.Up => "dn", // north door tile
                Direction.Down => "ds", // south
                Direction.Left => "dw", // west
                Direction.Right => "de", // east
                _ => null
            };

            if (doorTag == null)
                return;

            var borders = _universe.BorderFactory.BorderMap;

            for (int i = borders.Count - 1; i >= 0; i--)
            {
                Border border = borders[i];

                //if border is a locked door
                if (border.Name is BorderName.LockedDoorTileNorth
                    or BorderName.LockedDoorTileSouth
                    or BorderName.LockedDoorTileEast
                    or BorderName.LockedDoorTileWest)
                {
                    //if the locked entrance hitbox is intersecting with border hitbox
                    if (TriggerArea.Intersects(border.GetHitBox()))
                    {
                        border.UnsubscribeFromCollisions();
                        BorderName _borderName = border.Name;
                        borders.RemoveAt(i);

                        //spawn unlocked door on the position
                        if (LockedBorderToDoorBorder.TryGetValue(_borderName, out var openDoor))
                        {
                            _universe.BorderFactory.CreateBorder(openDoor, Position);
                            ParentRoom.AddBorder(openDoor, Position);
                        }

                        break;
                    }       
                }
            }

            _collisionHandler.Dead = true;


        }
    }
}
