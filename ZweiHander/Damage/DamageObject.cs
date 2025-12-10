using System;
using System.Collections.Generic;
using System.Linq;

namespace ZweiHander.Damage;

/// <summary>
/// What to do when taking damage
/// </summary>
/// <param name="damage">damage to apply</param>
/// <param name="knockback">knockback to apply</param>
/// <param name="effects">effects to apply</param>
public class DamageObject(int damage = 0, double knockback = 0, params (Effect effect, double duration)[] effects)
{
    /// <summary>
    /// The actual damage to be dealt.
    /// </summary>
    public int Damage = damage;

    /// <summary>
    /// How much knockback to take.
    /// </summary>
    public double Knockback = knockback;

    /// <summary>
    /// Effects associated with this damage
    /// </summary>
    public Dictionary<Effect, double> Effects = effects.ToDictionary(x => x.effect, x => x.duration);

    /// <summary>
    /// Gets the damage associated with a type using a dictionary. 
    /// Starts at most derived type, and works up, checking interfaces at each level.
    /// </summary>
    /// <param name="mapping">dictionary containing how different types get damaged.</param>
    /// <param name="type">the type whose damage is desired.</param>
    /// <returns>The damage, or null if could not be found.</returns>
    public static DamageObject GetDamage(Dictionary<Type, DamageObject> mapping, Type type)
    {
        Type current = type;
        DamageObject result = null;
        while (current != null && !mapping.TryGetValue(current, out result))
        {
            foreach (Type i in current.GetInterfaces())
            {
                if (mapping.TryGetValue(i, out result)) return result;
            }
            current = current.BaseType;
        }
        return result;
    }
}
