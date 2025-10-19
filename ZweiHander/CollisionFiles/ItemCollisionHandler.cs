using ZweiHander.Items;

namespace ZweiHander.CollisionFiles;

/// <summary>
/// Handles collisions for items.
/// </summary>
public class ItemCollisionHandler : CollisionHandlerAbstract
{
    /// <summary>
    /// The actual item this handles.
    /// </summary>
    private readonly IItem _item;

    /// <summary>
    /// The type of item this is handling.
    /// </summary>
    public ItemType ItemType { get => _item.ItemType; }

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
        CollisionBox = _item.GetHitBox();
    }
}