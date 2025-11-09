using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;
/// <summary>
/// 2s life, long death sprite, FriendlyProjectile, spawns facing velocity
/// </summary>
public class SwordItem : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.FriendlyProjectile;

    protected override double Life { get; set; } = 2f;

    protected override double DeathTime { get; set; } = 0.1;

    public SwordItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.SwordProjectile(Velocity), 
            itemConstructor.ItemSprites.SwordProjectileEffect(Velocity)];
        Setup(itemConstructor);
    }

    public override void OnDeath(GameTime gameTime)
    {
        base.OnDeath(gameTime);
        SpriteIndex = 1;
    }
}
