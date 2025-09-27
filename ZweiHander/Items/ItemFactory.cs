using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZweiHander.Items.ItemStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Items;

/// <summary>
/// Manages creations of items.
/// </summary>
public class ItemFactory
{
    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="itemType">What type of item to get.</param>
    /// <param name="position">The item's starting position.</param>
    /// <param name="velocity">The item's starting velocity.</param>
    /// <param name="acceleration">The item's starting acceleration.</param>
    /// <returns>The desired item.</returns>
    public IItem GetItem(ItemType itemType, ICollection<ItemProperty> properties, Vector2 position = default, Vector2 velocity = default, Vector2 acceleration = default)
    {
        IItem item = null;
        switch (itemType)
        {
            case ItemType.Compass:
                item = new CompassItem();
                break;
            case ItemType.Map:
                item = new MapItem();
                break;
            case ItemType.Key:
                item = new KeyItem();
                break;
            case ItemType.Heart:
                item = new HeartItem();
                break;
            default:
                // Should never actually reach here - will error out if so
                break;
        }
        item.Position = position;
        item.Velocity = velocity;
        item.Acceleration = acceleration;
        foreach(ItemProperty property in properties)
        {
            item.AddProperty(property);
        }
        return item;
    }

}