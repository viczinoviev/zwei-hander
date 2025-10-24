using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

namespace ZweiHander.Graphics.SpriteStorages;

public class BossSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/EnemyBossesDefinition.xml";
    readonly SpriteBatch _spriteBatch;
    public BossSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }
    public ISprite Aquamentus() => new AnimatedSprite(_spriteBatch, _animations["aquamentus-animation"]);
    public ISprite AquamentusProjectile() => new AnimatedSprite(_spriteBatch, _animations["aquamentus-projectile-animation"]);
    public ISprite DodongoDown() => new FlippingSprites(_regions["dodongo-down"], _spriteBatch);
    public ISprite DodongoDownHurt() => new AnimatedSprite(_spriteBatch, _animations["dodongo-down-hurt"]);
    public ISprite DodongoUp() => new AnimatedSprite(_spriteBatch, _animations["dodongo-move-up-animation"]);
    public ISprite DodongoRight() => new AnimatedSprite(_spriteBatch, _animations["dodongo-move-right-animation"]);
    public ISprite DodongoLeft()
    {
        ISprite s = this.DodongoRight();
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }
    public ISprite DodongoRightHurt() => new IdleSprite(_regions["dodongo-right-hurt"], _spriteBatch);
    public ISprite DodongoLeftHurt()
    {
        ISprite s = this.DodongoRightHurt();
        s.Effects = SpriteEffects.FlipHorizontally;
        return s;
    }

    //NOTE: There is no attack sprite for Dodongo Up
}

