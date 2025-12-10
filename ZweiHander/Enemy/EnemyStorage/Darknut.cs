using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Darknut enemy
/// </summary>
public class Darknut : AbstractEnemy
{
    protected override int EnemyStartHealth => 15;
    public ISprite Sprite { get; set; } = default;

    private readonly List<ISprite> _sprites = [];


    public Darknut(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 position)
        :base(null, sfxPlayer, position)
    {
        Position = position;
        //create list of all sprites associated with the enemy to swap with
        _sprites.Add(enemySprites.DarknutMoveUp());
        _sprites.Add(enemySprites.DarknutMoveRight());
        _sprites.Add(enemySprites.DarknutMoveDown());
        _sprites.Add(enemySprites.DarknutMoveLeft());
        Sprite = _sprites[0];
    }
    public override void Update(GameTime time)
    {
        base.Update(time);
        //Change sprite to correct sprite
        Sprite = _sprites[Face];
    }
}


