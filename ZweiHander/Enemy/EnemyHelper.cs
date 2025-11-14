using System;
using System.Linq;
using Microsoft.Xna.Framework;
using ZweiHander.Enemy.EnemyStorage;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;

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
    public static Vector2 BehaveFromFace(IEnemy enemy, float magnitude, int thrown)
    {
        if (thrown != 1)
        {
            return new Vector2(enemy.Position.X + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3))) * (magnitude * Convert.ToInt32((enemy.Face == 3) || (enemy.Face == 1)))), enemy.Position.Y + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == 0))) * (magnitude * Convert.ToInt32((enemy.Face == 0) || (enemy.Face == 2)))));
        }
        else
        {
            return new Vector2((-1 + 2 * Convert.ToInt32(!(enemy.Face == 3))) * (magnitude * Convert.ToInt32((enemy.Face == 3) || (enemy.Face == 1))), (-1 + 2 * Convert.ToInt32(!(enemy.Face == 0))) * (magnitude * Convert.ToInt32((enemy.Face == 0) || (enemy.Face == 2))));
        }
    }

        /// <summary>
    /// Handles goriya projectile attack
    /// </summary>
    /// <param name="enemy">Goriya that is attacking</param>
    /// <param name="projectileManager">Projectile manager to handle the projectile</param>
    public static void goriyaAttack(Goriya enemy, ItemManager projectileManager)
    {
        //Randomize attacking (projectile throwing)
        int attack = enemy.rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && enemy.Thrower != 2)
        {
            //Create a projectile using ItemHelpers boomerang trajectory method
            (float v, float a) = ItemHelper.BoomerangTrajectory(50, 2);
            enemy._currentProjectile = projectileManager.GetItem<Boomerang>(position: enemy.Position,
            velocity: EnemyHelper.BehaveFromFace(enemy, v, 1), acceleration: EnemyHelper.BehaveFromFace(enemy, a, 1),
                extras: [() => enemy.Position, enemy.CollisionHandler]);
            enemy.Thrower = 2;
        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (enemy.Thrower == 2)
            {

                if (enemy._currentProjectile.IsDead())
                {
                    enemy.Thrower = 1;
                }
            }
        }
    }
    /// <summary>
    /// Handles aquamentus projectile attack
    /// </summary>
    /// <param name="enemy">Aquamentus that is attacking</param>
    /// <param name="projectileManager">Projectile manager to handle the projectiles</param>
    public static void aquamentusAttack(Aquamentus enemy, ItemManager projectileManager)
    {
        //Randomize attacking (projectile throwing)
        int attack = enemy.rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && enemy.Thrower != 2)
        {
            //Create projectiles and Set up the projectiles behavior
            IItem _currentProjectile1 = projectileManager.GetItem<Fireball>(3, position: new Vector2(enemy.Position.X - 20, enemy.Position.Y - 20));
            _currentProjectile1.Velocity = new Vector2(-100, 0);
            IItem _currentProjectile2 = projectileManager.GetItem<Fireball>(3, position: new Vector2(enemy.Position.X - 20, enemy.Position.Y - 20));
            _currentProjectile2.Velocity = new Vector2(-100, 30);
            IItem _currentProjectile3 = projectileManager.GetItem<Fireball>(3, position: new Vector2(enemy.Position.X - 20, enemy.Position.Y - 20));
            _currentProjectile3.Velocity = new Vector2(-100, -30);
            enemy._projectiles.Add(_currentProjectile1);
            enemy._projectiles.Add(_currentProjectile2);
            enemy._projectiles.Add(_currentProjectile3);
            enemy.Thrower = 2;
            //Set up the projectiles behavior
        }
        else
        {
            //If currently throwing and projectiles are dead, set back to not throwing
            if (enemy.Thrower == 2)
            {
                if (enemy._projectiles.First().IsDead())
                {
                    enemy._projectiles.Remove(enemy._projectiles.First());
                    if (enemy._projectiles.Count == 0)
                    {
                        enemy.Thrower = 1;
                        enemy._projectiles = [];
                    }
                }
            }
        }
    }

    public static void bladeTrapAttack(BladeTrap enemy)
    {
        
    }
}
