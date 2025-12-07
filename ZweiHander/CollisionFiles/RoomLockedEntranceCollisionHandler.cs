using System;
using Microsoft.Xna.Framework;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;
using ZweiHander.Map;

namespace ZweiHander.CollisionFiles
{
    public class RoomLockedEntranceCollisionHandler : CollisionHandlerAbstract
    {
        private readonly RoomLockedEntrance _LockedEntrance;

        public RoomLockedEntranceCollisionHandler(RoomLockedEntrance lockedEntrance)
        {
            _LockedEntrance = lockedEntrance;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            if (other is PlayerCollisionHandler playerHandler && playerHandler._player.InventoryCount(typeof(Key)) > 0)
            {
                _LockedEntrance.ReplaceWithUnlockedEntrance(collisionInfo.Normal);
                playerHandler._player.RemoveItemFromInventory(typeof(Key));
            }
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = _LockedEntrance.TriggerArea;
        }
    }
}
