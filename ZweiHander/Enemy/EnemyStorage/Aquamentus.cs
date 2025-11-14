using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Items;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.CollisionFiles;
using ZweiHander.Items.ItemStorages;
using Microsoft.Xna.Framework.Content;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Aquamentus enemy
/// </summary>
public class Aquamentus : IEnemy
{
    public ISprite Sprite { get; set; }
    /// <summary>
    /// Projectiles for this enemy
    /// </summary>
    public List<IItem> _projectiles = [];
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    private readonly BossSprites _bossSprites;
    /// <summary>
    /// Manager for the projectile this enemy throws
    /// </summary>
    private readonly ItemManager _projectileManager;
    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

    public int Hitpoints { get; set; } = 15;

    public EnemyCollisionHandler CollisionHandler { get; } = default;
    public int Thrower = 1;
    /// <summary>
    /// Random number generator to randomize enemy behavior
    /// </summary>
    public readonly Random rnd = new();


    public Aquamentus(BossSprites bossSprites, ItemManager projectileManager,ContentManager sfxPlayer)
    {
        _projectileManager = projectileManager;
        _bossSprites = bossSprites;
        Sprite = _bossSprites.Aquamentus();
        CollisionHandler = new EnemyCollisionHandler(this,sfxPlayer);
    }
    public virtual void Update(GameTime time)
    {
        
            //Randomize  movement
            int mov = rnd.Next(200);
            //Move according to current direction faced
            if (mov > 3)
            {
                Position = EnemyHelper.BehaveFromFace(this,1,0);
            }
            //Change face to new value according to the randomized value
            else
            {
            Face = mov;
            }

        //projectile attacking
        EnemyHelper.aquamentusAttack(this, _projectileManager);
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
        _projectileManager.Update(time);
        }



    public void Draw()
    {
        Sprite.Draw(Position);
        _projectileManager.Draw();
    }
    public Rectangle GetCollisionBox()
    {
        // Sprites are centered
        return new Rectangle(
                (int)Position.X - Sprite.Width / 2,
                (int)Position.Y - Sprite.Height / 2,
                Sprite.Width,
                Sprite.Height
        );
    }
}


