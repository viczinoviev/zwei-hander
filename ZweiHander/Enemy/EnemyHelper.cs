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
    private const int Up = 0;
    private const int Right = 1;
    private const int Down = 2;
    private const int Left = 3;
    private const int randomChance = 300;
    private const int doAttack = 5;
    private const int attacking = 2;
    private const int attackDuration = 2;
    private const int attackLength = 50;
    private const int fireballLifetime = 3;
    private const int fireballSpawnOffset = 20;
    private const int fireballXSpeed = -100;
    private const int fireballYSpeed = 30;
    private const int FaceModulus = 4;
    private const int FaceChanger = 2;
    private const int TrapSpeed = 2;
    private const int AllowedPosDifference = 2;
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
            return new Vector2(enemy.Position.X + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == Left))) * (magnitude * Convert.ToInt32((enemy.Face == Left) || (enemy.Face == Right)))), enemy.Position.Y + ((-1 + 2 * Convert.ToInt32(!(enemy.Face == Up))) * (magnitude * Convert.ToInt32((enemy.Face == Up) || (enemy.Face == Down)))));
        }
        else
        {
            return new Vector2((-1 + 2 * Convert.ToInt32(!(enemy.Face == Left))) * (magnitude * Convert.ToInt32((enemy.Face == Left) || (enemy.Face == Right))), (-1 + 2 * Convert.ToInt32(!(enemy.Face == Up))) * (magnitude * Convert.ToInt32((enemy.Face == Up) || (enemy.Face == Down))));
        }
    }

        /// <summary>
    /// Handles goriya projectile attack
    /// </summary>
    /// <param name="enemy">Goriya that is attacking</param>
    /// <param name="projectileManager">Projectile manager to handle the projectile</param>
    public static void GoriyaAttack(Goriya enemy, ItemManager projectileManager)
    {
        //Randomize attacking (projectile throwing)
        int attack = enemy.rnd.Next(randomChance);
        //attack, as long as not already attacking
        if (attack == doAttack && enemy.Thrower != attacking)
        {
            //Create a projectile using ItemHelpers boomerang trajectory method
            (float v, float a) = Boomerang.Trajectory(attackLength, attackDuration);
            enemy._currentProjectile = projectileManager.GetItem("Boomerang",position: enemy.Position,
            velocity: EnemyHelper.BehaveFromFace(enemy, v, 1), acceleration: EnemyHelper.BehaveFromFace(enemy, a, 1),
                properties: [ItemProperty.EnemyProjectile],extras: [() => enemy.Position, enemy.CollisionHandler]);
            enemy.Thrower = attacking;
        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (enemy.Thrower == attacking)
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
    public static void AquamentusAttack(Aquamentus enemy, ItemManager projectileManager)
    {
        //Randomize attacking (projectile throwing)
        int attack = enemy.rnd.Next(randomChance);
        //attack, as long as not already attacking
        if (attack == doAttack && enemy.Thrower != attacking)
        {
            //Create projectiles and Set up the projectiles behavior
            IItem _currentProjectile1 = projectileManager.GetItem("Fireball",fireballLifetime, position: new Vector2(enemy.Position.X - fireballSpawnOffset, enemy.Position.Y - fireballSpawnOffset));
            _currentProjectile1.Velocity = new Vector2(fireballXSpeed, 0);
            IItem _currentProjectile2 = projectileManager.GetItem("Fireball",fireballLifetime, position: new Vector2(enemy.Position.X - fireballSpawnOffset, enemy.Position.Y - fireballSpawnOffset));
            _currentProjectile2.Velocity = new Vector2(fireballXSpeed, fireballYSpeed);
            IItem _currentProjectile3 = projectileManager.GetItem("Fireball",fireballLifetime, position: new Vector2(enemy.Position.X - fireballSpawnOffset, enemy.Position.Y - fireballSpawnOffset));
            _currentProjectile3.Velocity = new Vector2(fireballXSpeed, -fireballYSpeed);
            enemy._projectiles.Add(_currentProjectile1);
            enemy._projectiles.Add(_currentProjectile2);
            enemy._projectiles.Add(_currentProjectile3);
            enemy.Thrower = attacking;
            //Set up the projectiles behavior
        }
        else
        {
            //If currently throwing and projectiles are dead, set back to not throwing
            if (enemy.Thrower == attacking)
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

    public static void BladeTrapAttack(BladeTrap enemy, float dt)
    {
        if (enemy.attackTime >= 0)
        {
            enemy.Position = EnemyHelper.BehaveFromFace(enemy, TrapSpeed, 0);
            enemy.attackTime -= dt;
        }
        else
        {
            enemy.Thrower = attacking;
            enemy.Face = (enemy.Face + FaceChanger) % FaceModulus;
        }
    }
    public static void BladeTrapReturn(BladeTrap enemy)
    {
        if (Math.Abs(enemy.originalPosition.X - enemy.Position.X) >= AllowedPosDifference || Math.Abs(enemy.originalPosition.Y - enemy.Position.Y) >= AllowedPosDifference)
            {
                enemy.Position = EnemyHelper.BehaveFromFace(enemy, 1, 0);
            }
            else
            {
                enemy.Thrower = 0;
            }
    }
}
