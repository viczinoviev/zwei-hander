using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using System.Runtime.CompilerServices;

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

    IItem _currentProjectile;

    private EnemySprites _enemySprites;

    private ItemManager _projectileManager;
    public Vector2 position { get; set; } = default;

    public int face { get; set; } = default;

    public int thrower { get; set; } = 1;

    Random rnd = new Random();


    public Darknut(EnemySprites enemySprites, ItemManager projectileManager)
    {
        _projectileManager = projectileManager;
        _enemySprites = enemySprites;
        _sprite = _enemySprites.DarknutMoveUp();
    }
    public virtual void Update(GameTime time)
    {
        if (thrower != 2 && thrower != 3)
        {
            int mov = rnd.Next(200);
            if (mov > 5)
            {
                position = new Vector2(position.X + ((-1 + 2 * Convert.ToInt32(!(face == 3 && position.X > 40))) * Convert.ToInt32((face == 3 && position.X > 40) || (face == 1 && position.X < 800))), position.Y + ((-1 + 2 * Convert.ToInt32(!(face == 0 && position.Y > 40))) * Convert.ToInt32((face == 0 && position.Y > 40) || (face == 2 && position.Y < 400))));
            }
            else
            {
                switch (mov)
                {
                    case 0:
                        if (position.Y > 40)
                        {
                            position = new Vector2(position.X, position.Y - 1);
                            _sprite = _enemySprites.DarknutMoveUp();
                            face = 0;
                        }
                        else
                        {
                            goto case 2;
                        }
                        break;
                    case 1:
                        if (position.X < 800)
                        {
                            position = new Vector2(position.X + 1, position.Y);
                            _sprite = _enemySprites.DarknutMoveRight();
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
                            _sprite = _enemySprites.DarknutMoveDown();
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
                            _sprite = _enemySprites.DarknutMoveLeft();
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
        }

        int attack = rnd.Next(500);
        if (attack == 5 && thrower != 2)
        {
            _currentProjectile = _projectileManager.GetItem(ItemType.Boomerang, -1, position: position);
            _currentProjectile.Life = 3;
            thrower = 2;
            (float v, float a) = ItemHelper.BoomerangTrajectory(50, 3);
            _currentProjectile.Velocity = new Vector2((-1 + 2 * Convert.ToInt32(!(face == 3))) * (v * Convert.ToInt32((face == 3) || (face == 1))), (-1 + 2 * Convert.ToInt32(!(face == 0))) * (v * Convert.ToInt32((face == 0) || (face == 2))));
            _currentProjectile.Acceleration = new Vector2((-1 + 2 * Convert.ToInt32(!(face == 3))) * (a * Convert.ToInt32((face == 3) || (face == 1))), (-1 + 2 * Convert.ToInt32(!(face == 0))) * (a * Convert.ToInt32((face == 0) || (face == 2))));
        }
        else
        {
            if (thrower == 2)
            {

                if (_currentProjectile.Life <= 0)
                {
                    thrower = 1;
                }
            }
        }
        _sprite.Update(time);
        _projectileManager.Update(time);
        }

    

    public void Draw()
    {
        _sprite.Draw(position);
        _projectileManager.Draw();
    }
}


