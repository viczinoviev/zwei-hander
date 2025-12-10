namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Collectable, infinite life
/// </summary>
public class BlueKey : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public BlueKey(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.BlueKey()];
        Setup();
    }
}