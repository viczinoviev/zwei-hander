using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Dodongo boss
/// </summary>
public class Dodongo : IEnemy
{

    private const int EnemyStartHealth = 50;
    private const int FaceChangeChance = 200;
    private const int FaceChangeCase = 3;
    private const int CollisionBoxOffset = 2;
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];
    /// <summary>
    /// Holds all sprites for this boss
    /// </summary>
    private readonly BossSprites _bossSprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    public int Hitpoints { get; set; } = EnemyStartHealth;
    public float HitcoolDown { get; set; } = 0;

    public EnemyCollisionHandler CollisionHandler { get; } = default;

    /// <summary>
    /// Random number generator to randomize boss behavior
    /// </summary>
    readonly Random rnd = new();


    public Dodongo(BossSprites bossSprites, ContentManager sfxPlayer, Vector2 position)
    {
        Position = position;
        _bossSprites = bossSprites;
        _sprites.Add(_bossSprites.DodongoUp());
        _sprites.Add(_bossSprites.DodongoRight());
        _sprites.Add(_bossSprites.DodongoDown());
        _sprites.Add(_bossSprites.DodongoLeft());
        Sprite = _sprites[0];
        CollisionHandler = new EnemyCollisionHandler(this, sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {
        //Decrement hit cooldown
        if(HitcoolDown > 0)
        {
            HitcoolDown --;
        }
        Sprite = _sprites[Face];
        //Randomize  movement
        int mov = rnd.Next(FaceChangeChance);
        //Move according to current direction faced
        if (mov > FaceChangeCase)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1, 0);
        }
        //Change face according to the randomized value
        else
        {
            Face = mov;
        }
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
    }
    public void TakeDamage(int dmg)
    {
        Hitpoints -= dmg;

        if (Hitpoints <= 0)
        {
            if (CollisionHandler != null)
            {
                CollisionHandler.Dead = true;
            }
        }
    }



    public void Draw()
    {
        Sprite.Draw(Position);
    }
    public Rectangle GetCollisionBox()
    {
        // Sprites are centered
        return new Rectangle(
                (int)Position.X - (Sprite.Width / CollisionBoxOffset),
                (int)Position.Y - (Sprite.Height / CollisionBoxOffset),
                Sprite.Width,
                Sprite.Height
        );
    }

}


