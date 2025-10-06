using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class FireItem : AbstractItem
{
    public FireItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
    {
        if (defaultProperties)
        {
            Properties = [

            ];
        }
    }
}
