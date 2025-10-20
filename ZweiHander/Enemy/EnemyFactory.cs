using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics.SpriteStorages;
using System.Collections.Generic;
using ZweiHander.Items;
using ZweiHander.Enemy.EnemyStorage;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy;


/// <summary>
/// Manages creations of Enemies.
/// </summary>
public class EnemyFactory(EnemySprites enemysprites, ItemManager projectileManager, BossSprites bossSprites, NPCSprites npcSprites)
{
    /// <summary>
    /// Sprites for enemies
    /// </summary>
    readonly EnemySprites _enemySprites = enemysprites;
    /// <summary>
    /// Sprites for bosses
    /// </summary>
    readonly BossSprites _bossSprites = bossSprites;
    /// <summary>
    /// Sprites for bosses
    /// </summary>
    readonly NPCSprites _npcSprites = npcSprites;
    /// <summary>
    /// Manager for projectiles thrown by enemies
    /// </summary>
    readonly ItemManager _projectileManager = projectileManager;
    /// <summary>
    /// List of currently existing enemies
    /// </summary>
    readonly List<IEnemy> currentEnemies = [];


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
                enemy = new Goriya(_enemySprites, _projectileManager);
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
            case "OldMan":
                enemy = new OldMan(_npcSprites);
                break;
            default:
                // Should never actually reach here- will error out if so
                break;
        }
        enemy.Position = position;
        enemy.Face = face;
        currentEnemies.Add(enemy);
        return enemy;
    }

}