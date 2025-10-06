using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class RupyItem : AbstractItem
{
    public RupyItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
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
