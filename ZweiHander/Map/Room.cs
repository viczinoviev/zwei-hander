using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Environment;
using ZweiHander.Enemy;
using ZweiHander.Items;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Map
{
    public class Room
    {
        public int RoomNumber { get; }
        public Vector2 Position { get; }
        public Vector2 Size { get; }
        public bool IsLoaded { get; private set; }

        public List<Block> Blocks { get; }
        public List<IEnemy> Enemies { get; }
        public List<IItem> Items { get; }
        public List<IPortal> Portals { get; }

        private Rectangle Bounds;

        public Room(int roomNumber, Vector2 position, Vector2 size)
        {
            RoomNumber = roomNumber;
            Position = position;
            Size = size;
            Bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Blocks = new List<Block>();
            Enemies = new List<IEnemy>();
            Items = new List<IItem>();
            Portals = new List<IPortal>();
        }

        public void AddBlock(Block block) => Blocks.Add(block);
        public void AddEnemy(IEnemy enemy) => Enemies.Add(enemy);
        public void AddItem(IItem item) => Items.Add(item);
        public void AddPortal(IPortal portal) => Portals.Add(portal);

        public void Load()
        {
            CleanupNullReferences();
            IsLoaded = true;
            
            // Re-register all collision handlers when room loads
            foreach (var portal in Portals)
            {
                if (portal is RoomPortal roomPortal && roomPortal.CollisionHandler != null)
                {
                    CollisionManager.Instance.AddCollider(roomPortal.CollisionHandler);
                }
            }
            
            foreach (var block in Blocks)
            {
                if (block?.CollisionHandler != null)
                {
                    CollisionManager.Instance.AddCollider(block.CollisionHandler);
                }
            }
            
            foreach (var enemy in Enemies)
            {
                if (enemy?.CollisionHandler != null)
                {
                    CollisionManager.Instance.AddCollider(enemy.CollisionHandler);
                }
            }
            
            foreach (var item in Items)
            {
                if (item?.CollisionHandler != null)
                {
                    CollisionManager.Instance.AddCollider(item.CollisionHandler);
                }
            }
        }

        public void Unload()
        {
            CleanupNullReferences();
            IsLoaded = false;
            
            // Unsubscribe all collision handlers when room unloads
            foreach (var portal in Portals)
            {
                if (portal is RoomPortal roomPortal)
                {
                    roomPortal.CollisionHandler?.Unsubscribe();
                }
            }
            
            foreach (var block in Blocks)
            {
                block?.CollisionHandler?.Unsubscribe();
            }
            
            foreach (var enemy in Enemies)
            {
                enemy?.CollisionHandler?.Unsubscribe();
            }
            
            foreach (var item in Items)
            {
                item?.CollisionHandler?.Unsubscribe();
            }
        }

        public void CleanupNullReferences()
        {
            Blocks.RemoveAll(b => b == null);
            Enemies.RemoveAll(e => e == null || e.Hitpoints <= 0);
            Items.RemoveAll(i => i == null);
        }

        public void Update(GameTime gameTime)
        {
            if (!IsLoaded) return;

            foreach (var enemy in Enemies)
                enemy?.Update(gameTime);

            foreach (var item in Items)
                item?.Update(gameTime);

            foreach (var portal in Portals)
            {
                if (portal is RoomPortal roomPortal)
                    roomPortal.CollisionHandler?.Update();
            }

            CleanupNullReferences();
        }

        public bool Contains(Vector2 position) => Bounds.Contains(position);
    }
}
