using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class TriforceItem : AbstractItem
{
    public TriforceItem(ItemConstructor itemConstructor, bool defaultProperties)
        : base(itemConstructor)
    {
        if (defaultProperties)
        {
            Properties = ItemProperty.Collectable;
        }
    }
}
