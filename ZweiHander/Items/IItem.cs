using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
    /// The lifetime (in seconds) left for item; negative means infinite.
    /// </summary>
    public double Life { get; set; }

    /// <summary>
    /// Draws this item on screen.
    /// </summary>
    public void Draw();

    /// <summary>
    /// Updates this item, including life, sprite, and movement.
    /// <para>WARNING: Update does not kill the item; the itemManager's update does that.</para>
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
    /// What to do when Life reaches 0.
    /// </summary>
    public void OnDeath();
}