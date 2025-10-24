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
    /// <param name="enemy">the enemy to behave</param>
    /// <param name="magnitude">the speed to behave</param>
    /// <param name="thrown">If this call is for a projectile or not (1 = projectile)</param>
    /// <returns>Vector2 equating to the new position/acceleration/velocity</returns>
    public static Vector2 BehaveFromFace(IEnemy enemy, float magnitude,int thrown)
    {
        if (thrown != 1)
        {
            return new Vector2(enemy.Position.X + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3))) * (magnitude * Convert.ToInt32((enemy.Face == 3) || (enemy.Face == 1)))), enemy.Position.Y + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 0))) * (magnitude * Convert.ToInt32((enemy.Face == 0) || (enemy.Face == 2)))));
        }
        else
        {
            return new Vector2((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3))) * (magnitude * Convert.ToInt32((enemy.Face == 3) || (enemy.Face == 1))),  (-1 + 2 * Convert.ToInt32(!(enemy.Face == 0))) * (magnitude * Convert.ToInt32((enemy.Face == 0) || (enemy.Face == 2))));
        }
    }
}
