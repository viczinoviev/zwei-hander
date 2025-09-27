using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class TreasureSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/TreasureDefinition.xml";
    private SpriteBatch _spriteBatch;
    public TreasureSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite Heart() => new IdleSprite(_regions["heart"], _spriteBatch);
    public ISprite HeartContainer() => new IdleSprite(_regions["heart-container"], _spriteBatch);
    public ISprite Fairy() => new IdleSprite(_regions["fairy"], _spriteBatch);
    public ISprite Rupy() => new IdleSprite(_regions["rupy"], _spriteBatch);
}


