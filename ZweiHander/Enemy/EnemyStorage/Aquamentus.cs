using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy;

/// <summary>
/// Aquamentus enemy
/// </summary>
public class Aquamentus : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Projectile for this enemy
/// </summary>
    IItem _currentProjectile1;
    /// <summary>
/// Projectile for this enemy
/// </summary>
    IItem _currentProjectile2;
    /// <summary>
/// Projectile for this enemy
/// </summary>
    IItem _currentProjectile3;
/// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    private BossSprites _bossSprites;
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


    public Aquamentus(BossSprites bossSprites, ItemManager projectileManager)
    {
        _projectileManager = projectileManager;
        _bossSprites = bossSprites;
        _sprite = _bossSprites.Aquamentus();
    }
    public virtual void Update(GameTime time)
    {
        
            //Randomize  movement
            int mov = rnd.Next(200);
            //Move according to current direction faced
            if (mov > 5)
            {
                position = new Vector2(position.X + ((-1 + 2 * Convert.ToInt32(!(face == 3 && position.X > 40))) * Convert.ToInt32((face == 3 && position.X > 40) || (face == 1 && position.X < 750))), position.Y);
            }
            //Change face and sprite to new value according to the randomized value
            else
            {
                switch (mov)
                {
                    case 0:            
                            goto case 1;
                    case 1:
                        if (position.X < 750)
                        {
                            position = new Vector2(position.X + 1, position.Y);
                            face = 1;
                        }
                        else
                        {
                            goto case 3;
                        }
                        break;
                    case 2:
                        goto case 3;
                    case 3:
                        if (position.X > 40)
                        {
                            position = new Vector2(position.X - 1, position.Y);
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
        
        //Randomize attacking (projectile throwing)
        int attack = rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && thrower != 2)
        {
            //Create a projectile
            _currentProjectile1 = _projectileManager.GetItem(ItemType.Boomerang, -1, position: new Vector2(position.X - 20, position.Y - 20));
            _currentProjectile1.Life = 3;
            _currentProjectile2 = _projectileManager.GetItem(ItemType.Boomerang, -1, position: new Vector2(position.X - 20, position.Y - 20));
            _currentProjectile2.Life = 3;
            _currentProjectile3 = _projectileManager.GetItem(ItemType.Boomerang, -1, position: new Vector2(position.X - 20, position.Y - 20));
            _currentProjectile3.Life = 3;
            thrower = 2;
            //Set up the projectiles behavior
            (float v, float a) = ItemHelper.BoomerangTrajectory(50, 3);
            v = -v;
            a = -a;
            _currentProjectile1.Velocity = new Vector2(v,0);
            _currentProjectile1.Acceleration = new Vector2(a,0);
            _currentProjectile2.Velocity = new Vector2(v,v);
            _currentProjectile2.Acceleration = new Vector2(a,a);
            _currentProjectile3.Velocity = new Vector2(v,-v);
            _currentProjectile3.Acceleration = new Vector2(a,-a);

        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (thrower == 2)
            {

                if (_currentProjectile1.Life <= 0)
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


