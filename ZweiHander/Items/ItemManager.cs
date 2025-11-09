//TODO: Dictionary for itemCount, itemCollision

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
    private Dictionary<ItemType, int> ItemTypeCount { get; } = [];

    /// <summary>
    /// Number of items stored in this manager.
    /// </summary>
    public int ItemCount { get => _items.Count; }

    public ItemManager(ItemSprites itemSprites, TreasureSprites treasureSprites, BossSprites bossSprites = null)
    {
        _itemSprites = itemSprites;
        _treasureSprites = treasureSprites;
        _bossSprites = bossSprites;
        foreach(ItemType item in Enum.GetValues(typeof(ItemType)))
        {
            ItemTypeCount.Add(item, 0);
        }
    }

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
        ItemConstructor itemConstructor = new()
        {
            Manager = this,
            ItemType = itemType
        };
        switch (itemType)
        {
            case ItemType.Compass:
                itemConstructor.Sprites = [_treasureSprites.Compass()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new CompassItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Map:
                itemConstructor.Sprites = [_treasureSprites.Map()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new MapItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Key:
                itemConstructor.Sprites = [_treasureSprites.Key()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new KeyItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Heart:
                itemConstructor.Sprites = [_treasureSprites.Heart()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new HeartItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Boomerang:
                itemConstructor.Sprites = [_itemSprites.Boomerang()];
                itemConstructor.Life = life == 0 ? 3 : life;
                item = new BoomerangItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Arrow:
                itemConstructor.Sprites = [_itemSprites.Arrow(velocity), _itemSprites.ProjectileOnHit()];
                itemConstructor.Life = life == 0 ? 2 : life;
                item = new ArrowItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Sword:
                itemConstructor.Sprites = [_itemSprites.SwordProjectile(velocity), _itemSprites.SwordProjectileEffect(velocity)];
                itemConstructor.Life = life == 0 ? 2 : life;
                item = new SwordItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.HeartContainer:
                itemConstructor.Sprites = [_treasureSprites.HeartContainer()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new HeartContainerItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Rupy:
                itemConstructor.Sprites = [_treasureSprites.Rupy()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new RupyItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Fairy:
                itemConstructor.Sprites = [_treasureSprites.Fairy()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new FairyItem(itemConstructor, UseDefaultProperties, position);
                break;
            case ItemType.Bomb:
                itemConstructor.Sprites = [_itemSprites.Bomb(), _itemSprites.Explosion()];
                itemConstructor.Life = life == 0 ? 2 : life;
                item = new BombItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.BlueCandle:
                itemConstructor.Sprites = [_treasureSprites.CandleBlue()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new BlueCandleItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.BluePotion:
                itemConstructor.Sprites = [_treasureSprites.PotionBlue()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new BluePotionItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Bow:
                itemConstructor.Sprites = [_treasureSprites.Bow()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new BowItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Clock:
                itemConstructor.Sprites = [_treasureSprites.Clock()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new ClockItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.TriforcePiece:
                itemConstructor.Sprites = [_treasureSprites.Triforce()];
                itemConstructor.Life = life == 0 ? -1 : life;
                item = new TriforceItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Fireball:
                itemConstructor.Sprites = [_bossSprites.AquamentusProjectile()];
                itemConstructor.Life = life == 0 ? 2 : life;
                item = new FireballItem(itemConstructor, UseDefaultProperties);
                break;
            case ItemType.Fire:
                itemConstructor.Sprites = [_itemSprites.FireProjectile()];
                itemConstructor.Life = life == 0 ? 2.5 : life;
                item = new FireItem(itemConstructor, UseDefaultProperties);
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
        ItemTypeCount[itemType]++;
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
    /// <param name="itemType">What type of item to get.</param>
    /// <param name="count">Number of copies to make. If infinite, given by -1, does not make any.</param>
    /// <param name="life">Lifetime (in seconds) for item; 0 will use default for type; -1 is infinite.</param>
    /// <param name="position">The item's starting position.</param>
    /// <param name="velocity">The item's starting velocity.</param>
    /// <param name="acceleration">The item's starting acceleration.</param>
    /// <param name="properties">Properties attached to this instance; will use default properties for type if not given.</param>
    public void MassProduce(
        ItemType itemType,
        int count,
        double life = 0f,
        ICollection<ItemProperty> properties = null,
        Vector2 position = default,
        Vector2 velocity = default,
        Vector2 acceleration = default
    )
    {
        if(count >= 0)
        {
            for (int i = 0; i < count; i++)
            {
                // TODO: Figure out better way to do this using deepcopying
                GetItem(itemType, life, properties, position, velocity, acceleration);
            }
        }
        else
        {
            ItemTypeCount[itemType] = -1;
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
        foreach(ItemType item in ItemTypeCount.Keys)
        {
            ItemTypeCount[item] = 0;
        }
    }

    /// <summary>
    /// Provides the amount of an item. -1 is inifinite.
    /// </summary>
    /// <param name="itemType">Type of desired item to get count of.</param>
    /// <returns>Amount of desired item.</returns>
    public int CountOf(ItemType itemType)
    {
        return ItemTypeCount[itemType];
    }
}