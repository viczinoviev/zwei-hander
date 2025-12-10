using Microsoft.Xna.Framework.Content;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Gel enemy
/// </summary>
public class Gel : AbstractEnemy
{
    protected override int EnemyStartHealth => 5;

    public Gel(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        Sprite = enemySprites.Gel();
    }
}


