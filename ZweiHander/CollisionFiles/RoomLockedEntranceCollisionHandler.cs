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
            if (other is not PlayerCollisionHandler playerHandler)
                return;
            if (playerHandler._player.InventoryCount(typeof(Key)) > 0)
            {
                if(_LockedEntrance.ReplaceWithUnlockedEntrance(1))
                    playerHandler._player.RemoveItemFromInventory(typeof(Key));
            }
            if (playerHandler._player.InventoryCount(typeof(BlueKey)) > 0)
            {
                if(_LockedEntrance.ReplaceWithUnlockedEntrance(2))
                    playerHandler._player.RemoveItemFromInventory(typeof(BlueKey));
            }
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = _LockedEntrance.TriggerArea;
        }
    }
}
