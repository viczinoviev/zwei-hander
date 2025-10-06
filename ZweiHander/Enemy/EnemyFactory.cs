using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics.SpriteStorages;
using System.Collections.Generic;
using ZweiHander.Items;
using ZweiHander.boss;

namespace ZweiHander.Enemy;


/// <summary>
/// Manages creations of Enemies.
/// </summary>
public class EnemyFactory
{
    /// <summary>
    /// Sprites for enemies
    /// </summary>
    EnemySprites _enemySprites;
    /// <summary>
    /// Sprites for bosses
    /// </summary>
    BossSprites _bossSprites;
    /// <summary>
    /// Manager for projectiles thrown by enemies
    /// </summary>
    ItemManager _projectileManager;
    /// <summary>
    /// List of currently existing enemies
    /// </summary>
    List<IEnemy> currentEnemies;
    public EnemyFactory(EnemySprites enemysprites, ItemManager projectileManager,BossSprites bossSprites)
    {
        _enemySprites = enemysprites;
        _bossSprites = bossSprites;
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
                enemy = new Darknut(_enemySprites);
                break;
            case "Gel":
                enemy = new Gel(_enemySprites);
                break;
            case "Goriya":
                enemy = new Goriya(_enemySprites,_projectileManager);
                break;
            case "Keese":
                enemy = new Keese(_enemySprites);
                break;
            case "Stalfos":
                enemy = new Stalfos(_enemySprites);
                break;
            case "Aquamentus":
                enemy = new Aquamentus(_bossSprites, _projectileManager);
                break;
                case "Rope":
                enemy = new Rope(_enemySprites);
                break;
                case "Wallmaster":
                enemy = new Wallmaster(_enemySprites);
                break;
                case "Zol":
                enemy = new Zol(_enemySprites);
                break;
                case "Dodongo":
                enemy = new Dodongo(_bossSprites);
                break;
                case "BladeTrap":
                enemy = new BladeTrap(_enemySprites);
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