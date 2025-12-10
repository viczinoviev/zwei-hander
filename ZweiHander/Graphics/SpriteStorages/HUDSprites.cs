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

    public ISprite XSymbol() => new IdleSprite(_regions["x-symbol"], _spriteBatch);
    public ISprite Rupy() => new IdleSprite(_regions["rupy"], _spriteBatch);
    public ISprite Key() => new IdleSprite(_regions["key"], _spriteBatch);
    public ISprite BlueKey() => new IdleSprite(_regions["blue_key"], _spriteBatch);
    public ISprite InventoryDisplay() => new IdleSprite(_regions["inventory-display"], _spriteBatch);
    public ISprite MapDisplay() => new IdleSprite(_regions["map-display"], _spriteBatch);
    public ISprite HeadsUpHUD() => new IdleSprite(_regions["heads-up-hud"], _spriteBatch);
    public ISprite HeartFull() => new IdleSprite(_regions["heart-full"], _spriteBatch);
    public ISprite HeartHalf() => new IdleSprite(_regions["heart-half"], _spriteBatch);
    public ISprite HeartEmpty() => new IdleSprite(_regions["heart-empty"], _spriteBatch);
    public ISprite NormalSword() => new IdleSprite(_regions["normal-sword"], _spriteBatch);
    public ISprite Bow() => new IdleSprite(_regions["bow"], _spriteBatch);
    public ISprite Digit(int digit) => new IdleSprite(_regions[digit.ToString()], _spriteBatch);
    public ISprite Number(int number, int digits = -1) => new NumberSprite(number, this, digits);
    public ISprite Map() => new IdleSprite(_regions["map"], _spriteBatch);
    public ISprite OrangeCandle() => new IdleSprite(_regions["orange-candle"], _spriteBatch);
    public ISprite BlueCandle() => new IdleSprite(_regions["blue-candle"], _spriteBatch);
    public ISprite RedPotion() => new IdleSprite(_regions["red-potion"], _spriteBatch);
    public ISprite BluePotion() => new IdleSprite(_regions["blue-potion"], _spriteBatch);
    public ISprite BlueFrame() => new IdleSprite(_regions["blue-frame"], _spriteBatch);
    public ISprite RedFrame() => new IdleSprite(_regions["red-frame"], _spriteBatch);
    public ISprite NormalBoomerang() => new IdleSprite(_regions["normal-boomerang"], _spriteBatch);
    public ISprite Bomb() => new IdleSprite(_regions["bomb"], _spriteBatch);

    // Minimap sprites
    public ISprite MinimapNode(string connections)
    {
        string key = "map-node";
        if (!string.IsNullOrEmpty(connections))
        {
            key += "-" + connections.ToLower();
        }
        return _regions.TryGetValue(key, out TextureRegion value) ? new IdleSprite(value, _spriteBatch) : new IdleSprite(_regions["map-node"], _spriteBatch);
    }

    public ISprite MinimapUpper() => new IdleSprite(_regions["minimap-upper"], _spriteBatch);
    public ISprite MinimapLower() => new IdleSprite(_regions["minimap-lower"], _spriteBatch);
    public ISprite MinimapBoth() => new IdleSprite(_regions["minimap-both"], _spriteBatch);

    public ISprite MapPlayer() => new IdleSprite(_regions["map-player"], _spriteBatch);
    public ISprite MinimapPlayer() => new IdleSprite(_regions["minimap-player"], _spriteBatch);
    public ISprite MinimapTriforce() => new IdleSprite(_regions["minimap-triforce"], _spriteBatch);

    public ISprite Compass() => new IdleSprite(_regions["compass"], _spriteBatch);
}
