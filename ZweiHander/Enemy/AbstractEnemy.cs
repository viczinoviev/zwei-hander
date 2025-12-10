using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Damage;
using ZweiHander.Graphics;
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
    public EffectManager Effects { get; set; }

    /// <summary>
    /// Chance is 4 divided by this
    /// </summary>
    protected virtual int FaceChangeChance => 200;

    protected readonly int Faces = 4;

    public List<DamageDisplay> DamageNumbers { get; set; } = [];

    protected const double DamageDisplayDuration = 1;

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
        Effects = [];
    }

    public virtual void Draw()
    {
        Color color = Sprite.Color;
        color.R = (byte)(Effects.Contains(Effect.Slowed) ? 0x8f : 0xff);
        color.B = (byte)(Effects.Contains(Effect.OnFire) ? 0x8f : 0xff);
        Sprite.Color = color;
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
        HandleEffects(time);
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
            Position = EnemyHelper.BehaveFromFace(this, Effects.Contains(Effect.Slowed) ? 0.3f : 1f, 0);
        }
    }

    protected virtual void HandleEffects(GameTime time)
    {
        Effects.Update(time);
        foreach (Effect effect in Effects.Ticked)
        {
            switch (effect)
            {
                case Effect.OnFire:
                    TakeDamage(1);
                    break;
            }
        }
    }

    public virtual Rectangle GetCollisionBox()
    {
        //Type type = GetType();
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
        double angle = 2 * Math.PI * rnd.NextDouble();
        DamageNumbers.Add(new(dmg,
            Position + (
                (float)Double.Hypot(Sprite.Width, Sprite.Height) * 0.6f *
                new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))
            ),
            DamageDisplayDuration
        ));
    }

    public virtual void TakeDamage(DamageObject dmg)
    {
        TakeDamage(dmg.Damage);
        foreach (var (effect, duration) in dmg.Effects)
        {
            Effects[effect] = duration;
        }
    }
}
