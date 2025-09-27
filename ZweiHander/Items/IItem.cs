using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Items;

/// <summary>
/// Anything usable by something else.
/// </summary>
public interface IItem
{
    /// <summary>
    /// Current position using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 position { get; set; }

    /// <summary>
    /// Current velocity using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 velocity { get; set; }

    /// <summary>
    /// Current velocity using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 acceleration { get; set; }

    /// <summary>
    /// Draws this item on screen.
    /// </summary>
    public void Draw();

    /// <summary>
    /// Updates this item.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime);
}