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
    public ArrowItem(ISprite sprite, bool defaultProperties)
        : base(sprite)
    {
    }

    public override void OnDeath()
    {
        // Make the little sprite thing.
    }
}
