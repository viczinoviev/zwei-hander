using ZweiHander.Items;

namespace ZweiHander.CollisionFiles;

public class ItemCollisionHandler : CollisionHandlerAbstract
{
    /// <summary>
    /// The actual item this handles.
    /// </summary>
    private readonly IItem _item;

    public ItemCollisionHandler(IItem item)
    {
        _item = item;
        UpdateCollisionBox();
    }

    public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
    {
        _item.HandleCollision(other, collisionInfo);
    }

    public override void UpdateCollisionBox()
    {
        collisionBox = _item.GetHitBox();
    }
}