using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

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
    /// The manager this item is stored in.
    /// </summary>
    protected ItemManager _manager;

    /// <summary>
    /// What type of item this is.
    /// </summary>
    protected ItemType _itemType;

    public ItemType ItemType { get => _itemType; }

    /// <summary>
    /// The current sprite.
    /// </summary>
    protected ISprite Sprite { get => _sprites[_spriteIndex]; }

    public Vector2 Position { get; set; } = default;

    public Vector2 Velocity { get; set; } = default;

    public Vector2 Acceleration { get; set; } = default;

    /// <summary>
    /// The lifetime (in seconds) left for item; negative means infinite.
    /// </summary>
    protected double Life { get; set; }

    /// <summary>
    /// The time (in seconds) to spend dying.
    /// </summary>
    protected double DeathTime { get; set; } = 0.00001;

    /// <summary>
    /// Handles the collisions for this item.
    /// </summary>
    protected ItemCollisionHandler CollisionHandler { get; set; }

    /// <summary>
    /// The properties this item has.
    /// </summary>
    protected ItemProperty Properties { get; set; } = 0x0;

    /// <summary>
    /// How to damage different object types.
    /// </summary>
    protected Dictionary<Type, DamageObject> Damage { get; set; } = [];

    public AbstractItem(ItemConstructor itemConstructor)
    {
        _sprites = itemConstructor.Sprites;
        _manager = itemConstructor.Manager;
        _itemType = itemConstructor.ItemType;
        Life = itemConstructor.Life;
        CollisionHandler = new ItemCollisionHandler(this);
    }

    public virtual void Update(GameTime time)
    {
        float dt = (float)time.ElapsedGameTime.TotalSeconds;

        // Life progression
        if (Life > 0)
        {
            Life -= dt;
            if (Life < 0)
            {
                Life = 0;
            }
        } else
        {
            OnDeath(time);
            return;
        }

        // Movement
        if (!HasProperty(ItemProperty.Stationary))
        {
            Velocity += dt * Acceleration;
            Position += dt * Velocity + (dt * dt / 2) * Acceleration;
        }

        // Face correct direction; UNTESTED
        if (HasProperty(ItemProperty.FacingVelocity))
        {
            Sprite.Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
        }
        Sprite.Update(time);
        CollisionHandler.UpdateCollisionBox();
    }

    public void Draw()
    {
        Sprite.Draw(Position);
    }

    public void RemoveProperty(ItemProperty property)
    {
        Properties = Properties | property;
    }

    public void AddProperty(ItemProperty property)
    {
        Properties = Properties | property;
    }

    public bool HasProperty(ItemProperty property)
    {
        return Properties.HasFlag(property);
    }

    public void SetDamage(Type damaged, DamageObject damage)
    {
        Damage[damaged] = damage;
    }

    public DamageObject GetDamage(Type damaged)
    {
        return Damage[damaged];
    }

    public virtual void OnDeath(GameTime gameTime)
    {
        DeathTime -= gameTime.ElapsedGameTime.TotalSeconds;
        if (IsDead())
        {
            _manager.ItemTypeCount[_itemType]--; 
        }
    }

    public bool IsDead()
    {
        return DeathTime <= 0;
    }

    public void Kill()
    {
        Life = 0;
        DeathTime = 0;
    }

    public Rectangle GetHitBox()
    {
        return new Rectangle(
                (int) Position.X,
                (int)Position.Y,
                Sprite.Width,
                Sprite.Height
            );
    }

    public virtual void HandleCollision(ICollisionHandler other, CollisionInfo collisionInfo)
    {
        switch (other)
        {
            case PlayerCollisionHandler:
                if (HasProperty(ItemProperty.CanBePickedUp))
                {
                    Kill();
                }
                if (HasProperty(ItemProperty.DeleteOnPlayer))
                {
                    Kill();
                }
                break;
            case BlockCollisionHandler:
                if (HasProperty(ItemProperty.DeleteOnBlock))
                {
                    Kill();
                }
                if (HasProperty(ItemProperty.BounceOnBlock))
                {
                    Velocity *= -1;
                }
                if (HasProperty(ItemProperty.StopOnBlock))
                {
                    Velocity = Vector2.Zero;
                    Acceleration = Vector2.Zero;
                }
                break;
            case ItemCollisionHandler:
                break;
        }
    }
}