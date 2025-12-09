namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, collectable
/// </summary>
public class Bow : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;
    public Bow(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Bow()];
        Setup();
    }
}
