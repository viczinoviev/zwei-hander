using Microsoft.Xna.Framework;

namespace ZweiHander.Enemy;


/// <summary>
/// Contains waves of enemies to create.
/// </summary>
static class WaveConstuctor
{
    private const int xLeft = 150;
    private const int yUp = 100;
    private const int  xRight = 500;
    private const int yDown = 500;
    public static void CreateWave(EnemyManager Horde,int waveNum)
    {
        switch (waveNum)
        {
            case 1:
            CreateWave1(Horde);
            break;
            case 2:
            CreateWave2(Horde);
            break;
            case 3:
            CreateWave3(Horde);
            break;
            case 4:
            CreateWave4(Horde);
            break;
            case 5:
            CreateWave5(Horde);
            break;
            case 6:
            CreateWave6(Horde);
            break;
            case 7:
            CreateWave7(Horde);
            break;
            case 8:
            CreateWave8(Horde);
            break;
            case 9:
            CreateWave9(Horde);
            break;
            default:
            CreateWaveFinal(Horde);
            break;
        }
    }


    public static void CreateWave1(EnemyManager Horde)
    {
        Horde.MakeEnemy("Gel",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Gel",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Gel",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Gel",new Vector2(xRight,yDown));
    }

    public static void CreateWave2(EnemyManager Horde)
    {
        Horde.MakeEnemy("Gel",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Stalfos",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Rope",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Gel",new Vector2(xRight,yDown));
    }
    public static void CreateWave3(EnemyManager Horde)
    {
        Horde.MakeEnemy("Keese",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Keese",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Keese",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Keese",new Vector2(xRight,yDown));
    }
    public static void CreateWave4(EnemyManager Horde)
    {
        Horde.MakeEnemy("Darknut",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Stalfos",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Stalfos",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Goriya",new Vector2(xRight,yDown));
    }
    public static void CreateWave5(EnemyManager Horde)
    {
        Horde.MakeEnemy("Goriya",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Goriya",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Goriya",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Aquamentus",new Vector2(xRight,yDown));
    }
    public static void CreateWave6(EnemyManager Horde)
    {
        Horde.MakeEnemy("Wallmaster",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Zol",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Keese",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Goriya",new Vector2(xRight,yDown));
    }
    public static void CreateWave7(EnemyManager Horde)
    {
        Horde.MakeEnemy("Darknut",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Darknut",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Goriya",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Zol",new Vector2(xRight,yDown));
    }
    public static void CreateWave8(EnemyManager Horde)
    {
        Horde.MakeEnemy("Dodongo",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Zol",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Zol",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Dodongo",new Vector2(xRight,yDown));
    }
    public static void CreateWave9(EnemyManager Horde)
    {
        Horde.MakeEnemy("Dodongo",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Goriya",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Aquamentus",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Darknut",new Vector2(xRight,yDown));
    }
    public static void CreateWaveFinal(EnemyManager Horde)
    {
        Horde.MakeEnemy("Dodongo",new Vector2(xLeft,yUp));
        Horde.MakeEnemy("Dodongo",new Vector2(xLeft,yDown));
        Horde.MakeEnemy("Aquamentus",new Vector2(xRight,yUp));
        Horde.MakeEnemy("Aquamentus",new Vector2(xRight,yDown));
    }
}