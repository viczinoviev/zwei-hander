using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
    /// The sprites associated with this item.
    /// </summary>
    protected List<ISprite> _sprites;

    /// <summary>
    /// Current sprite index.
    /// </summary>
    protected int _spriteIndex = 0;

    /// <summary>
    /// Number of sprites this item has.
    /// </summary>
    protected int SpriteCount { get => _sprites.Count; }

    /// <summary>
    /// The current sprite.
    /// </summary>
    protected ISprite Sprite { get => _sprites[_spriteIndex]; }

    public Vector2 Position { get; set; } = default;

    public Vector2 Velocity { get; set; } = default;

    public Vector2 Acceleration { get; set; } = default;

    public double Life { get; set; }

    public double DeathTime { get; protected set; } = 0.00001;

    /// <summary>
    /// The properties this item has.
    /// </summary>
    protected HashSet<ItemProperty> Properties = [];


    protected AbstractItem(List<ISprite> sprites)
    {
        _sprites = sprites;
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

        // Face correct direction
        if (Properties.Contains(ItemProperty.FacingVelocity))
        {
            // NOT USED RIGHT NOW
            // I am not sure exactly what it would be because of the grid orientation
            // But I think one of these two:
            // Standard coordinate grid, standard unit circle: Math.Atan2(Velocity.Y, Velocity.X)
            // Pixel coordinate grid, standard unit rcircle: Math.Atan2(-Velocity.Y, Velocity.X)?
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

        Sprite.Update(time);
    }

    public void Draw()
    {
        Sprite.Draw(Position);
    }

    public void RemoveProperty(ItemProperty property)
    {
        Properties.Remove(property);
    }

    public void AddProperty(ItemProperty property)
    {
        Properties.Add(property);
    }

    public virtual void OnDeath(GameTime gameTime)
    {
        DeathTime -= gameTime.ElapsedGameTime.TotalSeconds;
    }
}