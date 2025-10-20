using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class FireballItem : AbstractItem
{
    public FireballItem(ItemConstructor itemConstructor, bool defaultProperties)
        : base(itemConstructor)
    {
        if (defaultProperties)
        {
            Properties = [
                ItemProperty.DeleteOnPlayer,
                ItemProperty.CanDamagePlayer
            ];
        }
    }
}
