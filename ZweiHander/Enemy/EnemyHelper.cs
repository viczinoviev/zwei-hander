using System;
using Microsoft.Xna.Framework;

namespace ZweiHander.Enemy;


/// <summary>
/// Contains methods that are helpful for enemies.
/// </summary>
class EnemyHelper
{
    /// <summary>
    /// Moves the enemy based on their speed and direction they face
    /// </summary>
    /// <param name="enemy">the enemy to move</param>
    /// <param name="magnitude">the speed to move</param>
    /// <returns>Vector2 equating to the new position of the enemy</returns>
    public static Vector2 BehaveFromFace(IEnemy enemy, float magnitude)
    {
        //return new Vector2(enemy.Position.X + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3 && enemy.Position.X > 40))) * (magnitude * Convert.ToInt32((enemy.Face == 3 && enemy.Position.X > 40) || (enemy.Face == 1 && enemy.Position.X < 750)))), enemy.Position.Y + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 0 && enemy.Position.Y > 40))) * (magnitude * Convert.ToInt32((enemy.Face == 0 && enemy.Position.Y > 40) || (enemy.Face == 2 && enemy.Position.Y < 400)))));
        return new Vector2(enemy.Position.X + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3))) * (magnitude * Convert.ToInt32((enemy.Face == 3) || (enemy.Face == 1)))), enemy.Position.Y + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 0))) * (magnitude * Convert.ToInt32((enemy.Face == 0) || (enemy.Face == 2)))));
    }

/// <summary>
    /// Moves the enemy and changes their face, only used for enemies with non directional sprites
    /// </summary>
    /// <param name="enemy">the enemy to move and change direction</param>
    /// <param name="mov">the face to swap to</param>
    public static void NoDirectionFaceChange(IEnemy enemy, int mov)
    {
        switch (mov)
        {
            case 0:
                if (enemy.Position.Y > 40)
                {
                    enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y - 1);
                    enemy.Face = 0;
                }
                else
                {
                    goto case 2;
                }
                break;
            case 1:
                if (enemy.Position.X < 750)
                {
                    enemy.Position = new Vector2(enemy.Position.X + 1, enemy.Position.Y);
                    enemy.Face = 1;
                }
                else
                {
                    goto case 3;
                }
                break;
            case 2:
                if (enemy.Position.Y < 400)
                {
                    enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y + 1);
                    enemy.Face = 2;
                }
                else
                {
                    goto case 0;
                }
                break;
            case 3:
                if (enemy.Position.X > 40)
                {
                    enemy.Position = new Vector2(enemy.Position.X - 1, enemy.Position.Y);
                    enemy.Face = 3;
                }
                else
                {
                    goto case 1;
                }
                break;
            default:
                //no movement  
                break;
        }
    }
}
