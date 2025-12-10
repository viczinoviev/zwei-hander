using System;

namespace ZweiHander.Damage;

/// <summary>
/// All effects, also called status effects
/// </summary>
public enum Effect : Int16
{
    None = 0,
    /// <summary>
    /// Damage over time
    /// </summary>
    OnFire = 0x1,
    /// <summary>
    /// Decreased movement speed
    /// </summary>
    Slowed = 0x2,
    /// <summary>
    /// Healing over time
    /// </summary>
    Regen = 0x4,
    /// <summary>
    /// Increased damage
    /// </summary>
    Strength = 0x8,
    /// <summary>
    /// Increased movement speed
    /// </summary>
    Speed = 0x10
}