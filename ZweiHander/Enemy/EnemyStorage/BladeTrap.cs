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
public class BladeTrap : AbstractEnemy
{
    private const int Attacking = 2;
    protected override int EnemyStartHealth => 2000000;

    /// <summary>
    /// position for the trap to return to
    /// </summary>
    public Vector2 originalPosition;

    public int Thrower;

    public float attackTime;

    public readonly BladeTrapHomeCollisionHandler homeRightCollisionHandler;
    public readonly BladeTrapHomeCollisionHandler homeUpCollisionHandler;
    public readonly BladeTrapHomeCollisionHandler homeLeftCollisionHandler;
    public readonly BladeTrapHomeCollisionHandler homeDownCollisionHandler;

    public BladeTrap(EnemySprites enemySprites, ContentManager sfxPlayer, Vector2 pos)
        : base(null, sfxPlayer, pos)
    {
        Sprite = enemySprites.Trap();
        homeRightCollisionHandler = new BladeTrapHomeCollisionHandler(this, "xr");
        homeUpCollisionHandler = new BladeTrapHomeCollisionHandler(this, "yu");
        homeLeftCollisionHandler = new BladeTrapHomeCollisionHandler(this, "xl");
        homeDownCollisionHandler = new BladeTrapHomeCollisionHandler(this, "yd");
        originalPosition = pos;
    }
    public override void Update(GameTime time)
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
}


