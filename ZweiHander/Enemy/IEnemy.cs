using Microsoft.Xna.Framework;

namespace ZweiHander.Enemy;

/// <summary>
/// Any character designed to impede the player
/// </summary>
public interface IEnemy
{
    /// <summary>
    /// Current position using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 position { get; set; }

    /// <summary>
    /// Current direction, equating to what sprite to draw (0 = up, 1 = right, 2 = down, 3 = left)
    /// </summary>
    public int face { get; set; }

    /// <summary>
    /// Current status of projectile use (0 = non throwing enemy, 1 = throwing enemy, not currently throwing, 2 = currently throwing)
    /// </summary>
    public int thrower { get; set; }

    /// <summary>
    /// Draws this Enemy on screen.
    /// </summary>
    public void Draw();

    /// <summary>
    /// Updates this Enemy.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime);
}