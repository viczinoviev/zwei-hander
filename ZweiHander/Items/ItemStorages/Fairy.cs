using Microsoft.Xna.Framework;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Infinite life, animation, CanBePickedUp, hovers
/// </summary>
public class Fairy : AbstractItem
{
    protected Vector2 StartingPosition { get; set; }

    protected override ItemProperty Properties { get; set; } = ItemProperty.CanBePickedUp;

    public Fairy(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.TreasureSprites.Fairy()];
        StartingPosition = Position;
        Setup(itemConstructor);
    }

    public override void Update(GameTime gameTime)
    {
        // Hard coded frequency for now
        Acceleration = 5.0f * (StartingPosition - Position);
        base.Update(gameTime);
    }
}
