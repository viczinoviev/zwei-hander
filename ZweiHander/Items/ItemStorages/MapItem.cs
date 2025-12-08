namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Collectable, infinite life
/// </summary>
public class MapItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public MapItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Map()];
        Setup();
    }
}
