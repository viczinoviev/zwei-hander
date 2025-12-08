using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Aquamentus enemy
/// </summary>
public class Aquamentus : IEnemy
{
    private const int EnemyStartHealth = 50;
    private const int FaceChangeChance = 200;
    private const int FaceChangeCase = 3;
    private const int CollisionBoxOffset = 2;
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

    public int Hitpoints { get; set; } = EnemyStartHealth;

    public EnemyCollisionHandler CollisionHandler { get; } = default;
    public int Thrower = 1;
    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    public readonly Random rnd = new();


    public Aquamentus(BossSprites bossSprites, ItemManager projectileManager, ContentManager sfxPlayer, Vector2 position)
    {
        Position = position;
        _projectileManager = projectileManager;
        _bossSprites = bossSprites;
        Sprite = _bossSprites.Aquamentus();
        CollisionHandler = new EnemyCollisionHandler(this, sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {

        //Randomize  movement
        int mov = rnd.Next(FaceChangeChance);
        //Move according to current direction faced
        if (mov > FaceChangeCase)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1, 0);
        }
        //Change face to new value according to the randomized value
        else
        {
            Face = mov;
        }

        //projectile attacking
        EnemyHelper.AquamentusAttack(this, _projectileManager);
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


