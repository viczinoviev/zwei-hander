using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ZweiHander.CollisionFiles;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// BladeTrap enemy
/// </summary>
public class BladeTrap : IEnemy
{
    private const int EnemyStartHealth = 2000000;
    private const int CollisionBoxOffset = 2;
    private const int Attacking = 2;
    public ISprite Sprite { get; set; } = default;
    /// <summary>
    /// Holds all sprites for this enemy
    /// </summary>
    private readonly EnemySprites _enemySprites;
    /// <summary>
    /// position for the trap to return to
    /// </summary>
    public Vector2 originalPosition;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = EnemyStartHealth;
    public float HitcoolDown { get; set; } = 0;

    public int Thrower;

    public float attackTime;

    public readonly BladeTrapHomeCollisionHandler homeRightCollisionHandler;
    public readonly BladeTrapHomeCollisionHandler homeUpCollisionHandler;
    public readonly BladeTrapHomeCollisionHandler homeLeftCollisionHandler;
    public readonly BladeTrapHomeCollisionHandler homeDownCollisionHandler;
    public EnemyCollisionHandler CollisionHandler { get; } = default;

    public BladeTrap(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 pos)
    {
        Position = pos;
        _enemySprites = enemySprites;
        Sprite = _enemySprites.Trap();
        CollisionHandler = new EnemyCollisionHandler(this, sfxPlayer);
        homeRightCollisionHandler = new BladeTrapHomeCollisionHandler(this, "xr");
        homeUpCollisionHandler = new BladeTrapHomeCollisionHandler(this, "yu");
        homeLeftCollisionHandler = new BladeTrapHomeCollisionHandler(this, "xl");
        homeDownCollisionHandler = new BladeTrapHomeCollisionHandler(this, "yd");
        originalPosition = pos;
    }
    public virtual void Update(GameTime time)
    {
        if (Thrower == 1)
        {
            float dt = (float)time.ElapsedGameTime.TotalSeconds;
            EnemyHelper.BladeTrapAttack(this, dt);
        }
        else if (Thrower == Attacking)
        {
            EnemyHelper.BladeTrapReturn(this);
        }
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
    }

    public void TakeDamage(int dmg)
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


    public void Draw()
    {
        Sprite.Draw(Position);
    }
    public Rectangle GetCollisionBox()
    {
        // Sprites are centered
        return new Rectangle(
                (int)Position.X - (Sprite.Width / CollisionBoxOffset),
                (int)Position.Y - (Sprite.Height / CollisionBoxOffset),
                Sprite.Width,
                Sprite.Height
        );
    }

}


