using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class FireItem : AbstractItem
{
    public FireItem(ItemConstructor itemConstructor, bool defaultProperties)
        : base(itemConstructor)
    {
        if (defaultProperties)
        {
            Properties = [

            ];
        }
    }
}
