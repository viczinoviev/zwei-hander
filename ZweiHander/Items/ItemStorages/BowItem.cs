using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, collectable
/// </summary>
public class BowItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.Collectable;

    public BowItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Bow()];
        Setup(itemConstructor);
    }
}
