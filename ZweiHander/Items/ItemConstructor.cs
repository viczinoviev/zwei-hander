using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;

namespace ZweiHander.Items;

/// <summary>
/// MEANT ONLY FOR USE WITHIN ITEMS THEMSELVES.<br></br>
/// Allows for adding parameters to item constructors without editing all class constructors.
/// </summary>
public class ItemConstructor
{
    /// <summary>
    /// The sprites associated with this item.
    /// </summary>
    public List<ISprite> Sprites { get; set; }

    /// <summary>
    /// The manager this item is stored in.
    /// </summary>
    public ItemManager Manager { get; set; }

    /// <summary>
    /// The lifetime (in seconds) left for item; negative means infinite.
    /// </summary>
    public double Life { get; set; }

    /// <summary>
    /// What type of item this is.
    /// </summary>
    public ItemType ItemType { get; set; }
}