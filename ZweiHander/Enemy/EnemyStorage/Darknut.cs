using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using System.Security.Principal;

namespace ZweiHander.Enemy;

/// <summary>
/// Darknut enemy
/// </summary>
public class Darknut : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;

    private EnemySprites _enemySprites;
    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = default;

    private int delay;
    Random rnd = new Random();

    public Darknut(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.DarknutMoveUp();
        delay = 0;
    }
    public virtual void Update(GameTime time)
    {
        if (delay == 10)
        {
            face = rnd.Next(4);
            switch (face)
            {
                case 0:
                    if (position.Y < 500)
                    {
                        position = new Vector2(position.X, position.Y - 3);
                        _sprite = _enemySprites.DarknutMoveUp();
                    }
                    else
                    {
                        goto case 1;
                    }
                    break;
                case 1:
                    if (position.X > 20)
                    {
                        position = new Vector2(position.X + 3, position.Y);
                        _sprite = _enemySprites.DarknutMoveRight();
                    }
                    else
                    {
                        goto case 2;
                    }
                    break;
                case 2:
                    if (position.Y > 20)
                    {
                        position = new Vector2(position.X, position.Y + 3);
                        _sprite = _enemySprites.DarknutMoveDown();
                    }
                    else
                    {
                        goto case 3;
                    }
                    break;
                case 3:
                    if (position.X < 500)
                    {
                        position = new Vector2(position.X - 3, position.Y);
                        _sprite = _enemySprites.DarknutMoveRight();
                    }
                    else
                    {
                        goto case 0;
                    }
                    break;
                default:
                    //not possible
                    break;
            }

            if (thrower != 0)
            {
                int attack = rnd.Next(4);
                if (attack == 4)
                {
                    thrower = 2;
                }
                else
                {
                    thrower = 1;
                }
            }

            _sprite.Update(time);
            delay = 0;
        }
        else
        {
            delay++;
        }
    }

    public void Draw()
    {
        _sprite.Draw(position);
    }
}


