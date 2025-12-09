using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Give infinite life for collectable<br></br>
/// 4.3s life, stationary<br></br>
/// Phase 0: Nothing
/// Phase 1: Switches to exploding sprite, damages enemies and player
/// Phase 2: Can no longer damage enemy and player
/// </summary>
public class Bomb : AbstractItem
{
    protected override double Life { get; set; } = 4.3f;

    protected override List<double> Phases { get; set; } = [0.3, 0.2];

    protected override ItemProperty Properties { get; set; } = ItemProperty.Stationary;

    /// <summary>
    /// When to slide next during explosion buildup
    /// </summary>
    private double Wiggle { get; set; } = 0;

    /// <summary>
    /// How to scale Wiggle based upon life
    /// </summary>
    private double WiggleScalar { get; } = 0.05;

    public Bomb(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Bomb(), itemConstructor.ItemSprites.Explosion()];
        Sprites[1].Scale = new(10, 10);
        Setup();
        if (Life < 0)
        {
            AddProperty(ItemProperty.CanBePickedUp);
        }
        else
        {
            Wiggle = WiggleScalar * Life;
            SpriteOffset = new(-2, 0);
        }
        Damage.Add<Player>(new(2));
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Phase == 0)
        {
            base.Update(gameTime);
            if (Life > 0)
            {
                Sprites[0].Scale += new Vector2(dt, dt) / 2;
                Wiggle -= dt;
                if (Wiggle <= 0)
                {
                    SpriteOffset *= -1;
                    Wiggle = Life * WiggleScalar;
                }

            }
        }
        else
        {
            ProgressLife(dt);
            Sprite.Update(gameTime);
        }
    }

    protected override void OnPhaseChange()
    {
        if (Phase == 1)
        {
            SpriteIndex = 1;
            AddProperty(ItemProperty.CanDamagePlayer);
            AddProperty(ItemProperty.CanDamageEnemy);
            SpriteOffset = Vector2.Zero;
        }
        else if (Phase == 2)
        {
            RemoveProperty(ItemProperty.CanDamagePlayer);
            RemoveProperty(ItemProperty.CanDamagePlayer);
        }
    }

    protected override void ItemInteract(ItemCollisionHandler other, CollisionInfo collisionInfo)
    {
        switch (other.Item)
        {
            case Bomb bomb:
                if (bomb.HasProperty(ItemProperty.CanDamagePlayer) && Life > 0 && Phase == 0) Life = Phases[Phase];
                break;
                //case Fire: //Yes, explode on *any* fire
                //    if (Life > 0 && Phase == 0) Life = Phases[Phase];
                //    break;
        }
    }
}
