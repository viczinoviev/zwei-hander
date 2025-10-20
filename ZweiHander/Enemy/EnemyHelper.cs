using System;
using Microsoft.Xna.Framework;

namespace ZweiHander.Enemy;


/// <summary>
/// Contains methods that are helpful for enemies.
/// </summary>
class EnemyHelper
{
    /// <summary>
    /// Manages behaviors for enemies/projectiles based on their speed and direction they face
    /// </summary>
    /// <param name="enemy">the enemy to move</param>
    /// <param name="magnitude">the speed to move</param>
    /// <returns>Vector2 equating to the new position of the enemy</returns>
    public static Vector2 BehaveFromFace(IEnemy enemy, float magnitude)
    {
        return new Vector2(enemy.Position.X + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3))) * (magnitude * Convert.ToInt32((enemy.Face == 3) || (enemy.Face == 1)))), enemy.Position.Y + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 0))) * (magnitude * Convert.ToInt32((enemy.Face == 0) || (enemy.Face == 2)))));
    }
}
