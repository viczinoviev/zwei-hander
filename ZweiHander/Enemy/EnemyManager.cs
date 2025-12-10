using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.Damage;
using ZweiHander.Enemy.EnemyStorage;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy;


/// <summary>
/// Manages creations of Enemies.
/// </summary>
public class EnemyManager(EnemySprites enemysprites, ItemManager projectileManager, BossSprites bossSprites, NPCSprites npcSprites, ContentManager sfx)
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

    protected List<DamageDisplay> DamageNumbers = [];



    private readonly ContentManager sfxPlayer = sfx;


    /// <summary>
    /// Creates a new Enemy.
    /// </summary>
    /// <param name="enemyName">What enemy to get.</param>
    /// <param name="position">The enemies starting position.</param>
    /// <param name="face">The enemies intial facing direction</param>
    /// <returns>The desired item.</returns>
    public IEnemy MakeEnemy(String enemyName, Vector2 position, int face = default)
    {
        IEnemy enemy = null;
        switch (enemyName)
        {
            case "Darknut":
                enemy = new Darknut(_enemySprites, sfxPlayer, position);
                break;
            case "Gel":
                enemy = new Gel(_enemySprites, sfxPlayer, position);
                break;
            case "Goriya":
                enemy = new Goriya(_enemySprites, _projectileManager, sfxPlayer, position);
                break;
            case "Keese":
                enemy = new Keese(_enemySprites, sfxPlayer, position);
                break;
            case "Stalfos":
                enemy = new Stalfos(_enemySprites, sfxPlayer, position);
                break;
            case "Aquamentus":
                enemy = new Aquamentus(_bossSprites, _projectileManager, sfxPlayer, position);
                break;
            case "Rope":
                enemy = new Rope(_enemySprites, sfxPlayer, position);
                break;
            case "Wallmaster":
                enemy = new Wallmaster(_enemySprites, sfxPlayer, position);
                break;
            case "Zol":
                enemy = new Zol(_enemySprites, sfxPlayer, position);
                break;
            case "Dodongo":
                enemy = new Dodongo(_bossSprites, sfxPlayer, position);
                break;
            case "BladeTrap":
                enemy = new BladeTrap(_enemySprites, sfxPlayer, position);
                break;
            case "OldMan":
                enemy = new OldMan(_npcSprites);
                break;
            case "MovingBlock":
                enemy = new MovingBlock(_enemySprites, position, Vector2.Zero, 5f);
                break;
            default:
                // Should never actually reach here- will error out if so
                break;
        }
        enemy.Position = position;
        enemy.Face = face;
        enemy.DamageNumbers = DamageNumbers;
        currentEnemies.Add(enemy);
        return enemy;
    }
    /// <summary>
    /// Updates all enemies in the manager and removes dead enemies
    /// </summary>
    /// <param name="time">gametime</param>
    public void Update(GameTime time)
    {
        foreach (IEnemy _enemy in currentEnemies)
        {

            _enemy.Update(time);
        }
        currentEnemies.RemoveAll(enemy => enemy.Hitpoints <= 0);
        foreach (DamageDisplay damageDisplay in DamageNumbers)
        {
            damageDisplay.Update(time);
        }
        DamageNumbers.RemoveAll(display => display.Finished);
    }
    /// <summary>
    /// Draws all enemies in the manager
    /// </summary>
    public void Draw()
    {
        foreach (IEnemy _enemy in currentEnemies)
        {
            _enemy.Draw();
        }

        foreach (DamageDisplay damageDisplay in DamageNumbers)
        {
            damageDisplay.Draw();
        }
    }
    /// <summary>
    /// Clears the manager of all enemies
    /// </summary>
    public void Clear()
    {
        foreach (IEnemy _enemy in currentEnemies)
        {
            _enemy.CollisionHandler.Dead = true;
            if (_enemy is BladeTrap bladeTrap)
            {
                bladeTrap.homeRightCollisionHandler.Dead = true;
                bladeTrap.homeUpCollisionHandler.Dead = true;
                bladeTrap.homeLeftCollisionHandler.Dead = true;
                bladeTrap.homeDownCollisionHandler.Dead = true;
            }
        }
        currentEnemies.RemoveAll(enemy => enemy != null);
        _projectileManager.Clear();
    }
    public bool HasThisEnemyInstance(IEnemy enemy)
    {
        return currentEnemies.Contains(enemy);
    }

    public bool IsEmpty()
    {
        return currentEnemies.Count == 0;
    }

    public void MCreateWave(int waveNum)
    {
        WaveConstuctor.CreateWave(this, waveNum);
    }
    public IReadOnlyList<IEnemy> CurrentEnemiesPub => currentEnemies;
}