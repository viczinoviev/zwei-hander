using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Goriya enemy
/// </summary>
public class Goriya : IEnemy
{
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Projectile for this enemy
    /// </summary>
    IItem _currentProjectile;
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    private readonly EnemySprites _enemySprites;
    /// <summary>
    /// Manager for the projectile this enemy throws
    /// </summary>
    private readonly ItemManager _projectileManager;
    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = 5;

    readonly EnemyCollisionHandler _collisionHandler;

private int Thrower = 1;
    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    readonly Random rnd = new();


    public Goriya(EnemySprites enemySprites, ItemManager projectileManager)
    {
        _projectileManager = projectileManager;
        _enemySprites = enemySprites;
        Sprite = _enemySprites.GoriyaUp();
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        //Only move if not currrently throwing a projectile
        if (Thrower != 2)
        {
            //Randomize  movement
            int mov = rnd.Next(200);
            //Move according to current direction faced
            if (mov > 3)
            {
                Position = EnemyHelper.BehaveFromFace(this,1);
            }
            //Change face and sprite to new value according to the randomized value
            else
            {
                switch (mov)
                {
                    case 0:
                        if (Position.Y > 40)
                        {
                            Position = new Vector2(Position.X, Position.Y - 1);
                            Sprite = _enemySprites.GoriyaUp();
                            Face = 0;
                        }
                        else
                        {
                            goto case 2;
                        }
                        break;
                    case 1:
                        if (Position.X < 750)
                        {
                            Position = new Vector2(Position.X + 1, Position.Y);
                            Sprite = _enemySprites.GoriyaRight();
                            Face = 1;
                        }
                        else
                        {
                            goto case 3;
                        }
                        break;
                    case 2:
                        if (Position.Y < 400)
                        {
                            Position = new Vector2(Position.X, Position.Y + 1);
                            Sprite = _enemySprites.GoriyaDown();
                            Face = 2;
                        }
                        else
                        {
                            goto case 0;
                        }
                        break;
                    case 3:
                        if (Position.X > 40)
                        {
                            Position = new Vector2(Position.X - 1, Position.Y);
                            Sprite = _enemySprites.GoriyaLeft();
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
        }
        //Randomize attacking (projectile throwing)
        int attack = rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && Thrower != 2)
        {
            //Create a projectile
            _currentProjectile = _projectileManager.GetItem(ItemType.Boomerang, 3, position: Position);
            Thrower = 2;
            //Set up the projectiles behavior
            (float v, float a) = ItemHelper.BoomerangTrajectory(50, 3);
            _currentProjectile.Velocity = EnemyHelper.BehaveFromFace(this, v);
            _currentProjectile.Acceleration = EnemyHelper.BehaveFromFace(this, a);
        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (Thrower == 2)
            {

                if (_currentProjectile.IsDead())
                {
                    Thrower = 1;
                }
            }
        }
        _collisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
        _projectileManager.Update(time);
        }



    public void Draw()
    {
        Sprite.Draw(Position);
        _projectileManager.Draw();
    }
    public Rectangle GetCollisionBox()
    {
        return new Rectangle(
                (int)(Position.X - Sprite.Width),
                (int)(Position.Y - Sprite.Height),
                Sprite.Width + 15,
                Sprite.Height + 15
            );
    }
}


