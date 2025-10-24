using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;
using System.Collections.Generic;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Wallmaster enemy
/// </summary>
public class Wallmaster : IEnemy
{
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    
    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = new List<ISprite>();
    private readonly EnemySprites _enemySprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = 5;

    readonly EnemyCollisionHandler _collisionHandler;

    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    readonly Random rnd = new();


    public Wallmaster(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprites.Add(_enemySprites.WallmasterUp());
        _sprites.Add(_enemySprites.WallmasterDown());
        Sprite = _sprites[0];
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        if (Face == 0 || Face == 2)
        {
            Sprite = _sprites[Face / 2];
        }
        //Randomize  movement
        int mov = rnd.Next(200);
        //Move according to current direction faced
        if (mov > 3)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1,0);
        }
        //Change face and sprite to new value according to the randomized value
        else
        {
            Face = mov;
        }
        _collisionHandler.UpdateCollisionBox();
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


