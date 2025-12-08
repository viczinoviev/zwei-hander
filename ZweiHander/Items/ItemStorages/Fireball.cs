namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// 2s life, animation, EnemyProjectile
/// </summary>
public class Fireball : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.EnemyProjectile;

    protected override double Life { get; set; } = 2f;

    public Fireball(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.BossSprites.AquamentusProjectile()];
        Setup(itemConstructor);
    }
}
