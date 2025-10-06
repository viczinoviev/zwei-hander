using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class ArrowItem : AbstractItem
{
    public ArrowItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
    {
        if (defaultProperties)
        {
            Properties = [];
        }
        DeathTime = 0.1;
    }

    public override void OnDeath(GameTime gameTime)
    {
        base.OnDeath(gameTime);
        _spriteIndex = 1;
    }
}
