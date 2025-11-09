using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Collectable, infinite life
/// </summary>
public class KeyItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public KeyItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Key()];
        Setup(itemConstructor);
    }
}
