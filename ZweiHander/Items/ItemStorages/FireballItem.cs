using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class FireballItem : AbstractItem
{
    public FireballItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
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
