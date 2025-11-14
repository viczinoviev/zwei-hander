using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// 2.3s life, stationary<br></br>
/// Phase 0: Nothing
/// Phase 1: Switches to exploding sprite, damages enemies and player
/// Phase 2: Can no longer damage enemy and player
/// </summary>
public class Bomb : AbstractItem
{
    protected override double Life { get; set; } = 2.3f;

    protected override List<double> Phases { get; set; } = [0.3, 0.2];

    protected override ItemProperty Properties { get; set; } = ItemProperty.Stationary;

    public Bomb(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Bomb(), itemConstructor.ItemSprites.Explosion()];
        Sprites[1].Scale = new(10, 10);
        Setup(itemConstructor);
        if (Life < 0) AddProperty(ItemProperty.CanBePickedUp);
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
            Sprite.Update(gameTime);
        }
    }

    public override void OnPhaseChange()
    {
        if (Phase == 1)
        {
            SpriteIndex = 1;
            AddProperty(ItemProperty.CanDamagePlayer);
            AddProperty(ItemProperty.CanDamageEnemy);
        }
        else if (Phase == 2)
        {
            RemoveProperty(ItemProperty.CanDamagePlayer);
            RemoveProperty(ItemProperty.CanDamagePlayer);
        }
    }
}
