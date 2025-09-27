using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Vector2 = Microsoft.Xna.Framework.Vector2;

using ZweiHander.Graphics;
using System;
using System.Collections;
using System.Transactions;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy;

/// <summary>
/// Stalfos enemy
/// </summary>
public class Stalfos : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;

    private EnemySprites _enemySprites;
    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = default;

    Random rnd = new Random();
    
    public Stalfos(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
    }
    public virtual void Update(GameTime time)
    {
        face = rnd.Next(3);
        switch (face)
        {
            case 0:
                position = new Vector2(position.X, position.Y - 3);
                _sprite = _enemySprites.DarknutMoveUp();
                break;
            case 1:
                position = new Vector2(position.X + 3, position.Y);
                _sprite = _enemySprites.DarknutMoveRight();
                break;
            case 2:
                position = new Vector2(position.X, position.Y + 3);
                _sprite = _enemySprites.DarknutMoveDown();
                break;
            case 3:
                position = new Vector2(position.X - 3, position.Y);
                _sprite = _enemySprites.DarknutMoveRight();
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


