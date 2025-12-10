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
/// Aquamentus enemy
/// </summary>
public class Aquamentus : AbstractEnemy
{
    public int Thrower = 1;
    protected override int EnemyStartHealth => 50;

    public Aquamentus(BossSprites bossSprites, ItemManager projectileManager, ContentManager sfxPlayer, Vector2 position)
        : base(projectileManager, sfxPlayer, position)
    {
        Sprite = bossSprites.Aquamentus();
    }
    public override void Update(GameTime time)
    {
        base.Update(time);

        //projectile attacking
        EnemyHelper.AquamentusAttack(this, _projectileManager);
        _projectileManager.Update(time);
    }
}


