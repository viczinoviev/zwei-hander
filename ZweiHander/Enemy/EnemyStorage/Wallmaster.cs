using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Wallmaster enemy
/// </summary>
public class Wallmaster : AbstractEnemy
{
    private const int FaceChangeHelper = 2;
    private const int FaceDown = 2;

    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];


    public Wallmaster(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        _sprites.Add(enemySprites.WallmasterUp());
        _sprites.Add(enemySprites.WallmasterDown());
        Sprite = _sprites[0];
    }
    public override void Update(GameTime time)
    {
        if (Face == 0 || Face == FaceDown)
        {
            Sprite = _sprites[Face / FaceChangeHelper];
        }
        base.Update(time);
    }
}


