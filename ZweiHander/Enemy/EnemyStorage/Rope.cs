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
/// Rope enemy
/// </summary>
public class Rope : IEnemy
{
    private const int EnemyStartHealth = 5;
    private const int FaceLeft = 3;
    private const int FaceChangeHelper1 = 2;
    private const int FaceChangeHelper2 = 3;
    private const int FaceChangeChance = 200;
    private const int FaceChangeCase = 3;
    private const int CollisionBoxOffset = 2;
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
        /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];
    private readonly EnemySprites _enemySprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = EnemyStartHealth;

    public EnemyCollisionHandler CollisionHandler { get; } = default;

    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    readonly Random rnd = new();


    public Rope(EnemySprites enemySprites,ContentManager sfxPlayer,Vector2 position)
    {
        Position = position;
        _enemySprites = enemySprites;
        _sprites.Add(_enemySprites.RopeRight());
        _sprites.Add(_enemySprites.RopeLeft());
        Sprite = _sprites[0];
        Face = 1;
        CollisionHandler = new EnemyCollisionHandler(this,sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {
        if (Face == 1 || Face == FaceLeft)
        {
            Sprite = _sprites[(Face * FaceChangeHelper1 + 1) % FaceChangeHelper2];
        }
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


