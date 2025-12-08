namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, collectable
/// </summary>
public class Triforce : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public Triforce(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Triforce()];
        Setup();
    }
}
