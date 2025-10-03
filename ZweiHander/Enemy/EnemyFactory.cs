using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics.SpriteStorages;
using System.Collections.Generic;
using ZweiHander.Items;

namespace ZweiHander.Enemy;

/// <summary>
/// All Enemies that exist are within this.
/// </summary>
/*public enum EnemyName
{
    Darknut,
    Gel,
    Goriya,
    Keese,
    Stalfos
} */


/// <summary>
/// Manages creations of Enemies.
/// </summary>
public class EnemyFactory
{
    EnemySprites _enemySprites;

    ItemManager _projectileManager;
    List<IEnemy> currentEnemies;
    public EnemyFactory(EnemySprites enemysprites, ItemManager projectileManager)
    {
        _enemySprites = enemysprites;
        _projectileManager = projectileManager;
        currentEnemies = new List<IEnemy>();
    }
    /// <summary>
    /// Creates a new Enemy.
    /// </summary>
    /// <param name="EnemyName">What enemy to get.</param>
    /// <param name="position">The enemies starting position.</param>
    /// <param name="face">The enemies intial facing direction</param>
    /// <param name="throw">Whether or not the enemy shoots projectiles/is shooting</param> 
    /// <returns>The desired item.</returns>
    public IEnemy GetEnemy(String enemyName, Vector2 position = default, int face = default, int thrower = default)
    {
        IEnemy enemy = null;
        switch (enemyName)
        {
            case "Darknut":
                enemy = new Darknut(_enemySprites,_projectileManager);
                break;
            case "Gel":
                enemy = new Gel(_enemySprites);
                break;
            case "Goriya":
                enemy = new Goriya(_enemySprites);
                break;
            case "Keese":
                enemy = new Keese(_enemySprites);
                break;
            case "Stalfos":
                enemy = new Stalfos(_enemySprites);
                break;
            default:
                // Should never actually reach here- will error out if so
                break;
        }
        enemy.position = position;
        enemy.face = face;
        enemy.thrower = thrower;
        currentEnemies.Add(enemy);
        return enemy;
    }

}