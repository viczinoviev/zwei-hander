using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Not an enemy, but idk where to put for now. Old man.
/// </summary>
public class OldMan : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    public ISprite _sprite { get; set; } = default;
/// <summary>
/// Holds all sprites for this NPC
/// </summary>
    private readonly NPCSprites _npcSprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;

readonly EnemyCollisionHandler _collisionHandler;

    public OldMan(NPCSprites npcSprites)
    {
        _npcSprites = npcSprites;
        _sprite = _npcSprites.OldMan();
        _collisionHandler = new EnemyCollisionHandler(this);
    }
    public virtual void Update(GameTime time)
    {
        _collisionHandler.UpdateCollisionBox();
        _sprite.Update(time);
        }




    public void Draw()
    {
        _sprite.Draw(Position);
    }
    public Rectangle GetCollisionBox()
    {
        return new Rectangle(
                (int)(Position.X - _sprite.Width),
                (int)(Position.Y - _sprite.Height),
                _sprite.Width + 15,
                _sprite.Height + 15
            );
    }
}


