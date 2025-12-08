using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZweiHander.Graphics.SpriteStorages;

public class ItemSprites : SpriteFactory
{
    private const string _definitionFile = "SpriteSheets/LinkDefinition.xml";
    readonly SpriteBatch _spriteBatch;
    public ItemSprites(ContentManager content, SpriteBatch spriteBatch)
    {
        FromFile(content, _definitionFile);
        _spriteBatch = spriteBatch;
    }

    public ISprite Arrow(Vector2 direction)
    {
        IdleSprite sprite = new IdleSprite(_regions["arrow-right"], _spriteBatch)
        {
            Rotation = (float)Math.Atan2(direction.Y, direction.X)
        };
        return sprite;
    }
    public ISprite FireProjectile()
    {
        ISprite sprite = new AnimatedSprite(_spriteBatch, _animations["fire-animation"]);
        return sprite;
    }

    public ISprite ProjectileOnHit() => new IdleSprite(_regions["projectile-hit"], _spriteBatch);
    public ISprite Boomerang() => new AnimatedSprite(_spriteBatch, _animations["boomerang-animation"]);
    public ISprite Bomb() => new IdleSprite(_regions["bomb"], _spriteBatch);
    public ISprite Explosion() => new AnimatedSprite(_spriteBatch, _animations["explosion-animation"]);

    public ISprite SwordProjectile(Vector2 direction)
    {
        IdleSprite sprite = new IdleSprite(_regions["sword-projectile-right"], _spriteBatch)
        {
            Rotation = (float)Math.Atan2(direction.Y, direction.X)
        };
        return sprite;
    }

    public ISprite SwordProjectileEffect(Vector2 direction)
    {
        IdleSprite sprite = new IdleSprite(_regions["sword-projectile-effect"], _spriteBatch)
        {
            Rotation = (float)Math.Atan2(direction.Y, direction.X)
        };
        return sprite;
    }

}

