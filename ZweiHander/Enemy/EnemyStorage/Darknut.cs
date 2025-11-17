using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Darknut enemy
/// </summary>
public class Darknut : IEnemy
{
    private const int EnemyStartHealth = 5;
    private const int FaceChangeChance = 200;
    private const int FaceChangeCase = 3;
    private const int CollisionBoxOffset = 2;
    public ISprite Sprite { get; set; } = default;

    private readonly List<ISprite> _sprites = [];
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    private readonly EnemySprites _enemySprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = EnemyStartHealth;

    public EnemyCollisionHandler CollisionHandler { get; } = default;

    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    readonly Random rnd = new();


    public Darknut(EnemySprites enemySprites,ContentManager sfxPlayer)
    {
        _enemySprites = enemySprites;
        //create list of all sprites associated with the enemy to swap with
        _sprites.Add(_enemySprites.DarknutMoveUp());
        _sprites.Add(_enemySprites.DarknutMoveRight());
        _sprites.Add(_enemySprites.DarknutMoveDown());
        _sprites.Add(_enemySprites.DarknutMoveLeft());
        Sprite = _sprites[0];
        CollisionHandler = new EnemyCollisionHandler(this,sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {
        //Change sprite to correct sprite
        Sprite = _sprites[Face];
        //Randomize  movement
        int mov = rnd.Next(FaceChangeChance);
        //Move according to current direction faced
        if (mov > FaceChangeCase)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1,0);
        }
        //Change face and sprite to new value according to the randomized value
        else
        {
            Face = mov;
        }
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
        }




    public void Draw()
    {
        Sprite.Draw(Position);
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


