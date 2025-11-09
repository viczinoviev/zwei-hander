using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// 2s life, death animation, CanBePickedUp
/// </summary>
public class BombItem : AbstractItem
{
    protected override double Life { get; set; } = 2f;

    protected override double DeathTime { get; set; } = 0.3;

    public BombItem(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Bomb(), itemConstructor.ItemSprites.Explosion()];
        Setup(itemConstructor);
    }

    public override void OnDeath(GameTime gameTime)
    {
        base.OnDeath(gameTime);
        SpriteIndex = 1;
        Sprite.Update(gameTime);
    }
}
