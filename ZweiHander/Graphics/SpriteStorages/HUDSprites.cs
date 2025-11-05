using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;

public class HUDSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/HUDDefinition.xml";
    readonly SpriteBatch _spriteBatch;

    public HUDSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }

    public ISprite HeartFull() => new IdleSprite(_regions["heart-full"], _spriteBatch);
    public ISprite HeartHalf() => new IdleSprite(_regions["heart-half"], _spriteBatch);
    public ISprite Map() => new IdleSprite(_regions["map"], _spriteBatch);
    public ISprite ItemSlotA() => new IdleSprite(_regions["item-slot-a"], _spriteBatch);
    public ISprite ItemSlotB() => new IdleSprite(_regions["item-slot-b"], _spriteBatch);
    public ISprite Rupy() => new IdleSprite(_regions["rupy"], _spriteBatch);
    public ISprite Key() => new IdleSprite(_regions["key"], _spriteBatch);
    public ISprite Bomb() => new IdleSprite(_regions["bomb"], _spriteBatch);
}
