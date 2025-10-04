using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using System.Runtime.CompilerServices;

namespace ZweiHander.Enemy;

/// <summary>
/// Keese enemy
/// </summary>
public class Keese : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;


    private EnemySprites _enemySprites;

    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = 1;

    Random rnd = new Random();


    public Keese(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.Keese();
    }
    public virtual void Update(GameTime time)
    {
        int mov = rnd.Next(200);
        if (mov > 5)
        {
            position = new Vector2(position.X + ((-1 + 2 * Convert.ToInt32(!(face == 3 && position.X > 40))) * Convert.ToInt32((face == 3 && position.X > 40) || (face == 1 && position.X < 750))), position.Y + ((-1 + 2 * Convert.ToInt32(!(face == 0 && position.Y > 40))) * Convert.ToInt32((face == 0 && position.Y > 40) || (face == 2 && position.Y < 400))));
        }
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
                    //no movement  
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


