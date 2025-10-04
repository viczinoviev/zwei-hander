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
    // Hearts
    public ISprite Heart() => new IdleSprite(_regions["heart"], _spriteBatch);
    public ISprite BlueHeart() => new IdleSprite(_regions["blue-heart"], _spriteBatch);
    public ISprite HeartContainer() => new IdleSprite(_regions["heart-container"], _spriteBatch);

    // Collectibles
    public ISprite Fairy() => new AnimatedSprite(_spriteBatch, _animations["fairy"]);
    public ISprite Clock() => new IdleSprite(_regions["clock"], _spriteBatch);
    public ISprite Rupy() => new IdleSprite(_regions["rupy"], _spriteBatch);
    public ISprite RupyFive() => new IdleSprite(_regions["rupy-five"], _spriteBatch);
    public ISprite Food() => new IdleSprite(_regions["food"], _spriteBatch);
    public ISprite Triforce() => new IdleSprite(_regions["triforce"], _spriteBatch);

    // Potions and Quest Items
    public ISprite PotionLife() => new IdleSprite(_regions["potion-life"], _spriteBatch);
    public ISprite Potion2nd() => new IdleSprite(_regions["potion-2nd"], _spriteBatch);
    public ISprite Letter() => new IdleSprite(_regions["letter"], _spriteBatch);
    public ISprite BookMagic() => new IdleSprite(_regions["book-magic"], _spriteBatch);

    // Swords
    public ISprite Sword() => new IdleSprite(_regions["sword"], _spriteBatch);
    public ISprite SwordWhite() => new IdleSprite(_regions["sword-white"], _spriteBatch);
    public ISprite SwordMagical() => new IdleSprite(_regions["sword-magical"], _spriteBatch);

    // Weapons and Tools
    public ISprite Shield() => new IdleSprite(_regions["shield"], _spriteBatch);
    public ISprite Boomerang() => new IdleSprite(_regions["boomerang"], _spriteBatch);
    public ISprite BoomerangMagical() => new IdleSprite(_regions["boomerang-magical"], _spriteBatch);
    public ISprite Bomb() => new IdleSprite(_regions["bomb"], _spriteBatch);
    public ISprite Bow() => new IdleSprite(_regions["bow"], _spriteBatch);
    public ISprite Arrow() => new IdleSprite(_regions["arrow"], _spriteBatch);
    public ISprite ArrowSilver() => new IdleSprite(_regions["arrow-silver"], _spriteBatch);
    public ISprite CandleBlue() => new IdleSprite(_regions["candle-blue"], _spriteBatch);
    public ISprite CandleRed() => new IdleSprite(_regions["candle-red"], _spriteBatch);
    public ISprite Recorder() => new IdleSprite(_regions["recorder"], _spriteBatch);
    public ISprite Raft() => new IdleSprite(_regions["raft"], _spriteBatch);
    public ISprite Ladder() => new IdleSprite(_regions["ladder"], _spriteBatch);
    public ISprite RodMagical() => new IdleSprite(_regions["rod-magical"], _spriteBatch);

    // Rings
    public ISprite RingBlue() => new IdleSprite(_regions["ring-blue"], _spriteBatch);
    public ISprite RingRed() => new IdleSprite(_regions["ring-red"], _spriteBatch);
    public ISprite BraceletPower() => new IdleSprite(_regions["bracelet-power"], _spriteBatch);

    // Dungeon Items
    public ISprite Key() => new IdleSprite(_regions["key"], _spriteBatch);
    public ISprite KeyMagical() => new IdleSprite(_regions["key-magical"], _spriteBatch);
    public ISprite Map() => new IdleSprite(_regions["map"], _spriteBatch);
    public ISprite Compass() => new IdleSprite(_regions["compass"], _spriteBatch);
}




