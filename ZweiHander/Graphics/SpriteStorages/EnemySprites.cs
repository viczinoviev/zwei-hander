using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;

public class EnemySprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/EnemyDefinition.xml";
    readonly SpriteBatch _spriteBatch;
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
        return new AnimatedSprite(_spriteBatch, _animations["darknut-move-animation-right"])
        {
            Effects = SpriteEffects.FlipHorizontally
        };
    }
    public ISprite Gel() => new AnimatedSprite(_spriteBatch, _animations["gel-move-animation"]);
    public ISprite GoriyaDown() => new IdleSprite(_regions["goriya-down"], _spriteBatch);
    public ISprite GoriyaUp() => new IdleSprite(_regions["goriya-up"], _spriteBatch);
    public ISprite GoriyaRight() => new IdleSprite(_regions["goriya-right"], _spriteBatch);
    public ISprite GoriyaLeft()
    {
        return new IdleSprite(_regions["goriya-right"], _spriteBatch)
        {
            Effects = SpriteEffects.FlipHorizontally
        };
    }
    public ISprite GoriyaUse() => new IdleSprite(_regions["goriya-use"], _spriteBatch);
    public ISprite Keese() => new AnimatedSprite(_spriteBatch, _animations["keese-move-animation"]);

    public ISprite Stalfos() => new IdleSprite(_regions["stalfos"], _spriteBatch);

    public ISprite RopeRight() => new AnimatedSprite(_spriteBatch, _animations["rope-move-animation"]);

    public ISprite RopeLeft()
    {
        return new AnimatedSprite(_spriteBatch, _animations["rope-move-animation"])
        {
            Effects = SpriteEffects.FlipHorizontally
        };
    }
    public ISprite Zol() => new AnimatedSprite(_spriteBatch, _animations["zol1-move-animation"]);
    public ISprite WallmasterUp() => new AnimatedSprite(_spriteBatch, _animations["wallmaster-move-animation"]);

    public ISprite WallmasterDown()
    {
        return new AnimatedSprite(_spriteBatch, _animations["wallmaster-move-animation"])
        {
            Effects = SpriteEffects.FlipVertically
        };
    }
    public ISprite Trap() => new IdleSprite(_regions["trap"], _spriteBatch);

}

