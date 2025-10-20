using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Darknut enemy
/// </summary>
public class Darknut : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Holds all sprites for this enemy
/// </summary>
    private readonly EnemySprites _enemySprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    readonly EnemyCollisionHandler _collisionHandler;

/// <summary>
/// Random number generator to randomize enemy behavior
/// </summary>
    readonly Random rnd = new();


    public Darknut(EnemySprites enemySprites)
    {
        _enemySprites = enemySprites;
        _sprite = _enemySprites.DarknutMoveUp();
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        //Randomize  movement
        int mov = rnd.Next(200);
        //Move according to current direction faced
        if (mov > 5)
        {
            Position = EnemyHelper.BehaveFromFace(this, 1);
        }
        //Change face and sprite to new value according to the randomized value
        else
        {
            switch (mov)
            {
                case 0:
                        _sprite = _enemySprites.DarknutMoveUp();
                        Face = 0;
                    break;
                case 1:
                        _sprite = _enemySprites.DarknutMoveRight();
                        Face = 1;
                    break;
                case 2:
                        _sprite = _enemySprites.DarknutMoveDown();
                        Face = 2;
                    break;
                case 3:
                        _sprite = _enemySprites.DarknutMoveLeft();
                        Face = 3;
                    break;
                default:
                    //no movement  
                    break;
            }
        }
        _collisionHandler.UpdateCollisionBox();
        _sprite.Update(time);
        }


    

    public void Draw()
    {
        _sprite.Draw(Position);
    }
}


