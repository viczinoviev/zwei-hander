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

}

