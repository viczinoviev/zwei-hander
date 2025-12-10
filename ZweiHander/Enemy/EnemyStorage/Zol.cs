using Microsoft.Xna.Framework.Content;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// zol enemy
/// </summary>
public class Zol : AbstractEnemy
{
    public Zol(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        Sprite = enemySprites.Zol();
    }
}


