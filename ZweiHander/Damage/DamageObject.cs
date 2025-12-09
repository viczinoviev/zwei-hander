using System;
using System.Collections.Generic;

namespace ZweiHander.Damage;

/// <summary>
/// What to do when taking damage
/// </summary>
public class DamageObject(int damage = 0, double knockback = 0)
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
