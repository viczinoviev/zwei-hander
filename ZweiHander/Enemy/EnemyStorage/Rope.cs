using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Rope enemy
/// </summary>
public class Rope : AbstractEnemy
{
    private const int FaceLeft = 3;
    private const int FaceChangeHelper1 = 2;
    private const int FaceChangeHelper2 = 3;
    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];


    public Rope(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        _sprites.Add(enemySprites.RopeRight());
        _sprites.Add(enemySprites.RopeLeft());
        Sprite = _sprites[0];
        Face = 1;
    }
    public override void Update(GameTime time)
    {
        if (Face == 1 || Face == FaceLeft)
        {
            Sprite = _sprites[((Face * FaceChangeHelper1) + 1) % FaceChangeHelper2];
        }
        base.Update(time);
    }
}


