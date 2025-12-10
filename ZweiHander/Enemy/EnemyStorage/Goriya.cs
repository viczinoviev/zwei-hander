using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Goriya enemy
/// </summary>
public class Goriya : AbstractEnemy
{
    protected override int EnemyStartHealth => 5;
    private const int Attacking = 2;

    /// <summary>
    /// List of Sprites for this enemy
    /// <summary>
    public List<ISprite> _sprites = [];
    public IItem _currentProjectile;

    public int Thrower = 1;


    public Goriya(EnemySprites enemySprites, ItemManager projectileManager, ContentManager sfxPlayer, Vector2 position)
        : base(projectileManager, sfxPlayer, position)
    {
        //create list of all sprites associated with the enemy to swap with
        _sprites.Add(enemySprites.GoriyaUp());
        _sprites.Add(enemySprites.GoriyaRight());
        _sprites.Add(enemySprites.GoriyaDown());
        _sprites.Add(enemySprites.GoriyaLeft());
        Sprite = _sprites[0];
    }
    public override void Update(GameTime time)
    {
        base.Update(time);
        Sprite = _sprites[Face];
        //projectile handling
        EnemyHelper.GoriyaAttack(this, _projectileManager);
        _projectileManager.Update(time);
    }

    protected override void ChangeFace()
    {
        if (Thrower != Attacking)
        {
            base.ChangeFace();
        }
    }
}


