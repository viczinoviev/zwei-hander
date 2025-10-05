using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Vector2 = Microsoft.Xna.Framework.Vector2;

using ZweiHander.Graphics;
using System.Collections.Generic;

namespace ZweiHander.Items;

/// <summary>
/// All Item classes implement this base class.
/// </summary>
public abstract class AbstractItem : IItem
{
    /// <summary>
    /// The sprite associated with this item.
    /// </summary>
    protected ISprite _sprite;

    public Vector2 Position { get; set; } = default;

    public Vector2 Velocity { get; set; } = default;

    public Vector2 Acceleration { get; set; } = default;

    public double Life { get; set; }

    /// <summary>
    /// The properties this item has.
    /// </summary>
    protected HashSet<ItemProperty> Properties = [];


    protected AbstractItem(ISprite sprite)
    {
        _sprite = sprite;
    }


    public virtual void Update(GameTime time)
    {
        float dt = (float)time.ElapsedGameTime.TotalSeconds;

        // Movement
        if (!Properties.Contains(ItemProperty.Stationary))
        {
            Velocity += dt * Acceleration;
            Position += dt * Velocity + (dt * dt / 2) * Acceleration;
        }

        // Life progression
        if (Life > 0)
        {
            Life -= dt;
            if (Life < 0)
            {
                Life = 0;
            }
        }

        _sprite.Update(time);
    }

    public void Draw()
    {
        _sprite.Draw(Position);
    }

    public void RemoveProperty(ItemProperty property)
    {
        Properties.Remove(property);
    }

    public void AddProperty(ItemProperty property)
    {
        Properties.Add(property);
    }

    public virtual void OnDeath()
    {
        // No default
    }
}