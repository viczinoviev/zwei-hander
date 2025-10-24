using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class BombItem : AbstractItem
{
    public BombItem(ItemConstructor itemConstructor, bool defaultProperties)
        : base(itemConstructor)
    {
        if (defaultProperties)
        {
        }
        DeathTime = 0.3;
    }

    public override void OnDeath(GameTime gameTime)
    {
        base.OnDeath(gameTime);
        _spriteIndex = 1;
        Sprite.Update(gameTime);
    }
}
