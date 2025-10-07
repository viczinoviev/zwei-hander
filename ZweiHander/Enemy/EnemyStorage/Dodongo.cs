using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Dodongo boss
/// </summary>
public class Dodongo : IEnemy
{
    /// <summary>
    /// The sprite associated with this boss.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Holds all sprites for this boss
/// </summary>
    private readonly BossSprites _bossSprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    public int Thrower { get; set; } = 0;
/// <summary>
/// Random number generator to randomize boss behavior
/// </summary>
    readonly Random rnd = new();


    public Dodongo(BossSprites bossSprites)
    {
        _bossSprites = bossSprites;
        _sprite = _bossSprites.DodongoUp();
    }
    public virtual void Update(GameTime time)
    {
        //Randomize  movement
        int mov = rnd.Next(200);
        //Move according to current direction faced
        if (mov > 5)
        {
            Position = new Vector2(Position.X + ((-1 + 2 * Convert.ToInt32(!(Face == 3 && Position.X > 40))) * Convert.ToInt32((Face == 3 && Position.X > 40) || (Face == 1 && Position.X < 750))), Position.Y + ((-1 + 2 * Convert.ToInt32(!(Face == 0 && Position.Y > 40))) * Convert.ToInt32((Face == 0 && Position.Y > 40) || (Face == 2 && Position.Y < 400))));
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
                        _sprite = _bossSprites.DodongoUp();
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
                        _sprite = _bossSprites.DodongoRight();
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
                        _sprite = _bossSprites.DodongoDown();
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
                        _sprite = _bossSprites.DodongoLeft();
                        Face = 3;
                    }
                    else
                    {
                        goto case 1;
                    }
                    break;
                default:
                    //no movement  
                    break;
            }
        }
        _sprite.Update(time);
        }


    

    public void Draw()
    {
        _sprite.Draw(Position);
    }
}


