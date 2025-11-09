using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// 2s life, animation, EnemyProjectile
/// </summary>
public class FireballItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.EnemyProjectile;

    protected override double Life { get; set; } = 2f;

    public FireballItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.BossSprites.AquamentusProjectile()];
        Setup(itemConstructor);
    }
}
