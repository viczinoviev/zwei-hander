using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class BoomerangItem : AbstractItem
{
    public BoomerangItem(ItemConstructor itemConstructor, bool defaultProperties)
        : base(itemConstructor)
    {
        if (defaultProperties)
        {
            Properties = ItemProperty.DeleteOnBlock;
        }
    }
}
