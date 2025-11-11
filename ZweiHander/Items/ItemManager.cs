//TODO: Dictionary for itemCount, itemCollision

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items.ItemStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Items;

/// <summary>
/// Used to create items. Holds all items it creates, and can do collective actions on these items.
/// </summary>
public class ItemManager
{
    /// <summary>
    /// Holds all the items made by this factory.
    /// </summary>
    private readonly HashSet<IItem> _items = [];

    /// <summary>
    /// Holds all the sprites for items.
    /// </summary>
    private readonly ItemSprites _itemSprites;

    /// <summary>
    /// Holds all the sprites for treasures.
    /// </summary>
    private readonly TreasureSprites _treasureSprites;

    /// <summary>
    /// Holds all the sprites related to bosses.
    /// </summary>
    private readonly BossSprites _bossSprites;

    /// <summary>
    /// Count of each item type in this; -1 is infinite.
    /// </summary>
    private Dictionary<Type, int> ItemTypeCount { get; } = [];

    /// <summary>
    /// Number of items stored in this manager.
    /// </summary>
    public int ItemCount { get => _items.Count; }

    public ItemManager(ItemSprites itemSprites, TreasureSprites treasureSprites, BossSprites bossSprites = null)
    {
        _itemSprites = itemSprites;
        _treasureSprites = treasureSprites;
        _bossSprites = bossSprites;
    }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="life">Lifetime (in seconds) for item; 0 will use default for type; -1 is infinite.</param>
    /// <param name="position">The item's starting position.</param>
    /// <param name="velocity">The item's starting velocity.</param>
    /// <param name="acceleration">The item's starting acceleration.</param>
    /// <param name="properties">Additional Properties attached to this instance.</param>
    /// <param name="useDefaultProperties">Whether to use the default properties for this item.</param>
    /// <param name="phases">Thresholds for switching phases; excludes spawn and death.</param>
    /// <param name="extras">Any extra parameters needed for that item; use class summary as reference.</param>
    /// <returns>The desired item.</returns>
    public ItemType GetItem<ItemType> (
        double life = 0f,
        Vector2 position = default,
        Vector2 velocity = default,
        Vector2 acceleration = default,
        ICollection<ItemProperty> properties = null,
        bool useDefaultProperties = true,
        List<double> phases = null,
        List<object> extras = null
    ) where ItemType : IItem
    {
        ItemProperty Properties = 0x0;
        if (properties != null)
        {
            foreach (ItemProperty property in properties)
            {
                Properties |= property;
            }
        }
        ItemConstructor itemConstructor = new()
        {
            Manager = this,
            Life = life,
            Position = position,
            Velocity = velocity,
            Acceleration = acceleration,
            AdditionalProperties = Properties,
            UseDefaultProperties = useDefaultProperties,
            BossSprites = _bossSprites,
            ItemSprites = _itemSprites,
            TreasureSprites = _treasureSprites,
            Phases = phases ?? [],
            Extras = extras ?? []
        };

        Type type = typeof(ItemType);
        ItemType item = (ItemType) Activator.CreateInstance(type, itemConstructor);
        
        _items.Add(item);
        if (ItemTypeCount.TryGetValue(type, out int value)) ItemTypeCount[type] = ++value;
        else ItemTypeCount[type] = 1;
            return item;
    }

    /// <summary>
    /// Updates all items this manager contains.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime)
    {
        foreach (IItem item in _items)
        {
            item.Update(gameTime); // Do each item's update
        }
        IEnumerable<IItem> DeadItems = _items.Where(item => item.IsDead());
        foreach (IItem item in DeadItems)
        {
            ItemTypeCount[item.ItemType]--;
            _items.Remove(item);
        }
    }

    /// <summary>
    /// Draws all items on this manager on screen.
    /// </summary>
    public void Draw()
    {
        foreach (IItem item in _items)
        {
            item.Draw();
        }
    }

    /// <summary>
    /// Creates multiple copies of a new item.
    /// </summary>
    /// <param name="count">Number of copies to make. If infinite, given by -1, does not make any.</param>
    /// <param name="life">Lifetime (in seconds) for item; 0 will use default for type; -1 is infinite.</param>
    /// <param name="position">The item's starting position.</param>
    /// <param name="velocity">The item's starting velocity.</param>
    /// <param name="acceleration">The item's starting acceleration.</param>
    /// <param name="properties">Additional Properties attached to this instance.</param>
    /// <param name="useDefaultProperties">Whether to use the default properties for this item.</param>
    /// <param name="phases">Thresholds for switching phases; excludes spawn and death.</param>
    /// <param name="extras">Any extra parameters needed for that item; use class summary as reference.</param>
    public void MassProduce<ItemType>(
        int count,
        double life = 0f,
        Vector2 position = default,
        Vector2 velocity = default,
        Vector2 acceleration = default,
        ICollection<ItemProperty> properties = null,
        bool useDefaultProperties = true,
        List<double> phases = null,
        List<object> extras = null
    ) where ItemType : IItem
    {
        if(count >= 0)
        {
            for (int i = 0; i < count; i++)
            {
                // TODO: Figure out better way to do this using deepcopying
                GetItem<ItemType>(life, position, velocity, acceleration, properties, useDefaultProperties, phases, extras);
            }
        }
        else
        {
            ItemTypeCount[typeof(ItemType)] = -1;
        }
    }

    /// <summary>
    /// Remove all items from this manager.
    /// </summary>
    public void Clear()
    {
        foreach (IItem item in _items)
        {
            if (item is AbstractItem abstractItem)
            {
                abstractItem.UnsubscribeFromCollisions();
            }
        }
        _items.Clear();
        ItemTypeCount.Clear();
    }

    /// <summary>
    /// Provides the amount of an item. -1 is inifinite.
    /// </summary>
    /// <param name="itemType">Type of desired item to get count of.</param>
    /// <returns>Amount of desired item.</returns>
    public int CountOf(Type itemType)
    {
        return ItemTypeCount.TryGetValue(itemType, out int value) ? value : 0;
    }
}