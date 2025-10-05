using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;
public class PlayerSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/LinkDefinition.xml";
    private SpriteBatch _spriteBatch;

    // Logic for centering the player in animation
    private Vector2 _defaultOrigin = new Vector2(8, 8);
    public PlayerSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }

    public ISprite PlayerIdle() => new IdleSprite(_regions["link-move-down-1"], _spriteBatch, true);
    public ISprite PlayerMoveDown() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-down"]);
    public ISprite PlayerMoveUp() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-up"]);
    public ISprite PlayerMoveRight() => new AnimatedSprite(_spriteBatch, _animations["link-move-animation-right"]);
    public ISprite PlayerMoveLeft()
    {
        AnimatedSprite s = new AnimatedSprite(_spriteBatch, _animations["link-move-animation-right"]);
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }
    public ISprite PlayerAttackSwordDown() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-down"]);
    public ISprite PlayerAttackSwordUp()
    {
        AnimatedSprite s = new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-up"]);
        s.AnchorBottomRight(_defaultOrigin);
        return s;
    } 
    public ISprite PlayerAttackSwordRight() => new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-right"]);
    public ISprite PlayerAttackSwordLeft()
    {
        AnimatedSprite s = new AnimatedSprite(_spriteBatch, _animations["link-attack-sword-animation-right"]);
        s.Effects = SpriteEffects.FlipHorizontally;
        s.AnchorBottomRight(_defaultOrigin);
        return s;
    }

    public ISprite PlayerUseItemDown() => new IdleSprite(_regions["link-use-item-down"], _spriteBatch, true);
    public ISprite PlayerUseItemUp() => new IdleSprite(_regions["link-use-item-up"], _spriteBatch, true);
    public ISprite PlayerUseItemRight() => new IdleSprite(_regions["link-use-item-right"], _spriteBatch, true);
    public ISprite PlayerUseItemLeft()
    {
        IdleSprite s = new IdleSprite(_regions["link-use-item-right"], _spriteBatch, true);
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }

}

