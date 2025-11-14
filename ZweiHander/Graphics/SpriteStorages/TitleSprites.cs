using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class TitleSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/TitleDefinition.xml";
    readonly SpriteBatch _spriteBatch;
    public TitleSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    
    public ISprite Title() => new IdleSprite(_regions["title"], _spriteBatch);
}

