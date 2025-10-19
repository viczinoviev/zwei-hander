using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class MapItem : AbstractItem
{
    public MapItem(ItemConstructor itemConstructor, bool defaultProperties)
        : base(itemConstructor)
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
