using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;

public class BossSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/EnemyBossesDefinition.xml";
    private SpriteBatch _spriteBatch;
    public BossSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite Aquamentus() => new AnimatedSprite(_spriteBatch, _animations["aquamentus-animation"]);
    public ISprite AquamentusProjectile() => new AnimatedSprite(_spriteBatch, _animations["aquamentus-projectile-animation"]);


}

