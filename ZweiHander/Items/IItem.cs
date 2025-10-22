using Microsoft.Xna.Framework;
using System;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Items;

/// <summary>
/// Anything usable by something else.
/// </summary>
public interface IItem
{
    /// <summary>
    /// Current position using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Current velocity using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// Current acceleration using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 Acceleration { get; set; }

    /// <summary>
    /// What type of item this is.
    /// </summary>
    public ItemType ItemType { get; }

    /// <summary>
    /// Draws this item on screen.
    /// </summary>
    public void Draw();

    /// <summary>
    /// Updates this item, including life, sprite, and movement.
    /// <para>WARNING: Should not be called itself; let ItemManager call this.</para>
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime);

    /// <summary>
    /// Removes a property from the item, if it has it.
    /// </summary>
    /// <param name="property">Property to be removed.</param>
    public void RemoveProperty(ItemProperty property);

    /// <summary>
    /// Add a property to the item, if it does not have it
    /// </summary>
    /// <param name="property">Property to be added.</param>
    public void AddProperty(ItemProperty property);

    /// <summary>
    /// Whether this item has this property or not.
    /// </summary>
    /// <param name="property">Property to check for.</param>
    /// <returns>If the item has this property.</returns>
    public bool HasProperty(ItemProperty property);

    /// <summary>
    /// Adds type to be damaged by this item.
    /// </summary>
    /// <param name="damaged">Type to be damaged.</param>
    /// <param name="damage">How this type is damaged.</param>
    public void SetDamage(Type damaged, DamageObject damage);

    /// <summary>
    /// Gets the damage associated with a type.
    /// </summary>
    /// <param name="damaged">What is getting damaged.</param>
    /// <returns>How to damage what is being damaged.</returns>
    public DamageObject GetDamage(Type damaged);

    /// <summary>
    /// What to do when life reaches 0.
    /// <para>WARNING: Should not be called itself; let ItemManager call this.</para>
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void OnDeath(GameTime gameTime);

    /// <summary>
    /// Whether this item needs to be deleted.
    /// </summary>
    /// <returns>True if "dead", false if "alive".</returns>
    public bool IsDead();

    /// <summary>
    /// Just kill this guy bruh.
    /// </summary>
    public void Kill();
    
    /// <summary>
    /// Gets the hitbox of this item.
    /// </summary>
    /// <returns>The rectangle (x,y,w,h) representing this hitbox.</returns>
    public Rectangle GetHitBox();

    /// <summary>
    /// What to do on collision.
    /// </summary>
    /// <param name="other">What is being collided with.</param>
    /// <param name="collisionInfo">Info related to the collision.</param>
    public void HandleCollision(ICollisionHandler other, CollisionInfo collisionInfo);
}