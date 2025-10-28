using Microsoft.Xna.Framework;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;

namespace ZweiHander.Enemy;

/// <summary>
/// Any character designed to impede the player
/// </summary>
public interface IEnemy
{
    /// <summary>
    /// Current position using xy coordinate system; default is (0,0).
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Currently faced direction, equating to what sprite to draw (0 = up, 1 = right, 2 = down, 3 = left)
    /// </summary>

    public int Face { get; set; }
/// <summary>
/// Current amount of health, when <= 0, this enemy is dead.
/// </summary>
    public int Hitpoints { get; set; }
/// <summary>
/// Collision manager for the enemy
/// </summary>
    public EnemyCollisionHandler CollisionHandler { get; }

    /// <summary>
    /// The current sprite associated with this Enemy.
    /// </summary>
    protected ISprite Sprite { get; set; }

    /// <summary>
    /// Draws this Enemy on screen.
    /// </summary>
    public void Draw();

    /// <summary>
    /// Updates this Enemy.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime);
/// <summary>
/// Gets the collisionBox for the enemy
/// </summary>
/// <returns>Rectangle equating to the collision box</returns>
    public Rectangle GetCollisionBox();
    
}