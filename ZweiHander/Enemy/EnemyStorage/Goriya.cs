using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Goriya enemy
/// </summary>
public class Goriya : IEnemy
{
    private const int EnemyStartHealth = 5;
    private const int FaceChangeChance = 200;
    private const int FaceChangeCase = 3;
    private const int CollisionBoxOffset = 2;
    private const int Attacking = 2;
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Projectile for this enemy
    /// </summary>

    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];
    public IItem _currentProjectile;
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
    public int Hitpoints { get; set; } = EnemyStartHealth;

    public EnemyCollisionHandler CollisionHandler { get; } = default;

public int Thrower = 1;
    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    public readonly Random rnd = new();


    public Goriya(EnemySprites enemySprites, ItemManager projectileManager, ContentManager sfxPlayer)
    {
        _projectileManager = projectileManager;
        _enemySprites = enemySprites;
        //create list of all sprites associated with the enemy to swap with
        _sprites.Add(_enemySprites.GoriyaUp());
        _sprites.Add(_enemySprites.GoriyaRight());
        _sprites.Add(_enemySprites.GoriyaDown());
        _sprites.Add(_enemySprites.GoriyaLeft());
        Sprite = _sprites[0];
        CollisionHandler = new EnemyCollisionHandler(this, sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {
        Sprite = _sprites[Face];
        //Only move if not currrently throwing a projectile
        if (Thrower != Attacking)
        {
            //Randomize  movement
            int mov = rnd.Next(FaceChangeChance);
            //Move according to current direction faced
            if (mov > FaceChangeCase)
            {
                Position = EnemyHelper.BehaveFromFace(this, 1, 0);
            }
            //Change face and sprite to new value according to the randomized value
            else
            {
                Face = mov;
            }
        }
        //projectile handling
        EnemyHelper.GoriyaAttack(this,_projectileManager);
        //updates
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
        // Sprites are centered
        return new Rectangle(
                (int)Position.X - Sprite.Width / CollisionBoxOffset,
                (int)Position.Y - Sprite.Height / CollisionBoxOffset,
                Sprite.Width,
                Sprite.Height
        );
    }
}


