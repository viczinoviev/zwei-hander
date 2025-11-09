using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZweiHander.Items.ItemStorages;

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
    
    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];
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

    public EnemyCollisionHandler CollisionHandler { get; } = default;

private int Thrower = 1;
    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    readonly Random rnd = new();


    public Goriya(EnemySprites enemySprites, ItemManager projectileManager)
    {
        _projectileManager = projectileManager;
        _enemySprites = enemySprites;
        //create list of all sprites associated with the enemy to swap with
        _sprites.Add(_enemySprites.GoriyaUp());
        _sprites.Add(_enemySprites.GoriyaRight());
        _sprites.Add(_enemySprites.GoriyaDown());
        _sprites.Add(_enemySprites.GoriyaLeft());
        Sprite = _sprites[0];
        CollisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        Sprite = _sprites[Face];
        //Only move if not currrently throwing a projectile
        if (Thrower != 2)
        {
            //Randomize  movement
            int mov = rnd.Next(200);
            //Move according to current direction faced
            if (mov > 3)
            {
                Position = EnemyHelper.BehaveFromFace(this, 1, 0);
            }
            //Change face and sprite to new value according to the randomized value
            else
            {
                Face = mov;
            }
        }
        //Randomize attacking (projectile throwing)
        int attack = rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && Thrower != 2)
        {
            //Create a projectile
            _currentProjectile = _projectileManager.GetItem<BoomerangItem>(position: Position);
            Thrower = 2;
            //Set up the projectiles behavior
            (float v, float a) = ItemHelper.BoomerangTrajectory(50, 3);
            _currentProjectile.Velocity = EnemyHelper.BehaveFromFace(this, v,1);
            _currentProjectile.Acceleration = EnemyHelper.BehaveFromFace(this, a,1);
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
        CollisionHandler.UpdateCollisionBox();
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


