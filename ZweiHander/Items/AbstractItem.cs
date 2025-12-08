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

    public Vector2 Position { get; set; } = Vector2.Zero;

    public Vector2 Velocity { get; set; }

    public Vector2 Acceleration { get; set; }

    public Vector2 SpriteOffset { get; set; } = Vector2.Zero;

    public Vector2 Hitbox { get; set; } = Vector2.Zero;

    /// <summary>
    /// The lifetime (in seconds) left for item; negative means infinite.
    /// </summary>
    protected virtual double Life { get; set; } = -1f;

    /// <summary>
    /// Thresholds for switching phases; excludes spawn and death.
    /// </summary>
    protected virtual List<double> Phases { get; set; } = [];

    private int _phase = 0;

    /// <summary>
    /// Current phase, starting from 0.
    /// </summary>
    protected int Phase
    {
        get => _phase; set
        {
            _phase = value;
            OnPhaseChange();
        }
    }

    /// <summary>
    /// Handles the collisions for this item.
    /// </summary>
    public ItemCollisionHandler CollisionHandler { get; set; }

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
        if (itemConstructor.Phases.Count != 0) Phases = itemConstructor.Phases;
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
        ProgressLife(dt);
        if (IsDead()) return;
        // Movement
        if (!HasProperty(ItemProperty.Stationary)) Move(dt);
        // Face correct direction; UNTESTED
        if (HasProperty(ItemProperty.FacingVelocity)) Sprite.Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);

        Sprite.Update(time);
        CollisionHandler.UpdateCollisionBox();
    }

    public void Draw() { Sprite.Draw(Position + SpriteOffset); }

    /// <summary>
    /// Progresses this item's life.
    /// </summary>
    /// <param name="dt">Time that has passed.</param>
    protected void ProgressLife(float dt)
    {
        if (Life > 0) //Life can only progress if it is alive
        {
            Life -= dt;
            if (Life < 0)
            {
                Life = 0; //Negative life is infinite, which we do not want
                CollisionHandler.Dead = true;
            }
            // If there is threshold for this phase, and we are past that threshold, then progress the phase
            if (Phase < Phases.Count && Life <= Phases[Phase]) Phase++;
        }
    }

    /// <summary>
    /// Move this item.
    /// </summary>
    /// <param name="dt">Time that has passed.</param>
    protected void Move(float dt)
    {
        Velocity += dt * Acceleration;
        Position += dt * Velocity + (dt * dt / 2) * Acceleration;
    }

    protected virtual void OnPhaseChange() { }

    public void RemoveProperty(ItemProperty property) { Properties &= ~property; }

    public void AddProperty(ItemProperty property) { Properties |= property; }

    public bool HasProperty(ItemProperty property) { return Properties.HasFlag(property); }

    public void SetDamage(Type damaged, DamageObject damage) { Damage[damaged] = damage; }

    public DamageObject GetDamage(Type damaged) { return Damage[damaged]; }

    public bool IsDead()
    {
        return Life == 0;
    }

    public void Kill()
    {
        CollisionHandler.Dead = true;
        Life = 0;
    }

    public Rectangle GetHitBox()
    {
        int width = Hitbox.X == 0f ? Sprite.Width : (int)Hitbox.X;
        int height = Hitbox.Y == 0f ? Sprite.Height : (int)Hitbox.Y;
        // Sprites are centered
        return new Rectangle(
                (int)Position.X - width / 2,
                (int)Position.Y - height / 2,
                width,
                height
            );
    }

    public virtual void HandleCollision(ICollisionHandler other, CollisionInfo collisionInfo)
    {
        switch (other)
        {
            case PlayerCollisionHandler:
                if (HasProperty(ItemProperty.DeleteOnPlayer)) Kill();
                break;
            case BlockCollisionHandler:
                if (HasProperty(ItemProperty.DeleteOnBlock)) Kill();
                if (HasProperty(ItemProperty.BounceOnBlock)) Velocity *= -1;
                if (HasProperty(ItemProperty.StopOnBlock))
                {
                    Velocity = Vector2.Zero;
                    Acceleration = Vector2.Zero;
                }
                break;
            case ItemCollisionHandler otherItem:
                ItemInteract(otherItem, collisionInfo);
                break;
            case EnemyCollisionHandler otherEnemy:
                EnemyInteract(otherEnemy, collisionInfo);
                break;
        }
    }

    /// <summary>
    /// How to interact with other items on collision.
    /// </summary>
    /// <param name="other">What is being collided with.</param>
    /// <param name="collisionInfo">Info related to the collision.</param>
    protected virtual void ItemInteract(ItemCollisionHandler other, CollisionInfo collisionInfo) { }

    /// <summary>
    /// How to interact with enemies on collision.
    /// </summary>
    /// <param name="other">What is being collided with.</param>
    /// <param name="collisionInfo">Info related to the collision.</param>
    protected virtual void EnemyInteract(EnemyCollisionHandler other, CollisionInfo collisionInfo)
    {
        if (HasProperty(ItemProperty.DeleteOnEnemy)) Kill();
    }
}