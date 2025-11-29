using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;
/// <summary>
/// 2s life, long death sprite, FriendlyProjectile, spawns facing velocity
/// </summary>
public class Sword : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.FriendlyProjectile;

    protected override double Life { get; set; } = 1.1;

    protected override List<double> Phases { get; set; } = [0.1];

    public Sword(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.SwordProjectile(Velocity), 
            itemConstructor.ItemSprites.SwordProjectileEffect(Velocity)];
        Setup(itemConstructor);
    }

    public override void Update(GameTime gameTime)
    {
        if(Phase == 0)
        {
            base.Update(gameTime);
        }
        else
        {
            ProgressLife((float)gameTime.ElapsedGameTime.TotalSeconds);
            Sprite.Update(gameTime);
        }
    }

    protected override void OnPhaseChange()
    {
        if(Phase == 1)
        {
            SpriteIndex = 1;
        }
    }
}
