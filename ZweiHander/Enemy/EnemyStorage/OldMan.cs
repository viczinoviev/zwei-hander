using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using ZweiHander.Graphics;
using System;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Not an enemy, but idk where to put for now. Old man.
/// </summary>
public class OldMan : IEnemy
{
    /// <summary>
    /// The sprite associated with this Enemy.
    /// </summary>
    protected ISprite _sprite;
/// <summary>
/// Holds all sprites for this NPC
/// </summary>
    private readonly NPCSprites _npcSprites;

    public Vector2 Position { get; set; } = default;

    public int Face { get; set; } = default;



    public OldMan(NPCSprites npcSprites)
    {
        _npcSprites = npcSprites;
        _sprite = _npcSprites.OldMan();
    }
    public virtual void Update(GameTime time)
    {
        _sprite.Update(time);
        }


    

    public void Draw()
    {
        _sprite.Draw(Position);
    }
}


