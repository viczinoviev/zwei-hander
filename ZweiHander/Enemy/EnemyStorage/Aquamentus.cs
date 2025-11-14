using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.CollisionFiles;
using ZweiHander.Items.ItemStorages;
using Microsoft.Xna.Framework.Content;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Aquamentus enemy
/// </summary>
public class Aquamentus : IEnemy
{
    public ISprite Sprite { get; set; }
    /// <summary>
    /// Projectiles for this enemy
    /// </summary>
    public List<IItem> _projectiles = [];
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

    public int Hitpoints { get; set; } = 15;

    public EnemyCollisionHandler CollisionHandler { get; } = default;
    public int Thrower = 1;
    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    public readonly Random rnd = new();


    public Aquamentus(BossSprites bossSprites, ItemManager projectileManager,ContentManager sfxPlayer)
    {
        _projectileManager = projectileManager;
        _bossSprites = bossSprites;
        Sprite = _bossSprites.Aquamentus();
        CollisionHandler = new EnemyCollisionHandler(this,sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {
        
            //Randomize  movement
            int mov = rnd.Next(200);
            //Move according to current direction faced
            if (mov > 3)
            {
                Position = EnemyHelper.BehaveFromFace(this,1,0);
            }
            //Change face to new value according to the randomized value
            else
            {
            Face = mov;
            }
        
        //Randomize attacking (projectile throwing)
        int attack = rnd.Next(300);
        //attack, as long as not already attacking
        if (attack == 5 && Thrower != 2)
        {
            //Create projectiles and Set up the projectiles behavior
            IItem _currentProjectile1 = _projectileManager.GetItem("Fireball", 3, position: new Vector2(Position.X - 20, Position.Y - 20));
            _currentProjectile1.Velocity = new Vector2(-100, 0);
            IItem _currentProjectile2 = _projectileManager.GetItem("Fireball", 3, position: new Vector2(Position.X - 20, Position.Y - 20));
            _currentProjectile2.Velocity = new Vector2(-100, 30);
            IItem _currentProjectile3 = _projectileManager.GetItem("Fireball", 3, position: new Vector2(Position.X - 20, Position.Y - 20));
            _currentProjectile3.Velocity = new Vector2(-100, -30);
            _projectiles.Add(_currentProjectile1);
            _projectiles.Add(_currentProjectile2);
            _projectiles.Add(_currentProjectile3);
            Thrower = 2;
            //Set up the projectiles behavior
        }
        else
        {
            //If currently throwing and projectile is dead, set back to not throwing
            if (Thrower == 2)
            {

        //projectile attacking
        EnemyHelper.aquamentusAttack(this, _projectileManager);
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
                Sprite.Width + 25,
                Sprite.Height + 15
            );
    }
}


