using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;
/// <summary>
/// 2s life, spawns facing velocity, DeleteOnBlock, death sprite
/// </summary>
public class ArrowItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.DeleteOnBlock;

    protected override double Life { get; set; } = 2f;

    public ArrowItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Arrow(Velocity), itemConstructor.ItemSprites.ProjectileOnHit()];
        Setup(itemConstructor);
    }

    public override void OnDeath(GameTime gameTime)
    {
        base.OnDeath(gameTime);
        SpriteIndex = 1;
    }
}
