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
    public readonly IItem Item;

    /// <summary>
    /// The type of item this is handling.
    /// </summary>
    public ItemType ItemType { get => Item.ItemType; }

    public ItemCollisionHandler(IItem item)
    {
        Item = item;
        UpdateCollisionBox();
    }

    public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
    {
        Item.HandleCollision(other, collisionInfo);
    }

    public override void UpdateCollisionBox()
    {
        CollisionBox = Item.GetHitBox();
    }
}