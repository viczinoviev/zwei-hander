using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class NPCSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/NPC-Definition.xml";
    private SpriteBatch _spriteBatch;
    public NPCSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite OldMan() => new IdleSprite(_regions["old-man-1"], _spriteBatch);
    public ISprite Merchant() => new IdleSprite(_regions["merchant-1"], _spriteBatch);
    public ISprite Zelda() => new IdleSprite(_regions["zelda-green-1"], _spriteBatch);
}

