using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;

namespace ZweiHander.Items.ItemStorages;
public class FairyItem : AbstractItem
{
    private Vector2 _startingPosition;
    public FairyItem(List<ISprite> sprites, bool defaultProperties, Vector2 startingPosition)
        : base(sprites)
    {
        if (defaultProperties)
        {
            Properties = [
                ItemProperty.CanBePickedUp,
                ItemProperty.DeleteOnCollision
            ];
        }
        _startingPosition = startingPosition;
    }

    public override void Update(GameTime gameTime)
    {
        // Hard coded frequency for now
        Acceleration = 5.0f * (_startingPosition - Position);
        base.Update(gameTime);
    }
}
