using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy.EnemyStorage;

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
    private readonly BossSprites _bossSprites;
/// <summary>
/// Manager for the projectile this enemy throws
/// </summary>
    private readonly ItemManager _projectileManager;
    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    public int Thrower { get; set; } = 1;
/// <summary>
/// Random number generator to randomize enemy behavior
/// </summary>
    readonly Random rnd = new();


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
                Position = new Vector2(Position.X + ((-1 + 2 * Convert.ToInt32(!(Face == 3 && Position.X > 40))) * Convert.ToInt32((Face == 3 && Position.X > 40) || (Face == 1 && Position.X < 750))), Position.Y);
            }
            //Change face and sprite to new value according to the randomized value
            else
            {
                switch (mov)
                {
                    case 0:            
                            goto case 1;
                    case 1:
                        if (Position.X < 750)
                        {
                            Position = new Vector2(Position.X + 1, Position.Y);
                            Face = 1;
                        }
                        else
                        {
                            goto case 3;
                        }
                        break;
                    case 2:
                        goto case 3;
                    case 3:
                        if (Position.X > 40)
                        {
                            Position = new Vector2(Position.X - 1, Position.Y);
                            Face = 3;
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
        if (attack == 5 && Thrower != 2)
        {
            //Create a projectile
            _currentProjectile1 = _projectileManager.GetItem(ItemType.Fireball, -1, position: new Vector2(Position.X - 20, Position.Y - 20));
            _currentProjectile1.Life = 3;
            _currentProjectile2 = _projectileManager.GetItem(ItemType.Fireball, -1, position: new Vector2(Position.X - 20, Position.Y - 20));
            _currentProjectile2.Life = 3;
            _currentProjectile3 = _projectileManager.GetItem(ItemType.Fireball, -1, position: new Vector2(Position.X - 20, Position.Y - 20));
            _currentProjectile3.Life = 3;
            Thrower = 2;
            //Set up the projectiles behavior
            _currentProjectile1.Velocity = new Vector2(-100,0);
            _currentProjectile2.Velocity = new Vector2(-100,30);
            _currentProjectile3.Velocity = new Vector2(-100,-30);

        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (Thrower == 2)
            {

                if (_currentProjectile1.Life <= 0)
                {
                    Thrower = 1;
                }
            }
        }
        _sprite.Update(time);
        _projectileManager.Update(time);
        }

    

    public void Draw()
    {
        _sprite.Draw(Position);
        _projectileManager.Draw();
    }
}


