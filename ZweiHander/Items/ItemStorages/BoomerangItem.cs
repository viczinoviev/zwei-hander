using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Items.ItemStorages;
public class BoomerangItem : AbstractItem
{
    public BoomerangItem(List<ISprite> sprites, bool defaultProperties)
        : base(sprites)
    {
    }
}
