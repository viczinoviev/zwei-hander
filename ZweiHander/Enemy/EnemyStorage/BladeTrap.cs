using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;
using Microsoft.Xna.Framework.Content;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// BladeTrap enemy
/// </summary>
public class BladeTrap : IEnemy
{
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
    public int Hitpoints { get; set; } = 20000;

    public int Thrower;

    public float attackTime;

    public readonly BladeTrapHomeCollisionHandler home1CollisionHandler;
    public readonly BladeTrapHomeCollisionHandler home2CollisionHandler;
    public EnemyCollisionHandler CollisionHandler { get; } = default;

    public BladeTrap(EnemySprites enemySprites,ContentManager sfxPlayer,Vector2 pos)
    {
        Position = pos;
        _enemySprites = enemySprites;
        Sprite = _enemySprites.Trap();
        CollisionHandler = new EnemyCollisionHandler(this, sfxPlayer);
        home1CollisionHandler = new BladeTrapHomeCollisionHandler(this, 'x');
        home2CollisionHandler = new BladeTrapHomeCollisionHandler(this, 'y');
        originalPosition = pos;
    }
    public virtual void Update(GameTime time)
    {
        if (Thrower == 1)
        {
                float dt = (float)time.ElapsedGameTime.TotalSeconds;
                EnemyHelper.bladeTrapAttack(this, dt);
        }
        else if (Thrower == 2)
        {
            EnemyHelper.bladeTrapReturn(this);
        }
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
        }




    public void Draw()
    {
        Sprite.Draw(Position);
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


