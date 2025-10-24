using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Dodongo boss
/// </summary>
public class Dodongo : IEnemy
{
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Holds all sprites for this boss
    /// </summary>
    private readonly BossSprites _bossSprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    public int Hitpoints { get; set; } = 5;

    readonly EnemyCollisionHandler _collisionHandler;

    /// <summary>
    /// Random number generator to randomize boss behavior
    /// </summary>
    readonly Random rnd = new();


    public Dodongo(BossSprites bossSprites)
    {
        _bossSprites = bossSprites;
        Sprite = _bossSprites.DodongoUp();
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        //Randomize  movement
        int mov = rnd.Next(200);
        //Move according to current direction faced
        if (mov > 3)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1);
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
                        Sprite = _bossSprites.DodongoUp();
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
                        Sprite = _bossSprites.DodongoRight();
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
                        Sprite = _bossSprites.DodongoDown();
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
                        Sprite = _bossSprites.DodongoLeft();
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


