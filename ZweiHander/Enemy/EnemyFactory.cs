using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Enemy.EnemyStorage;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy;

/// <summary>
/// All Enemies that exist are within this.
/// </summary>
public enum EnemyName
{
    Darknut,
    Gel,
    Goriya,
    Keese,
    Stalfos
}

/// <summary>
/// Manages creations of Enemies.
/// </summary>
public class EnemyFactory
{
    /// <summary>
    /// Creates a new Enemy.
    /// </summary>
    /// <param name="EnemyName">What enemy to get.</param>
    /// <param name="position">The enemies starting position.</param>
    /// <param name="face">The enemies intial facing direction</param>
    /// <param name="throw">Whether or not the enemy shoots projectiles/is shooting</param> 
    /// <returns>The desired item.</returns>
    public IEnemy GetEnemy(EnemyName enemyName, Vector2 position = default, int face = default, int thrower = default)
    {
        IEnemy enemy = null;
        switch (enemyName)
        {
            case EnemyName.Darknut:
                enemy = new Darknut();
                break;
            case EnemyName.Gel:
                enemy = new Gel();
                break;
            case EnemyName.Goriya:
                enemy = new Goriya();
                break;
            case EnemyName.Keese:
                enemy = new Keese();
                break;
            case EnemyName.Stalfos:
                enemy = new Stalfos();
                break;
            default:
                // Should never actually reach here- will error out if so
                break;
        }
        enemy.position = position;
        enemy.face = face;
        enemy.thrower = thrower;
        return enemy;
    }

}