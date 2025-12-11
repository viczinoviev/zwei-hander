using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;

public class HUDSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/HUDDefinition.xml";

    public SpriteBatch SpriteBatch { get; }

    public HUDSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        SpriteBatch = spriteBatch;
    }

    public ISprite XSymbol() => new IdleSprite(_regions["x-symbol"], SpriteBatch);
    public ISprite Rupy() => new IdleSprite(_regions["rupy"], SpriteBatch);
    public ISprite Key() => new IdleSprite(_regions["key"], SpriteBatch);
    public ISprite BlueKey() => new IdleSprite(_regions["blue_key"], SpriteBatch);
    public ISprite InventoryDisplay() => new IdleSprite(_regions["inventory-display"], SpriteBatch);
    public ISprite MapDisplay() => new IdleSprite(_regions["map-display"], SpriteBatch);
    public ISprite HeadsUpHUD() => new IdleSprite(_regions["heads-up-hud"], SpriteBatch);
    public ISprite HeartFull() => new IdleSprite(_regions["heart-full"], SpriteBatch);
    public ISprite HeartHalf() => new IdleSprite(_regions["heart-half"], SpriteBatch);
    public ISprite HeartEmpty() => new IdleSprite(_regions["heart-empty"], SpriteBatch);
    public ISprite NormalSword() => new IdleSprite(_regions["normal-sword"], SpriteBatch);
    public ISprite Bow() => new IdleSprite(_regions["bow"], SpriteBatch);
    public ISprite Digit(int digit) => new IdleSprite(_regions[digit.ToString()], SpriteBatch);
    public ISprite Number(int number, int digits = -1) => new NumberSprite(number, this, digits);
    public ISprite Map() => new IdleSprite(_regions["map"], SpriteBatch);
    public ISprite OrangeCandle() => new IdleSprite(_regions["orange-candle"], SpriteBatch);
    public ISprite BlueCandle() => new IdleSprite(_regions["blue-candle"], SpriteBatch);
    public ISprite RedPotion() => new IdleSprite(_regions["red-potion"], SpriteBatch);
    public ISprite BluePotion() => new IdleSprite(_regions["blue-potion"], SpriteBatch);
    public ISprite BlueFrame() => new IdleSprite(_regions["blue-frame"], SpriteBatch);
    public ISprite RedFrame() => new IdleSprite(_regions["red-frame"], SpriteBatch);
    public ISprite NormalBoomerang() => new IdleSprite(_regions["normal-boomerang"], SpriteBatch);
    public ISprite Bomb() => new IdleSprite(_regions["bomb"], SpriteBatch);

    // Minimap sprites
    public ISprite MinimapNode(string connections)
    {
        string key = "map-node";
        if (!string.IsNullOrEmpty(connections))
        {
            key += "-" + connections.ToLower();
        }
        return _regions.TryGetValue(key, out TextureRegion value) ? new IdleSprite(value, SpriteBatch) : new IdleSprite(_regions["map-node"], SpriteBatch);
    }

    public ISprite MinimapUpper() => new IdleSprite(_regions["minimap-upper"], SpriteBatch);
    public ISprite MinimapLower() => new IdleSprite(_regions["minimap-lower"], SpriteBatch);
    public ISprite MinimapBoth() => new IdleSprite(_regions["minimap-both"], SpriteBatch);

    public ISprite MapPlayer() => new IdleSprite(_regions["map-player"], SpriteBatch);
    public ISprite MinimapPlayer() => new IdleSprite(_regions["minimap-player"], SpriteBatch);
    public ISprite MinimapTriforce() => new IdleSprite(_regions["minimap-triforce"], SpriteBatch);

    public ISprite Compass() => new IdleSprite(_regions["compass"], SpriteBatch);
}
