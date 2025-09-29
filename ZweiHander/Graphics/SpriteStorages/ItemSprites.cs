using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class ItemSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/LinkDefinition.xml";
    private SpriteBatch _spriteBatch;
    public ItemSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }

    public ISprite ArrowUp() => new IdleSprite(_regions["arrow-up"], _spriteBatch);
    public ISprite ArrowDown() { 
        IdleSprite s = new IdleSprite(_regions["arrow-up"], _spriteBatch);
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    } 
    public ISprite ArrowLeft() => new IdleSprite(_regions["arrow-right"], _spriteBatch);
    public ISprite ArrowRight()
    {
        IdleSprite s = new IdleSprite(_regions["arrow-right"], _spriteBatch);
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }
    public ISprite Boomerang() => new AnimatedSprite(_spriteBatch, _animations["boomerang-animation"]);

}

