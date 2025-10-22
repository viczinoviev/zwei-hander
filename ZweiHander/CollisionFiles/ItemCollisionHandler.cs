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
    protected IItem Item { get; set; }

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

    /// <summary>
    /// Whether the item this handles has this property or not.
    /// </summary>
    /// <param name="itemProperty">Property to check for.</param>
    /// <returns>If the item has this property.</returns>
    public bool HasProperty(ItemProperty itemProperty)
    {
        return Item.HasProperty(itemProperty);
    }
}