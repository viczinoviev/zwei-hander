using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// BladeTrap enemy
/// </summary>
public class BladeTrap : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Holds all sprites for this enemy
/// </summary>
    private readonly EnemySprites _enemySprites;
    /// <summary>
    /// Time to attack
    /// </summary>
    private float attackTime = 0;

    /// <summary>
    /// Time to return from attack
    /// </summary>
    private float returnTime = 0;

    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = 1;
/// <summary>
/// Random number generator to randomize enemy behavior
/// </summary>
    readonly Random rnd = new();


    public BladeTrap(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.Trap();
    }
    public virtual void Update(GameTime time)
    {
        float dt = (float)time.ElapsedGameTime.TotalSeconds;
        //Randomize  movement
        if (thrower != 2)
        {
            int mov = rnd.Next(200);
            if (mov < 4)
            {
                face = mov;
                thrower = 2;
                attackTime = 1;
                returnTime = 3;
            }
        }
        else
        {
            if (attackTime > 0)
            {
                attackTime -= dt;
                Vector2 currentPos = position;
                position = new Vector2(position.X + ((-1 + 2 * Convert.ToInt32(!(face == 3 && position.X > 40))) * (3 * Convert.ToInt32((face == 3 && position.X > 40) || (face == 1 && position.X < 750)))), position.Y + ((-1 + 2 * Convert.ToInt32(!(face == 0 && position.Y > 40))) * (3 * Convert.ToInt32((face == 0 && position.Y > 40) || (face == 2 && position.Y < 400)))));
                if (currentPos.X == position.X && currentPos.Y == position.Y)
                {
                    returnTime -= dt * 3;
                }
            }
            else
            {
                if (returnTime > 0)
                {
                    returnTime -= dt;
                    position = new Vector2(position.X - ((-1 + 2 * Convert.ToInt32(!(face == 3))) * (1 * Convert.ToInt32((face == 3) || (face == 1)))), position.Y - ((-1 + 2 * Convert.ToInt32(!(face == 0))) * (1 * Convert.ToInt32((face == 0) || (face == 2)))));
                }
                else
                {
                    thrower = 1;
                }
            }
        }
        _sprite.Update(time);
        }


    

    public void Draw()
    {
        _sprite.Draw(position);
    }
}


