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
    public ISprite LinkMoveDown() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-down"]);
    public ISprite LinkMoveUp() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-up"]);
    public ISprite LinkMoveRight() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-Right"]);
    public ISprite LinkAttackSwordDown() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-down"]);
    public ISprite LinkAttackSwordUp() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-up"]);
    public ISprite LinkAttackSwordRight() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-right"]);

}

