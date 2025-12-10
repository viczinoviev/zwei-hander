using System;
using System.Collections.Generic;

namespace ZweiHander.Damage;

public class DamageDict : Dictionary<Type, DamageObject>
{
    /// <summary>
    /// Adds the specified damage to this dictionary.
    /// </summary>
    /// <typeparam name="T">Type this damage applies to.</typeparam>
    /// <param name="damage">Specified damage.</param>
    public DamageDict Add<T>(DamageObject damage)
    {
        this[typeof(T)] = damage;
        return this;
    }

    /// <summary>
    /// Removes the damage from this dictionary for specified type.
    /// </summary>
    /// <typeparam name="T">Type this damage applies to.</typeparam>
    /// <returns>If damage was sucessfully removed.</returns>
    public bool Remove<T>() { return Remove(typeof(T)); }

    /// <summary>
    /// Gets the damage for a specified type using this dictionary.
    /// Starts at most derived type, and works up, checking interfaces at each level.
    /// </summary>
    /// <param name="type">the type whose damage is desired.</param>
    /// <returns>The damage, or null if could not be found.</returns>
    public DamageObject GetDamage(Type type) { return DamageObject.GetDamage(this, type); }

    /// <summary>
    /// Gets the damage for a specified type using this dictionary.
    /// Starts at most derived type, and works up, checking interfaces at each level.
    /// </summary>
    /// <typeparam name="T">the type whose damage is desired.</typeparam>
    /// <returns>The damage, or null if could not be found.</returns>
    public DamageObject GetDamage<T>() { return GetDamage(typeof(T)); }
}
