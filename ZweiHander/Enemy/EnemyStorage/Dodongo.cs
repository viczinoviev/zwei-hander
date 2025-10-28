using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;
using System.Collections.Generic;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Dodongo boss
/// </summary>
public class Dodongo : IEnemy
{
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

    public int Hitpoints { get; set; } = 15;

    public EnemyCollisionHandler CollisionHandler { get; } = default;

    /// <summary>
    /// Random number generator to randomize boss behavior
    /// </summary>
    readonly Random rnd = new();


    public Dodongo(BossSprites bossSprites)
    {
        _bossSprites = bossSprites;
        _sprites.Add(_bossSprites.DodongoUp());
        _sprites.Add(_bossSprites.DodongoRight());
        _sprites.Add(_bossSprites.DodongoDown());
        _sprites.Add(_bossSprites.DodongoLeft());
        Sprite = _sprites[0];
        CollisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        Sprite = _sprites[Face];
        //Randomize  movement
        int mov = rnd.Next(200);
        //Move according to current direction faced
        if (mov > 3)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1,0);
        }
        //Change face according to the randomized value
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
        return new Rectangle(
                (int)(Position.X - Sprite.Width),
                (int)(Position.Y - Sprite.Height),
                Sprite.Width + 15,
                Sprite.Height + 15
            );
    }

}


