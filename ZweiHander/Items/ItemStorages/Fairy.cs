using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Damage;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, animation, CanBePickedUp, hovers
/// </summary>
public class Fairy : AbstractItem
{
    protected Vector2 StartingPosition { get; set; }

    protected override ItemProperty Properties { get; set; } = ItemProperty.CanBePickedUp;

    private static readonly List<Effect> PossibleBuffs = [Effect.Speed, Effect.Strength, Effect.Regen];

    private static readonly Random rng = new();

    /// <summary>
    /// How long fairy buffs last
    /// </summary>
    public static readonly double BuffDuration = 30;

    public Fairy(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Fairy()];
        StartingPosition = Position;
        Setup();
    }

    public override void Update(GameTime gameTime)
    {
        // Hard coded frequency for now
        Acceleration = 5.0f * (StartingPosition - Position);
        base.Update(gameTime);
    }

    /// <summary>
    /// Get a random effect from a fairy.
    /// </summary>
    /// <returns>The randomly selected effect.</returns>
    public static Effect GetBuff()
    {
        return PossibleBuffs[rng.Next(PossibleBuffs.Count)];
    }
}
