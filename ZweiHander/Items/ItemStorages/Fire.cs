using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// 2.5s life, animation
/// </summary>
public class Fire : AbstractItem
{
    protected override double Life { get; set; } = 2.5;

    public Fire(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.FireProjectile()];
        Setup(itemConstructor);
    }
}
