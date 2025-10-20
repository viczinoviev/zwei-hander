using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// zol enemy
/// </summary>
public class Zol : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    public ISprite _sprite { get; set; } = default;
/// <summary>
/// Holds all sprites for this enemy
/// </summary>
    private readonly EnemySprites _enemySprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    readonly EnemyCollisionHandler _collisionHandler;

/// <summary>
/// Random number generator to randomize enemy behavior
/// </summary>
    readonly Random rnd = new();


    public Zol(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.Zol();
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        //Randomize  movement
        int mov = rnd.Next(200);
        //Move according to current direction faced
        if (mov > 3)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1);
        }
        //Change face and sprite to new value according to the randomized value
        else
        {
            Face = mov;
        }
        _collisionHandler.UpdateCollisionBox();
        _sprite.Update(time);
        }

    public void Draw()
    {
        _sprite.Draw(Position);
    }
    public Rectangle GetCollisionBox()
    {
        return new Rectangle(
                (int)(Position.X - _sprite.Width),
                (int)(Position.Y - _sprite.Height),
                _sprite.Width + 15,
                _sprite.Height + 15
            );
    }
}


