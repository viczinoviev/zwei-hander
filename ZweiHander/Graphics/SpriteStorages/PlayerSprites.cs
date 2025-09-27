using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class PlayerSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/LinkDefinition.xml";
    private SpriteBatch _spriteBatch;
    public PlayerSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite PlayerMoveDown() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-down"]);
    public ISprite PlayerMoveUp() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-up"]);
    public ISprite PlayerMoveRight() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-right"]);
    public ISprite PlayerMoveLeft() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-right"]);
    public ISprite PlayerAttackSwordDown() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-down"]);
    public ISprite PlayerAttackSwordUp() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-up"]);
    public ISprite PlayerAttackSwordRight() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-right"]);

}

