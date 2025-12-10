namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Collectable, infinite life
/// </summary>
public class MagicKey : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public MagicKey(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.KeyMagical()];
        Setup();
    }
}