using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;
/// <summary>
/// 1.1s life, spawns facing velocity, DeleteOnBlock, death sprite
/// </summary>
public class Arrow : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.DeleteOnBlock;

    protected override double Life { get; set; } = 1.1;

    protected override List<double> Phases { get; set; } = [0.1];

    public Arrow(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Arrow(Velocity), itemConstructor.ItemSprites.ProjectileOnHit()];
        Setup(itemConstructor);
    }

    public override void Update(GameTime gameTime)
    {
        if (Phase == 0)
        {
            base.Update(gameTime);
        } 
        else
        {
            ProgressLife((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    protected override void OnPhaseChange()
    {
        if (Phase == 1)
        {
            SpriteIndex = 1;
        }
    }
}
