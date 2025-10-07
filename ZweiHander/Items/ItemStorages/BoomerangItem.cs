using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class BoomerangItem : AbstractItem
{
    public BoomerangItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
    {
        if (defaultProperties)
        {
            Properties = [];
        }
    }
}
