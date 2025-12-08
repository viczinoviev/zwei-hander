using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Enemy;
using ZweiHander.Environment;
using ZweiHander.FriendlyNPC;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.PlayerFiles;


namespace ZweiHander.Map
{
    public class Universe
    {
        private readonly Dictionary<string, Area> _areas;

        public Area CurrentArea { get; private set; }
        public Room CurrentRoom { get; private set; }
        public IPlayer Player { get; private set; }
        public IKirby Kirby { get; private set; }
        public EnemyManager EnemyManager { get; }
        public ItemManager ItemManager { get; }
        public BlockManager BlockFactory { get; }
        public BorderManager BorderFactory { get; }
        public PortalManager PortalManager { get; private set; }

        public LockedEntranceManager LockedEntranceManager { get; private set; }

        public Camera.Camera Camera { get; }

        public RoomTransition RoomTransition { get; }

        public int TileSize { get; }

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
            _areas = [];

            // Create separate instances for Universe's use
            ItemManager projectileManager = new(itemSprites, treasureSprites, bossSprites);
            ItemManager = new ItemManager(itemSprites, treasureSprites, bossSprites);
            EnemyManager = new EnemyManager(enemySprites, projectileManager, bossSprites, npcSprites, Content);
            BlockFactory = new BlockManager(tileSize, blockSprites, playerSprites);
            BorderFactory = new BorderManager(tileSize, blockSprites);
            RoomTransition = new RoomTransition(this, tileSize);

            this.Camera = camera;
        }

        public void AddArea(Area area) => _areas[area.Name] = area;

        public Area GetArea(string areaName) => _areas.TryGetValue(areaName, out Area area) ? area : null;

        public void SetPlayer(IPlayer player) => Player = player;

        public void SetKirby(IKirby kirby) => Kirby = kirby;

        public void SetupPortalManager(Camera.Camera camera)
        {
            PortalManager = new PortalManager(this, Player, camera);
        }

        public void SetupLockedEntranceManager(Camera.Camera camera)
        {
            LockedEntranceManager = new LockedEntranceManager(this, Player, camera);
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

        public void ForceChangeRoom(int roomNumber)
        {
            if (CurrentArea == null) return;

            Room targetRoom = CurrentArea.GetRoom(roomNumber);
            if (targetRoom == null) return;

            UnloadContents();
            CurrentRoom = targetRoom;
            CurrentRoom.Load();
        }

        public void LoadRoom(int roomNumber, Vector2 spawnPosition, Camera.Camera camera, Vector2 oldPortalPos, Vector2 newPortalPos, Direction portalDirection)
        {
            if (RoomTransition.IsTransitioning) return;
            if (CurrentArea == null) return;

            Room targetRoom = CurrentArea.GetRoom(roomNumber);
            if (targetRoom == null) return;

            // Remove items and enemies that were picked up/killed in the previous room
            CurrentRoom.PersistentRemoveItemAndEnemy(ItemManager, EnemyManager, BorderFactory);

            RoomTransition.StartTransition(CurrentRoom, targetRoom, spawnPosition, camera, oldPortalPos, newPortalPos, portalDirection, Player);

            CurrentRoom = targetRoom;
        }

        internal void UnloadContents()
        {
            if (CurrentRoom == null) return;

            // Clear managers/factory/portals
            BlockFactory.Clear();
            BorderFactory.Clear();
            EnemyManager.Clear();
            ItemManager.Clear();
            PortalManager.Clear();
            LockedEntranceManager.Clear();

            // Remove dead/null colliders immediately before loading next room
            CollisionManager.Instance.RemoveDeadColliders();

            CurrentRoom.IsLoaded = false;
        }

        public void Update(GameTime gameTime)
        {
            RoomTransition.Update(gameTime, CurrentRoom, Camera, Player, Kirby);

            if (CurrentRoom?.IsLoaded != true || RoomTransition.IsTransitioning) return;

            EnemyManager.Update(gameTime);
            ItemManager.Update(gameTime);
        }


        public void Draw()
        {
            if (CurrentRoom?.IsLoaded != true) return;

            BlockFactory.Draw();
            BorderFactory.Draw();
            EnemyManager.Draw();
            ItemManager.Draw();
        }
    }
}
