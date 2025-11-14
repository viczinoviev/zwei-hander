using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Not an enemy, but idk where to put for now. Old man.
/// </summary>
public class OldMan : IEnemy
{
    public ISprite Sprite { get; set; } = default;

    /// <summary>
    /// Holds all sprites for this NPC
    /// </summary>
    private readonly NPCSprites _npcSprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;
    public int Hitpoints { get; set; } = 5;

    public EnemyCollisionHandler CollisionHandler { get; } = default;

    public OldMan(NPCSprites npcSprites)
    {
        _npcSprites = npcSprites;
        Sprite = _npcSprites.OldMan();
        //CollisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
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


