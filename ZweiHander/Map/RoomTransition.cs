using System;
using Microsoft.Xna.Framework;
using ZweiHander.CollisionFiles;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Map
{
    public class RoomTransition
    {
        private float _transitionTimer = 0f;
        private Vector2 _spawnPosition = Vector2.Zero;
        
        public float TransitionTime { get; set; } = 0.5f;
        public bool IsTransitioning => _transitionTimer > 0;

        private readonly int _tileSize;
        private readonly Universe _universe;

		private int tilePadding = 1;

        public RoomTransition(Universe universe, int tileSize)
        {
            _universe = universe;
            _tileSize = tileSize;
        }

        public void StartTransition(
            Room currentRoom,
            Room targetRoom,
            Vector2 spawnPosition,
            Camera.Camera camera,
            Vector2 oldPortalPos,
            Vector2 newPortalPos,
            Direction portalDirection,
            IPlayer player)
        {
            if (_transitionTimer > 0) return;

            _spawnPosition = spawnPosition;

            Vector2 roomSpawnTransitionOffset = CalculateTransitionOffset(
                currentRoom.Size,
                targetRoom.Size,
                oldPortalPos,
                newPortalPos,
                portalDirection);

            if (TransitionTime > 0 && roomSpawnTransitionOffset != Vector2.Zero)
            {
                Vector2 tiledOffset = roomSpawnTransitionOffset / _tileSize;
                targetRoom.Load(true, tiledOffset);

				//CreateTunnelBlocks(oldPortalPos, roomSpawnTransitionOffset, portalDirection);
            }

            player.SetUpdateEnabled(false);
            _transitionTimer = TransitionTime;
            camera.OverrideMotion(roomSpawnTransitionOffset + _spawnPosition + new Vector2(_tileSize, _tileSize), TransitionTime);
        }

        // private void CreateTunnelBlocks(Vector2 oldPortalPos, Vector2 transitionOffset, Direction tunnelDirection)
		// {

        //     if (tunnelDirection == Direction.Right) // Moving Right
        //     {
        //         for (int i = 0; i <= (transitionOffset.X - oldPortalPos.X) / _tileSize; i++)
        //         {
        //             Block tunnel = _universe.BlockFactory.CreateBlock(
        //                 BlockName.TunnelTile,
        //                 new Point((int)(oldPortalPos.X / _tileSize) + i, (int)(oldPortalPos.Y / _tileSize)));

        //             //tunnel._sprite.Rotation = (float)(Math.PI / 2);
        //             //tunnel._sprite.Origin += new Vector2(_tileSize/2f, _tileSize/2f);
        //         }
        //     }
        //     if (tunnelDirection == Direction.Left) // Moving Left
        //     {
        //         for (int i = 0; i <= (-transitionOffset.X + oldPortalPos.X) / _tileSize; i++)
        //         {
        //             Block tunnel = _universe.BlockFactory.CreateBlock(
        //                 BlockName.TunnelTile,
        //                 new Point((int)(oldPortalPos.X / _tileSize) - i, (int)(oldPortalPos.Y / _tileSize)));

        //             //tunnel._sprite.Rotation = (float)(-Math.PI / 2);
        //             //tunnel._sprite.Origin += new Vector2(_tileSize/2f, _tileSize/2f);
        //         }
        //     }
        //     // TO FIX UP AND LEFT
        //     if (tunnelDirection == Direction.Down) // Moving Down
        //     {
        //         for (int i = 0; i <= (transitionOffset.Y - oldPortalPos.Y) / _tileSize; i++)
        //         {
        //             Block tunnel =_universe.BlockFactory.CreateBlock(
        //                 BlockName.TunnelTile,
        //                 new Point((int)(oldPortalPos.X / _tileSize), (int)(oldPortalPos.Y / _tileSize) + i));
        //             //tunnel._sprite.Rotation = (float)(Math.PI);
        //             //tunnel._sprite.Origin += new Vector2(_tileSize/2f, _tileSize/2f);
        //         }
        //     }
        //     if (tunnelDirection == Direction.Up) // Moving Up
        //     {
        //         for (int i = 0; i <= (-transitionOffset.Y + oldPortalPos.Y) / _tileSize; i++)
        //         {
        //             Block tunnel = _universe.BlockFactory.CreateBlock(
        //                 BlockName.TunnelTile,
        //                 new Point((int)(oldPortalPos.X / _tileSize), (int)(oldPortalPos.Y / _tileSize) - i));
        //             //tunnel._sprite.Origin += new Vector2(_tileSize/2f, _tileSize/2f);
        //         }
        //     }
		// }

        public void Update(GameTime gameTime, Room currentRoom, Camera.Camera camera, IPlayer player)
        {
            if (_transitionTimer > 0)
            {
                _transitionTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;
                if (_transitionTimer < 0)
                {
                    _transitionTimer = 0;
                    CompleteTransition(currentRoom, camera, player);
                }
            }
        }

        private void CompleteTransition(Room currentRoom, Camera.Camera camera, IPlayer player)
        {
            _universe.UnloadContents();
            currentRoom.Load();
            new Commands.PlayerTeleportCommand(player, _spawnPosition + new Vector2(_tileSize, _tileSize)).Execute();
            new Commands.SetCameraCommand(camera, player).Execute();
            player.SetUpdateEnabled(true);
        }

        private Vector2 CalculateTransitionOffset(
            Vector2 oldRoomBound,
            Vector2 newRoomBound,
            Vector2 oldPortalPos,
            Vector2 newPortalPos,
            Direction portalDirection)
        {
            if (portalDirection == Direction.Up)
            {
                return new Vector2(oldPortalPos.X - newPortalPos.X, -newRoomBound.Y - _tileSize - tilePadding*_tileSize);
            }
            else if (portalDirection == Direction.Down)
            {
                return new Vector2(oldPortalPos.X - newPortalPos.X, oldRoomBound.Y + _tileSize + tilePadding*_tileSize);
            }
            else if (portalDirection == Direction.Left)
            {
                return new Vector2(-newRoomBound.X - _tileSize - tilePadding*_tileSize, oldPortalPos.Y - newPortalPos.Y);
            }
            else if (portalDirection == Direction.Right)
            {
                return new Vector2(oldRoomBound.X + _tileSize + tilePadding*_tileSize, oldPortalPos.Y - newPortalPos.Y);
            }

            return Vector2.Zero;
        }
    }
}
