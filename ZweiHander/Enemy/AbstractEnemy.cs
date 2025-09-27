using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Vector2 = Microsoft.Xna.Framework.Vector2;

using ZweiHander.Graphics;
using System;
using System.Collections;
using System.Transactions;

namespace ZweiHander.Enemy;

/// <summary>
/// All Enemy classes implement this base class.
/// </summary>
public abstract class AbstractEnemy : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;

    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = default;

    Random rnd = new Random();
    public virtual void Update(GameTime time)
    {
        face = rnd.Next(3);
        switch (face)
        {
            case 0:
                position = new Vector2(position.X,position.Y - 3);
                break;
            case 1:
                position = new Vector2(position.X + 3,position.Y);
                break;
            case 2:
                position = new Vector2(position.X,position.Y + 3);
                break;
            case 3:
                position = new Vector2(position.X - 3, position.Y);
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
    }

    public void Draw()
    {
        _sprite.Draw(position);
    }
}