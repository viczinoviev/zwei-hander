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

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    public int Thrower { get; set; } = 1;
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
        if (Thrower != 2)
        {
            int mov = rnd.Next(200);
            if (mov < 4)
            {
                Face = mov;
                Thrower = 2;
                attackTime = 1;
                returnTime = 3;
            }
        }
        else
        {
            if (attackTime > 0)
            {
                attackTime -= dt;
                Vector2 currentPos = Position;
                Position = new Vector2(Position.X + ((-1 + 2 * Convert.ToInt32(!(Face == 3 && Position.X > 40))) * (3 * Convert.ToInt32((Face == 3 && Position.X > 40) || (Face == 1 && Position.X < 750)))), Position.Y + ((-1 + 2 * Convert.ToInt32(!(Face == 0 && Position.Y > 40))) * (3 * Convert.ToInt32((Face == 0 && Position.Y > 40) || (Face == 2 && Position.Y < 400)))));
                if (currentPos.X == Position.X && currentPos.Y == Position.Y)
                {
                    returnTime -= dt * 3;
                }
            }
            else
            {
                if (returnTime > 0)
                {
                    returnTime -= dt;
                    Position = new Vector2(Position.X - ((-1 + 2 * Convert.ToInt32(!(Face == 3))) * (1 * Convert.ToInt32((Face == 3) || (Face == 1)))), Position.Y - ((-1 + 2 * Convert.ToInt32(!(Face == 0))) * (1 * Convert.ToInt32((Face == 0) || (Face == 2)))));
                }
                else
                {
                    Thrower = 1;
                }
            }
        }
        _sprite.Update(time);
        }


    

    public void Draw()
    {
        _sprite.Draw(Position);
    }
}


