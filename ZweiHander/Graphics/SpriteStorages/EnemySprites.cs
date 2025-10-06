using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;

public class EnemySprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/EnemyDefinition.xml";
    private SpriteBatch _spriteBatch;
    public EnemySprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite DarknutMoveDown() => new AnimatedSprite(_spriteBatch, _animations["darknut-move-animation-down"]);
    public ISprite DarknutMoveUp() => new AnimatedSprite(_spriteBatch, _animations["darknut-move-animation-up"]);
    public ISprite DarknutMoveRight() => new AnimatedSprite(_spriteBatch, _animations["darknut-move-animation-right"]);
    
    public ISprite DarknutMoveLeft()
    {
        AnimatedSprite s = new AnimatedSprite(_spriteBatch, _animations["darknut-move-animation-right"]);
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }
    public ISprite Gel() => new AnimatedSprite(_spriteBatch, _animations["gel-move-animation"]);
    public ISprite GoriyaDown() => new IdleSprite(_regions["goriya-down"], _spriteBatch);
    public ISprite GoriyaUp() => new IdleSprite(_regions["goriya-up"], _spriteBatch);
    public ISprite GoriyaRight() => new IdleSprite(_regions["goriya-right"], _spriteBatch);
    public ISprite GoriyaLeft()
    {
        IdleSprite s = new IdleSprite(_regions["goriya-right"], _spriteBatch);
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }
    public ISprite GoriyaUse() => new IdleSprite(_regions["goriya-use"], _spriteBatch);
    public ISprite Keese() => new AnimatedSprite(_spriteBatch, _animations["keese-move-animation"]);

    public ISprite Stalfos() => new IdleSprite(_regions["stalfos"], _spriteBatch);

    public ISprite Rope() => new AnimatedSprite(_spriteBatch, _animations["rope-move-animation"]);
    public ISprite Zol() => new AnimatedSprite(_spriteBatch, _animations["zol-move-animation"]);
    public ISprite Wallmaster() => new AnimatedSprite(_spriteBatch, _animations["wallmaster-move-animation"]);
    public ISprite Trap() => new IdleSprite(_regions["trap"], _spriteBatch);

}

