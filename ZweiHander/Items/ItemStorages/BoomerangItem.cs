using System;
using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
/// <summary>
/// DeleteOnBlock, 3s life, use ItemHelper.BoomerangTrajectory
/// </summary>
public class BoomerangItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.DeleteOnBlock;

    protected override double Life { get; set; } = 3f;

    public BoomerangItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Boomerang()];
        Setup(itemConstructor);
    }
}
