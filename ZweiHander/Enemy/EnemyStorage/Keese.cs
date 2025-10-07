using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Keese enemy
/// </summary>
public class Keese : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Holds all sprites for this enemy
/// </summary>
    private readonly EnemySprites _enemySprites;

    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = 0;
/// <summary>
/// Random number generator to randomize enemy behavior
/// </summary>
    readonly Random rnd = new();


    public Keese(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.Keese();
    }
    public virtual void Update(GameTime time)
    {
        //Randomize  movement
        int mov = rnd.Next(300);
        //Move according to current direction faced
        if (mov > 8)
        {
            if (face < 4)
            {
                position = new Vector2(position.X + ((-1 + 2 * Convert.ToInt32(!(face == 3 && position.X > 40))) * Convert.ToInt32((face == 3 && position.X > 40) || (face == 1 && position.X < 750))), position.Y + ((-1 + 2 * Convert.ToInt32(!(face == 0 && position.Y > 40))) * Convert.ToInt32((face == 0 && position.Y > 40) || (face == 2 && position.Y < 400))));
            }
            else
            {
                switch (face)
                {
                    case 4:
                        position = new Vector2(position.X + 1, position.Y + 1);
                        break;
                    case 5:
                        position = new Vector2(position.X + 1, position.Y - 1);
                        break;
                    case 6:
                        position = new Vector2(position.X - 1, position.Y + 1);
                        break;
                    case 7:
                        position = new Vector2(position.X - 1, position.Y - 1);
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
                    if (position.Y > 40)
                    {
                        position = new Vector2(position.X, position.Y - 1);
                        face = 0;
                    }
                    else
                    {
                        goto case 2;
                    }
                    break;
                case 1:
                    if (position.X < 750)
                    {
                        position = new Vector2(position.X + 1, position.Y);
                        face = 1;
                    }
                    else
                    {
                        goto case 3;
                    }
                    break;
                case 2:
                    if (position.Y < 400)
                    {
                        position = new Vector2(position.X, position.Y + 1);
                        face = 2;
                    }
                    else
                    {
                        goto case 0;
                    }
                    break;
                case 3:
                    if (position.X > 40)
                    {
                        position = new Vector2(position.X - 1, position.Y);
                        face = 3;
                    }
                    else
                    {
                        goto case 1;
                    }
                    break;
                default:
                    face = mov;
                    break;
            }
        }
        _sprite.Update(time);
        }


    

    public void Draw()
    {
        _sprite.Draw(position);
    }
}


