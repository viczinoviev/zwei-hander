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
    /// <param name="life">Lifetime (in seconds) for item; 0 will use default for type; -1 is infinite.</param>
    /// <param name="position">The item's starting position.</param>
    /// <param name="velocity">The item's starting velocity.</param>
    /// <param name="acceleration">The item's starting acceleration.</param>
    /// <param name="properties">Properties attached to this instance; will use default properties for type if not given.</param>
    /// <returns>The desired item.</returns>
    public IItem GetItem(ItemType itemType, double life = 0f, ICollection<ItemProperty> properties = null, Vector2 position = default, Vector2 velocity = default, Vector2 acceleration = default)
    {
        IItem item = null;
        switch (itemType)
        {
            case ItemType.Compass:
                item = new CompassItem();
                if (properties == null)
                {
                    item.AddProperty(ItemProperty.Stationary); //Put this as defaults into constructor
                    item.AddProperty(ItemProperty.DeleteOnCollision);
                    item.AddProperty(ItemProperty.CanBePickedUp);
                }
                break;
            case ItemType.Map:
                item = new MapItem();
                if (properties == null)
                {
                    item.AddProperty(ItemProperty.Stationary);
                    item.AddProperty(ItemProperty.DeleteOnCollision);
                    item.AddProperty(ItemProperty.CanBePickedUp);
                }
                break;
            case ItemType.Key:
                item = new KeyItem();
                if (properties == null)
                {
                    item.AddProperty(ItemProperty.Stationary);
                    item.AddProperty(ItemProperty.DeleteOnCollision);
                    item.AddProperty(ItemProperty.CanBePickedUp);
                }
                break;
            case ItemType.Heart:
                item = new HeartItem();
                if (properties == null)
                {
                    item.AddProperty(ItemProperty.Stationary);
                    item.AddProperty(ItemProperty.DeleteOnCollision);
                    item.AddProperty(ItemProperty.CanBePickedUp);
                }
                break;
            default:
                // Should never actually reach here - will error out if so
                break;
        }
        item.Position = position;
        item.Velocity = velocity;
        item.Acceleration = acceleration;
        if (properties != null)
        {
            foreach (ItemProperty property in properties)
            {
                item.AddProperty(property);
            }

        }
        return item;
    }

}