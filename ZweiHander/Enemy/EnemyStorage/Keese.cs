using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Keese enemy
/// </summary>
public class Keese : AbstractEnemy
{
    protected override int EnemyStartHealth => 5;
    private const int BasicDirections = 4;
    private const int SouthEast = 4;
    private const int NorthEast = 5;
    private const int SouthWest = 6;
    private const int NorthWest = 7;


    public Keese(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        Sprite = enemySprites.Keese();
    }

    protected override void ChangeFace()
    {
        //Randomize  movement
        int mov = rnd.Next(FaceChangeChance);
        //Move according to current direction faced
        if (mov > Faces)
        {
            if (Face < BasicDirections)
            {
                Position = EnemyHelper.BehaveFromFace(this, 1, 0);
            }
            else
            {
                switch (Face)
                {
                    case SouthEast:
                        Position = new Vector2(Position.X + 1, Position.Y + 1);
                        break;
                    case NorthEast:
                        Position = new Vector2(Position.X + 1, Position.Y - 1);
                        break;
                    case SouthWest:
                        Position = new Vector2(Position.X - 1, Position.Y + 1);
                        break;
                    case NorthWest:
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
    }
}


