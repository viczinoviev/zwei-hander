using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Gel enemy
/// </summary>
public class Gel : IEnemy
{
    private const int EnemyStartHealth = 5;
    private const int FaceChangeChance = 200;
    private const int FaceChangeCase = 3;
    private const int CollisionBoxOffset = 2;
    public ISprite Sprite { get; set; } = default;
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


    public Gel(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
    {
        Position = position;
        _enemySprites = enemySprites;
        Sprite = _enemySprites.Gel();
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


