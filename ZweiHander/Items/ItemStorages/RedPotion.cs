namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, collectable
/// </summary>
public class RedPotion : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public RedPotion(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.PotionRed()];
        Setup();
    }
}
