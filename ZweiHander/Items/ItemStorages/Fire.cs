using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Items.ItemStorages;

/// <summary>
/// Give infinite life for collectable<br></br>
/// 6s life, animation<br></br>
/// Phase 0: Move out, slowing down; no life loss
/// Phase 1: Choose enemy
/// Phase 1: Home in on chosen enemy
/// EXTRAS: (double HomingAcceleration = |Acceleration|)
/// </summary>
public class Fire : AbstractItem
{
    protected override double Life { get; set; } = 6f;

    protected override ItemProperty Properties { get; set; } = ItemProperty.DeleteOnBlock;

    /// <summary>
    /// Sign of Fire velocity, for seeing when to switch phase
    /// </summary>
    protected Vector2 Signs { get; set; } = Vector2.Zero;

    /// <summary>
    /// "Acceleration" for homing to enemy
    /// </summary>
    protected double HomingAcceleration { get; set; } = 0f;

    /// <summary>
    /// Speed for homing to enemy
    /// </summary>
    protected double HomingSpeed { get; set; } = 0f;

    /// <summary>
    /// Locates closest enemy
    /// </summary>
    protected double HomingDistanceSquared { get; set; } = 9e20;

    /// <summary>
    /// Reference to HomingPosition
    /// </summary>
    protected Func<Vector2> HomingPositon { get; set; }

    /// <summary>
    /// Collision Handler for homing
    /// </summary>
    protected ICollisionHandler Homing { get; set; } = null;

    public Fire(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.FireProjectile()];
        if (Life < 0) AddProperty(ItemProperty.Collectable);
        else
        {
            if (itemConstructor.Extras.Count > 0)
            {
                HomingAcceleration = (double)itemConstructor.Extras[0];
            }
            else
            {
                HomingSpeed = Acceleration.Length();
            }
            AddProperty(ItemProperty.CanDamageEnemy);
        }
        Setup(itemConstructor);
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        base.Update(gameTime);
        if (Phase == 0)
        {
            if (Life > 0)
            {
                if (Signs == Vector2.Zero)
                {
                    Signs = new(Math.Sign(Velocity.X), Math.Sign(Velocity.Y));
                }
                if (HomingAcceleration <= 0)
                {
                    HomingAcceleration = Acceleration.Length();
                }
                if (Math.Sign(Velocity.X) != Signs.X || Math.Sign(Velocity.Y) != Signs.Y)
                {
                    Phase++;
                    OnPhaseChange();
                }
                Life += dt;
            }
        }
        else if (Phase == 1)
        {
            if (Homing != null)
            {
                Phase++;
                OnPhaseChange();
            }
        }
        else if (Phase == 2)
        {
            HomingSpeed += HomingAcceleration * dt;
            Vector2 difference = HomingPositon() - Position;
            Velocity = (float)HomingSpeed * difference / difference.Length();
        }
    }

    public override void OnPhaseChange()
    {
        if (Phase == 1)
        {
            Hitbox = new(300, 300);
            RemoveProperty(ItemProperty.CanDamageEnemy);
            RemoveProperty(ItemProperty.DeleteOnBlock);
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
        } 
        else if (Phase == 2)
        {
            Hitbox = Vector2.Zero;
            AddProperty(ItemProperty.CanDamageEnemy);
            CollisionHandler.UpdateCollisionBox();
        }
    }

    protected override void EnemyInteract(EnemyCollisionHandler other, CollisionInfo collisionInfo)
    {
        base.EnemyInteract(other, collisionInfo);
        if (Phase == 1)
        {
            double distanceSquared = (other._enemy.Position - Position).LengthSquared();
            if (distanceSquared < HomingDistanceSquared)
            {
                Homing = other;
                HomingPositon = () => other._enemy.Position;
                HomingDistanceSquared = distanceSquared;
            }
        }
    }

    public override void HandleCollision(ICollisionHandler other, CollisionInfo collisionInfo)
    {
        base.HandleCollision(other, collisionInfo);
        if (Phase == 2)
        {
            if (other == Homing)
            {
                Kill();
            }
        }
    }
}
