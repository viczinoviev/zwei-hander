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
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    private readonly EnemySprites _enemySprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = 5;

    readonly EnemyCollisionHandler _collisionHandler;

    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    readonly Random rnd = new();


    public Keese(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        Sprite = _enemySprites.Keese();
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
                Position = EnemyHelper.BehaveFromFace(this, 1,0);
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
            Face = mov;
        }
        _collisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
        }




    public void Draw()
    {
        Sprite.Draw(Position);
    }
    public Rectangle GetCollisionBox()
    {
        return new Rectangle(
                (int)(Position.X - Sprite.Width),
                (int)(Position.Y - Sprite.Height),
                Sprite.Width + 15,
                Sprite.Height + 15
            );
    }
}


