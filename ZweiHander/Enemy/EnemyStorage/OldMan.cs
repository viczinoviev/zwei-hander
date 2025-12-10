using Microsoft.Xna.Framework;
using ZweiHander.Graphics.SpriteStorages;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.Enemy.EnemyStorage;

/// <summary>
/// Not an enemy, but idk where to put for now. Old man. unused.
/// </summary>
public class OldMan : AbstractEnemy
{

    public OldMan(NPCSprites npcSprites)
        : base(null, null, Vector2.Zero)
    {
        Sprite = npcSprites.OldMan();
        //CollisionHandler = new EnemyCollisionHandler(this);
    }
    public override void Update(GameTime time)
    {
        CollisionHandler.UpdateCollisionBox();
        Sprite.Update(time);
    }
}


