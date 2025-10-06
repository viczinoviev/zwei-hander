using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy;

/// <summary>
/// Goriya enemy
/// </summary>
public class Goriya : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Projectile for this enemy
/// </summary>
    IItem _currentProjectile;
/// <summary>
/// Holds all sprites for this enemy
/// </summary>
    private EnemySprites _enemySprites;
/// <summary>
/// Manager for the projectile this enemy throws
/// </summary>
    private ItemManager _projectileManager;
    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = 1;
/// <summary>
/// Random number generator to randomize enemy behavior
/// </summary>
    Random rnd = new Random();


    public Goriya(EnemySprites enemySprites, ItemManager projectileManager)
    {
        _projectileManager = projectileManager;
        _enemySprites = enemySprites;
        _sprite = _enemySprites.GoriyaUp();
    }
    public virtual void Update(GameTime time)
    {
        //Only move if not currrently throwing a projectile
        if (thrower != 2)
        {
            //Randomize  movement
            int mov = rnd.Next(200);
            //Move according to current direction faced
            if (mov > 5)
            {
                position = new Vector2(position.X + ((-1 + 2 * Convert.ToInt32(!(face == 3 && position.X > 40))) * Convert.ToInt32((face == 3 && position.X > 40) || (face == 1 && position.X < 750))), position.Y + ((-1 + 2 * Convert.ToInt32(!(face == 0 && position.Y > 40))) * Convert.ToInt32((face == 0 && position.Y > 40) || (face == 2 && position.Y < 400))));
            }
            //Change face and sprite to new value according to the randomized value
            else
            {
                switch (mov)
                {
                    case 0:
                        if (position.Y > 40)
                        {
                            position = new Vector2(position.X, position.Y - 1);
                            _sprite = _enemySprites.GoriyaUp();
                            face = 0;
                        }
                        else
                        {
                            goto case 2;
                        }
                        break;
                    case 1:
                        if (position.X < 750)
                        {
                            position = new Vector2(position.X + 1, position.Y);
                            _sprite = _enemySprites.GoriyaRight();
                            face = 1;
                        }
                        else
                        {
                            goto case 3;
                        }
                        break;
                    case 2:
                        if (position.Y < 400)
                        {
                            position = new Vector2(position.X, position.Y + 1);
                            _sprite = _enemySprites.GoriyaDown();
                            face = 2;
                        }
                        else
                        {
                            goto case 0;
                        }
                        break;
                    case 3:
                        if (position.X > 40)
                        {
                            position = new Vector2(position.X - 1, position.Y);
                            _sprite = _enemySprites.GoriyaLeft();
                            face = 3;
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
        //Randomize attacking (projectile throwing)
        int attack = rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && thrower != 2)
        {
            //Create a projectile
            _currentProjectile = _projectileManager.GetItem(ItemType.Boomerang, -1, position: position);
            _currentProjectile.Life = 3;
            thrower = 2;
            //Set up the projectiles behavior
            (float v, float a) = ItemHelper.BoomerangTrajectory(50, 3);
            _currentProjectile.Velocity = new Vector2((-1 + 2 * Convert.ToInt32(!(face == 3))) * (v * Convert.ToInt32((face == 3) || (face == 1))), (-1 + 2 * Convert.ToInt32(!(face == 0))) * (v * Convert.ToInt32((face == 0) || (face == 2))));
            _currentProjectile.Acceleration = new Vector2((-1 + 2 * Convert.ToInt32(!(face == 3))) * (a * Convert.ToInt32((face == 3) || (face == 1))), (-1 + 2 * Convert.ToInt32(!(face == 0))) * (a * Convert.ToInt32((face == 0) || (face == 2))));
        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (thrower == 2)
            {

                if (_currentProjectile.Life <= 0)
                {
                    thrower = 1;
                }
            }
        }
        _sprite.Update(time);
        _projectileManager.Update(time);
        }

    

    public void Draw()
    {
        _sprite.Draw(position);
        _projectileManager.Draw();
    }
}


