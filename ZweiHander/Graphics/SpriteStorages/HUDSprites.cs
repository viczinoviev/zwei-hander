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

    public ISprite InventoryDisplay() => new IdleSprite(_regions["inventory-display"], _spriteBatch);
    public ISprite MapDisplay() => new IdleSprite(_regions["map-display"], _spriteBatch);
    public ISprite HeadsUpHUD() => new IdleSprite(_regions["heads-up-hud"], _spriteBatch);
    public ISprite HeartFull() => new IdleSprite(_regions["heart-full"], _spriteBatch);
    public ISprite HeartHalf() => new IdleSprite(_regions["heart-half"], _spriteBatch);
    public ISprite HeartEmpty() => new IdleSprite(_regions["heart-empty"], _spriteBatch);
    public ISprite NormalSword() => new IdleSprite(_regions["normal-sword"], _spriteBatch);
    public ISprite Bow() => new IdleSprite(_regions["bow"], _spriteBatch);
    public ISprite Digit(int digit) => new IdleSprite(_regions[digit.ToString()], _spriteBatch);
    public ISprite Number(int number, int digits = -1) => new NumberSprite(number, _spriteBatch, this, digits);
    public ISprite Map() => new IdleSprite(_regions["map"], _spriteBatch);
    public ISprite OrangeCandle() => new IdleSprite(_regions["orange-candle"], _spriteBatch);
    public ISprite BlueFrame() => new IdleSprite(_regions["blue-frame"], _spriteBatch);
    public ISprite RedFrame() => new IdleSprite(_regions["red-frame"], _spriteBatch);
    public ISprite NormalBoomerang() => new IdleSprite(_regions["normal-boomerang"], _spriteBatch);
    public ISprite Bomb() => new IdleSprite(_regions["bomb"], _spriteBatch);
}
