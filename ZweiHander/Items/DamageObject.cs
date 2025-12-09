namespace ZweiHander.Items;

/// <summary>
/// What to do when taking damage
/// </summary>
public class DamageObject(int damage = 0, double knockback = 0)
{
    public int Damage = damage;
    public double Knockback = knockback;
}
