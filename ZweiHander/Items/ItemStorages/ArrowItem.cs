using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Items.ItemStorages;
public class ArrowItem : AbstractItem
{
    public ArrowItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
    {
        if (defaultProperties)
        {
            Properties = [
                ItemProperty.FacingVelocity
            ];
        }
        DeathTime = 0.1;
    }

    public override void OnDeath(GameTime gameTime)
    {
        base.OnDeath(gameTime);
        _spriteIndex = 1;
    }
}
