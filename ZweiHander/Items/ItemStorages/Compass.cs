namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Collectable, Infinite life
/// </summary>
public class Compass : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public Compass(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Compass()];
        Setup(itemConstructor);
    }
}
