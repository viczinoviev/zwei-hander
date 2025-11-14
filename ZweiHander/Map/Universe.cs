using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.PlayerFiles;
using ZweiHander.Enemy;
using ZweiHander.Items;
using ZweiHander.Environment;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics.SpriteStorages;
using Microsoft.Xna.Framework.Audio;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


namespace ZweiHander.Map
{
    public class Universe
    {
        private readonly Dictionary<string, Area> _areas;

        public Area CurrentArea { get; private set; }
        public Room CurrentRoom { get; private set; }
        public IPlayer Player { get; private set; }
        public EnemyManager EnemyManager { get; private set; }
        public ItemManager ItemManager { get; private set; }
        public BlockFactory BlockFactory { get; private set; }
        public BorderFactory BorderFactory { get; private set; }
        public PortalManager PortalManager { get; private set; }

        public Camera.Camera camera { get; private set; }

        public float TransitionTime = 1f;
        private float _transitionTimer = 0f;

        public int TileSize { get; private set; }

        public Universe(
            EnemySprites enemySprites,
            BossSprites bossSprites,
            NPCSprites npcSprites,
            ItemSprites itemSprites,
            TreasureSprites treasureSprites,
            BlockSprites blockSprites,
            PlayerSprites playerSprites,
            ContentManager Content,
            Camera.Camera camera,
            int tileSize = 32)
        {
            TileSize = tileSize;
            _areas = new Dictionary<string, Area>();
            
            // Create separate instances for Universe's use
            ItemManager projectileManager = new ItemManager(itemSprites, treasureSprites, bossSprites);
            ItemManager = new ItemManager(itemSprites, treasureSprites, bossSprites);
            EnemyManager = new EnemyManager(enemySprites, projectileManager, bossSprites, npcSprites, Content);
            BlockFactory = new BlockFactory(tileSize, blockSprites, playerSprites);
            BorderFactory = new BorderFactory(tileSize, blockSprites);

            this.camera = camera;
        }

        public void AddArea(Area area) => _areas[area.Name] = area;

        public Area GetArea(string areaName) => _areas.TryGetValue(areaName, out Area area) ? area : null;

        public void SetPlayer(IPlayer player) => Player = player;

        public void SetupPortalManager(Camera.Camera camera)
        {
            PortalManager = new PortalManager(this, Player, camera);
        }

        public void SetCurrentLocation(string areaName, int roomNumber)
        {
            Area area = GetArea(areaName);
            Room room = area?.GetRoom(roomNumber);

            if (area == null || room == null) return;

            UnloadContents();
            CurrentArea = area;
            CurrentRoom = room;
            CurrentRoom.Load();
        }
        public void LoadRoom(int roomNumber, Vector2 spawnPosition, Camera.Camera camera, Vector2 oldPortalPos, Vector2 newPortalPos, Direction portalDirection)
        {   
            if (_transitionTimer > 0) return;
            if (CurrentArea == null) return;

            Room targetRoom = CurrentArea.GetRoom(roomNumber);
            if (targetRoom == null) return;

            transitionSpawnPosition = spawnPosition;

            Vector2 roomSpawnTransitionOffset = calculateTransitionRoomSpawnOffset(CurrentRoom.Size, targetRoom.Size, oldPortalPos, newPortalPos, portalDirection);
            if (TransitionTime > 0 && roomSpawnTransitionOffset != Vector2.Zero)
            {
                Vector2 tiledOffset = roomSpawnTransitionOffset / TileSize;
                targetRoom.Load(true, tiledOffset);
            }

            Player.SetUpdateEnabled(false);
            CurrentRoom = targetRoom;
            _transitionTimer = TransitionTime;
            camera.OverrideMotion(roomSpawnTransitionOffset + transitionSpawnPosition + new Vector2(TileSize, TileSize), TransitionTime);

        }
        private Vector2 transitionSpawnPosition = Vector2.Zero;
        public void TransitionUpdate(GameTime gameTime)
        {
            if (_transitionTimer > 0)
            {
                _transitionTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;
                if (_transitionTimer < 0) {
                    _transitionTimer = 0; 
                    UnloadContents();
                    CurrentRoom.Load();
                    new Commands.PlayerTeleportCommand(Player, transitionSpawnPosition + new Vector2(TileSize, TileSize)).Execute();
                    new Commands.SetCameraCommand(camera, Player).Execute();
                    Player.SetUpdateEnabled(true);
                }
            }
        }
        
        private Vector2 calculateTransitionRoomSpawnOffset(Vector2 oldRoomBound, Vector2 newRoomBound, Vector2 oldPortalPos, Vector2 newPortalPos, Direction portalDirection)
        {
            if (portalDirection == Direction.Up){
                return new Vector2(oldPortalPos.X - newPortalPos.X, -newRoomBound.Y-TileSize);
            } else if (portalDirection == Direction.Down){
                return new Vector2(oldPortalPos.X - newPortalPos.X, oldRoomBound.Y+TileSize);
            } else if (portalDirection == Direction.Left){
                return new Vector2(-newRoomBound.X-TileSize, oldPortalPos.Y - newPortalPos.Y);
            } else if (portalDirection == Direction.Right){
                return new Vector2(oldRoomBound.X+TileSize, oldPortalPos.Y - newPortalPos.Y);
            }

            return Vector2.Zero;
        }
        
        private void UnloadContents()
        {
            if (CurrentRoom == null) return;

            // Clear managers/factory/portals
            BlockFactory.Clear();
            BorderFactory.Clear();
            EnemyManager.Clear();
            ItemManager.Clear();
            PortalManager.Clear();
            
            // Remove dead/null colliders immediately before loading next room
            CollisionManager.Instance.RemoveDeadColliders();
            
            CurrentRoom.IsLoaded = false;
        }

        public void Update(GameTime gameTime)
        {
            TransitionUpdate(gameTime);

            if (CurrentRoom == null || !CurrentRoom.IsLoaded || _transitionTimer > 0) return;

            EnemyManager.Update(gameTime);
            ItemManager.Update(gameTime);
            PortalManager.Update(gameTime);
        }
        

        public void Draw()
        {
            if (CurrentRoom == null || !CurrentRoom.IsLoaded) return;

            BlockFactory.Draw();
            BorderFactory.Draw();
            EnemyManager.Draw();
            ItemManager.Draw();
        }
    }
}
