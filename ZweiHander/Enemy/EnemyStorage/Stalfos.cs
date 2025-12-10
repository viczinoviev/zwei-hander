using Microsoft.Xna.Framework.Content;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Stalfos enemy
/// </summary>
public class Stalfos : AbstractEnemy
{
    public Stalfos(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        Sprite = enemySprites.Stalfos();
    }
}


