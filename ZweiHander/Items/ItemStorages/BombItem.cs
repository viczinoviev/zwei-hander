using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class BombItem : AbstractItem
{
    public BombItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
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
    }
}
