using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Items.ItemStorages;
public class HeartItem : AbstractItem
{
    public HeartItem(ISprite sprite, bool defaultProperties)
        : base(sprite)
    {
        if (defaultProperties)
        {
            Properties = [
                ItemProperty.CanBePickedUp,
                ItemProperty.DeleteOnCollision,
                ItemProperty.Stationary
            ];
        }
    }
}
