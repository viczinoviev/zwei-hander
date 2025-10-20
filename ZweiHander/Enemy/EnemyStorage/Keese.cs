using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Keese enemy
/// </summary>
public class Keese : IEnemy
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


    public Keese(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.Keese();
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        //Randomize  movement
        int mov = rnd.Next(300);
        //Move according to current direction faced
        if (mov > 8)
        {
            if (Face < 4)
            {
                Position = EnemyHelper.BehaveFromFace(this, 1);
            }
            else
            {
                switch (Face)
                {
                    case 4:
                        Position = new Vector2(Position.X + 1, Position.Y + 1);
                        break;
                    case 5:
                        Position = new Vector2(Position.X + 1, Position.Y - 1);
                        break;
                    case 6:
                        Position = new Vector2(Position.X - 1, Position.Y + 1);
                        break;
                    case 7:
                        Position = new Vector2(Position.X - 1, Position.Y - 1);
                        break;
                    default:
                        break;
                }
            }
        }
        //Change face and sprite to new value according to the randomized value
        else
        {
            switch (mov)
            {
                case 0:
                    if (Position.Y > 40)
                    {
                        Position = new Vector2(Position.X, Position.Y - 1);
                        Face = 0;
                    }
                    else
                    {
                        goto case 2;
                    }
                    break;
                case 1:
                    if (Position.X < 750)
                    {
                        Position = new Vector2(Position.X + 1, Position.Y);
                        Face = 1;
                    }
                    else
                    {
                        goto case 3;
                    }
                    break;
                case 2:
                    if (Position.Y < 400)
                    {
                        Position = new Vector2(Position.X, Position.Y + 1);
                        Face = 2;
                    }
                    else
                    {
                        goto case 0;
                    }
                    break;
                case 3:
                    if (Position.X > 40)
                    {
                        Position = new Vector2(Position.X - 1, Position.Y);
                        Face = 3;
                    }
                    else
                    {
                        goto case 1;
                    }
                    break;
                default:
                    Face = mov;
                    break;
            }
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


