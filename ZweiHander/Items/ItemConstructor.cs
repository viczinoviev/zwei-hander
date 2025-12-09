using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items;

/// <summary>
/// MEANT ONLY FOR USE WITHIN ITEMS THEMSELVES.<br></br>
/// Allows for adding parameters to item constructors without editing all class constructors.
/// </summary>
public class ItemConstructor
{
    /// <summary>
    /// The manager this item is stored in.
    /// </summary>
    public ItemManager Manager { get; set; }

    /// <summary>
    /// The lifetime (in seconds) left for item; negative means infinite.
    /// </summary>
    public double Life { get; set; }

    /// <summary>
    /// Current position using xy coordinate system
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Current velocity using xy coordinate system
    /// </summary>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// Current acceleration using xy coordinate system
    /// </summary>
    public Vector2 Acceleration { get; set; }

    /// <summary>
    /// Holds all the sprites for treasures.
    /// </summary>
    public TreasureSprites TreasureSprites { get; set; }

    /// <summary>
    /// Holds all the sprites for items.
    /// </summary>
    public ItemSprites ItemSprites { get; set; }

    /// <summary>
    /// Holds all the sprites for bosses.
    /// </summary>
    public BossSprites BossSprites { get; set; }

    /// <summary>
    /// Whether to use the default properties or not
    /// </summary>
    public bool UseDefaultProperties { get; set; }

    /// <summary>
    /// Additional properties to give this item
    /// </summary>
    public ItemProperty AdditionalProperties { get; set; }

    /// <summary>
    /// Thresholds for switching phases; excludes spawn and death
    /// </summary>
    public List<double> Phases { get; set; }

    /// <summary>
    /// How to damage different object types.
    /// </summary>
    public Dictionary<Type, DamageObject> Damage { get; set; }

    /// <summary>
    /// Any extra parameters needed for that item; use class summary as reference
    /// </summary>
    public List<object> Extras { get; set; }
}