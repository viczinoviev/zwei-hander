using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    protected List<ISprite> Sprites { get; set; }

    /// <summary>
    /// Current sprite index.
    /// </summary>
    protected int SpriteIndex { get; set; } = 0;

    /// <summary>
    /// Number of sprites this item has.
    /// </summary>
    protected int SpriteCount { get => Sprites.Count; }

    /// <summary>
    /// The manager this item is stored in.
    /// </summary>
    protected ItemManager _manager;

    /// <summary>
    /// The type of item this is.
    /// </summary>
    public Type ItemType { get => this.GetType(); }

    /// <summary>
    /// The current sprite.
    /// </summary>
    protected ISprite Sprite { get => Sprites[SpriteIndex]; }

    public Vector2 Position { get; set; }= Vector2.Zero;

    public Vector2 Velocity { get; set; }

    public Vector2 Acceleration { get; set; }

    /// <summary>
    /// The lifetime (in seconds) left for item; negative means infinite.
    /// </summary>
    protected virtual double Life { get; set; } = -1f;

    /// <summary>
    /// The time (in seconds) to spend dying.
    /// </summary>
    protected virtual double DeathTime { get; set; } = 0.01;

    /// <summary>
    /// Handles the collisions for this item.
    /// </summary>
    protected ItemCollisionHandler CollisionHandler { get; set; }

    /// <summary>
    /// The properties this item has.
    /// </summary>
    protected virtual ItemProperty Properties { get; set; } = 0x0;

    /// <summary>
    /// How to damage different object types.
    /// </summary>
    protected Dictionary<Type, DamageObject> Damage { get; set; } = [];

    public AbstractItem(ItemConstructor itemConstructor)
    {
        _manager = itemConstructor.Manager;
        if (itemConstructor.Life != 0) Life = itemConstructor.Life;
        if (itemConstructor.DeathTime != 0) DeathTime = itemConstructor.DeathTime;
        Position = itemConstructor.Position;
        Velocity = itemConstructor.Velocity;
        Acceleration = itemConstructor.Acceleration;
        if (itemConstructor.UseDefaultProperties) Properties |= itemConstructor.AdditionalProperties;
        else Properties = itemConstructor.AdditionalProperties;
    }

    /// <summary>
    /// Final step in each item's constructor.
    /// </summary>
    /// <param name="itemConstructor"></param>
    protected void Setup(ItemConstructor itemConstructor)
    {
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
        Properties |= property;
    }

    public void AddProperty(ItemProperty property)
    {
        Properties |= property;
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
            CollisionHandler.Dead = true;
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
            case EnemyCollisionHandler:
                if (HasProperty(ItemProperty.DeleteOnEnemy))
                {
                    Kill();
                }
                break;
        }
    }
}