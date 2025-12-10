using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;

namespace ZweiHander.Enemy;
public class AbstractEnemy : IEnemy
{
    protected virtual int EnemyStartHealth => 5;
    public Vector2 Position { get; set; }
    public int Face { get; set; }
    public int Hitpoints { get; set; }
    public float HitcoolDown { get; set; }
    public EnemyCollisionHandler CollisionHandler { get; }
    protected ISprite Sprite { get; set; }

    public Random rnd = new();

    /// <summary>
    /// Chance is 4 divided by this
    /// </summary>
    protected virtual int FaceChangeChance => 200;

    protected readonly int Faces = 4;

    /// <summary>
    /// Manager for the projectile this enemy throws
    /// </summary>
    protected readonly ItemManager _projectileManager;

    protected AbstractEnemy(ItemManager projectileManager, ContentManager sfxPlayer, Vector2 position)
    {
        Position = position;
        _projectileManager = projectileManager;
        Hitpoints = EnemyStartHealth;
        CollisionHandler = new EnemyCollisionHandler(this, sfxPlayer);
    }

    public virtual void Draw()
    {
        Sprite.Draw(Position);
        _projectileManager?.Draw();
    }

    public virtual void Update(GameTime time)
    {
        if (HitcoolDown > 0)
        {
            HitcoolDown--;
        }
        ChangeFace();
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
    }

    protected virtual void ChangeFace()
    {
        //Randomize  movement
        int mov = rnd.Next(FaceChangeChance);
        //Change face to new value according to the randomized value
        if (mov < Faces)
        {
            Face = mov;
        }
        //Move according to current direction faced
        else
        {
            Position = EnemyHelper.BehaveFromFace(this, 1, 0);
        }
    }

    public virtual Rectangle GetCollisionBox()
    {
        Type type = GetType();
        // Sprites are centered
        return new Rectangle(
                (int)Position.X - (Sprite.Width / 2),
                (int)Position.Y - (Sprite.Height / 2),
                Sprite.Width,
                Sprite.Height
        );
    }

    public virtual void TakeDamage(int dmg)
    {
        Hitpoints -= dmg;

        if (Hitpoints <= 0)
        {
            if (CollisionHandler != null)
            {
                CollisionHandler.Dead = true;
            }
        }
    }
}
