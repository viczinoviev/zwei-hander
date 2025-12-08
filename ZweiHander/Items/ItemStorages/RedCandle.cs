namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, collectable
/// </summary>
public class RedCandle : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public RedCandle(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.CandleRed()];
        Setup();
    }
}
