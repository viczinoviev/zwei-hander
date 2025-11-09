using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.PlayerFiles;
using ZweiHander.Enemy;
using ZweiHander.Items;
using ZweiHander.Environment;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics.SpriteStorages;

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

        public Universe(
            EnemySprites enemySprites,
            BossSprites bossSprites,
            NPCSprites npcSprites,
            ItemSprites itemSprites,
            TreasureSprites treasureSprites,
            BlockSprites blockSprites,
            PlayerSprites playerSprites,
            int tileSize = 32)
        {
            _areas = new Dictionary<string, Area>();
            
            // Create separate instances for Universe's use
            ItemManager projectileManager = new ItemManager(itemSprites, treasureSprites, bossSprites);
            ItemManager = new ItemManager(itemSprites, treasureSprites, bossSprites);
            EnemyManager = new EnemyManager(enemySprites, projectileManager, bossSprites, npcSprites);
            BlockFactory = new BlockFactory(tileSize, blockSprites, playerSprites);
        }

        public void AddArea(Area area) => _areas[area.Name] = area;

        public Area GetArea(string areaName) => _areas.TryGetValue(areaName, out Area area) ? area : null;

        public void SetPlayer(IPlayer player) => Player = player;

        public void SetCurrentLocation(string areaName, int roomNumber)
        {
            Area area = GetArea(areaName);
            Room room = area?.GetRoom(roomNumber);
            
            if (area == null || room == null) return;

            UnloadCurrentRoom();
            CurrentArea = area;
            CurrentRoom = room;
            CurrentRoom.Load();
        }

        public void LoadRoom(int roomNumber, Vector2 spawnPosition, Camera.Camera camera)
        {
            if (CurrentArea == null) return;

            Room targetRoom = CurrentArea.GetRoom(roomNumber);
            if (targetRoom == null) return;

            UnloadCurrentRoom();
            CurrentRoom = targetRoom;
            CurrentRoom.Load();

            new Commands.PlayerTeleportCommand(Player, spawnPosition + new Vector2(16, 16)).Execute();
            new Commands.SetCameraCommand(camera, Player).Execute();
        }
        
        private void UnloadCurrentRoom()
        {
            if (CurrentRoom == null) return;
            
            // Unsubscribe portal collision handlers
            foreach (var portal in CurrentRoom.Portals)
            {
                if (portal is RoomPortal roomPortal)
                {
                    roomPortal.OnRoomUnload();
                }
            }
            
            // Clear managers/factory - each handles its own collision cleanup
            BlockFactory.Clear();
            EnemyManager.Clear();
            ItemManager.Clear();
            
            CurrentRoom.IsLoaded = false;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentRoom == null || !CurrentRoom.IsLoaded) return;
            
            CurrentRoom.Update(gameTime);
            EnemyManager.Update(gameTime);
            ItemManager.Update(gameTime);
        }

        public void Draw()
        {
            if (CurrentRoom == null || !CurrentRoom.IsLoaded) return;

            BlockFactory.Draw();
            EnemyManager.Draw();
            ItemManager.Draw();
            
            // Draw portals (if needed for debug visualization)
            foreach (var portal in CurrentRoom.Portals)
            {
                // Portals don't have a Draw method currently, just trigger areas
                // Add portal drawing logic here if needed in the future
            }
        }
    }
}
