using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items.ItemStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Items;

/// <summary>
/// Creates items, stores them, and updates stored items.
/// </summary>
public class ItemManager
{
    /// <summary>
    /// Holds all the items made by this factory.
    /// </summary>
    private readonly HashSet<IItem> _items = [];

    /// <summary>
    /// Holds all the sprites
    /// </summary>
    private readonly ItemSprites _sprites;

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
    public IItem GetItem(
        ItemType itemType, 
        double life = 0f, 
        ICollection<ItemProperty> properties = null, 
        Vector2 position = default, 
        Vector2 velocity = default, 
        Vector2 acceleration = default
    )
    {
        bool UseDefaultProperties = properties == null;
        IItem item = null;
        switch (itemType)
        {
            case ItemType.Compass:
                item = new CompassItem(null, UseDefaultProperties);
                item.Life = life == 0 ? -1 : life; 
                break;
            case ItemType.Map:
                item = new MapItem(null, UseDefaultProperties);
                item.Life = life == 0 ? -1 : life;
                break;
            case ItemType.Key:
                item = new KeyItem(null, UseDefaultProperties);
                item.Life = life == 0 ? -1 : life;
                break;
            case ItemType.Heart:
                item = new HeartItem(null, UseDefaultProperties);
                item.Life = life == 0 ? -1 : life;
                break;
            case ItemType.Boomerang:
                item = new BoomerangItem(_sprites.Boomerang(), UseDefaultProperties);
                item.Life = life == 0 ? 3 : life;
                break;
            case ItemType.Arrow:
                item = new ArrowItem(_sprites.ArrowRight(), UseDefaultProperties);
                item.Life = life == 0 ? 2 : life;
                break;
            default:
                // Should never actually reach here - will error out if so
                break;
        }
        item.Position = position;
        item.Velocity = velocity;
        item.Acceleration = acceleration;
        if (!UseDefaultProperties)
        {
            foreach (ItemProperty property in properties)
            {
                item.AddProperty(property);
            }

        }
        _items.Add(item);
        return item;
    }

    /// <summary>
    /// Updates all items this manager contains.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime)
    {
        foreach(IItem item in _items)
        {
            item.Update(gameTime); // Do each item's update
            if(item.Life == 0)
            {
                item.OnDeath(); // Do item's death behavior if died
            }
        }
        _items.RemoveWhere(item => item.Life == 0); // Remove any dead items
    }
}