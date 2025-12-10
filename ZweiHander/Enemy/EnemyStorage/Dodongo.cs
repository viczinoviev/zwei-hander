using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Dodongo boss
/// </summary>
public class Dodongo : AbstractEnemy
{

    protected override int EnemyStartHealth => 50;
    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];


    public Dodongo(BossSprites bossSprites, ContentManager sfxPlayer, Vector2 position)
        : base(null, sfxPlayer, position)
    {
        Position = position;
        _sprites.Add(bossSprites.DodongoUp());
        _sprites.Add(bossSprites.DodongoRight());
        _sprites.Add(bossSprites.DodongoDown());
        _sprites.Add(bossSprites.DodongoLeft());
        Sprite = _sprites[0];
    }
    public override void Update(GameTime time)
    {
        base.Update(time);
        Sprite = _sprites[Face];
    }
}


