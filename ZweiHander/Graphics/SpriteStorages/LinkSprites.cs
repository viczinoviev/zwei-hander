using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class LinkSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/LinkDefinition.xml";
    private SpriteBatch _spriteBatch;
    public LinkSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }

    public ISprite LinkAttackSword() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation"]);
}

