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
    protected double _life;

    /// <summary>
    /// The time (in seconds) to spend dying.
    /// </summary>
    protected double _deathTime = 0.00001;

    /// <summary>
    /// Handles the collisions for this item.
    /// </summary>
    protected ItemCollisionHandler _collisionHandler;

    /// <summary>
    /// The properties this item has.
    /// </summary>
    protected HashSet<ItemProperty> Properties = [];

    public AbstractItem(ItemConstructor itemConstructor)
    {
        _sprites = itemConstructor.Sprites;
        _manager = itemConstructor.Manager;
        _itemType = itemConstructor.ItemType;
        _life = itemConstructor.Life;
        _collisionHandler = new ItemCollisionHandler(this);
    }

    public virtual void Update(GameTime time)
    {
        float dt = (float)time.ElapsedGameTime.TotalSeconds;

        // Life progression
        if (_life > 0)
        {
            _life -= dt;
            if (_life < 0)
            {
                _life = 0;
            }
        } else
        {
            OnDeath(time);
            return;
        }

        // Movement
        if (!Properties.Contains(ItemProperty.Stationary))
        {
            Velocity += dt * Acceleration;
            Position += dt * Velocity + (dt * dt / 2) * Acceleration;
        }

        // Face correct direction; UNTESTED
        if (Properties.Contains(ItemProperty.FacingVelocity))
        {
            Sprite.Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
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

    public bool HasProperty(ItemProperty property)
    {
        return Properties.Contains(property);
    }

    public virtual void OnDeath(GameTime gameTime)
    {
        _deathTime -= gameTime.ElapsedGameTime.TotalSeconds;
        if (IsDead())
        {
            _manager.ItemTypeCount[_itemType]--; 
        }
    }

    public bool IsDead()
    {
        return _deathTime <= 0;
    }

    public void Kill()
    {
        _life = 0;
        _deathTime = 0;
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
                if (Properties.Contains(ItemProperty.CanBePickedUp))
                {
                    Kill();
                }
                if (Properties.Contains(ItemProperty.DeleteOnPlayer))
                {
                    Kill();
                }
                break;
            case BlockCollisionHandler:
                if (Properties.Contains(ItemProperty.DeleteOnBlock))
                {
                    Kill();
                }
                if (Properties.Contains(ItemProperty.BounceOnBlock))
                {
                    Velocity *= -1;
                }
                if (Properties.Contains(ItemProperty.StopOnBlock))
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