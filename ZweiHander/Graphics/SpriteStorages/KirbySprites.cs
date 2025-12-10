using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Graphics.SpriteStorages
{
    public class KirbySprites : SpriteFactory
    {
        private const string _definitionFile = "SpriteSheets/kirbyDefinition.xml";
        private readonly SpriteBatch _spriteBatch;

        public KirbySprites(ContentManager content, SpriteBatch spriteBatch)
        {
            FromFile(content, _definitionFile);
            _spriteBatch = spriteBatch;
        }

        public ISprite KirbyRunningRight() => new AnimatedSprite(_spriteBatch, _animations["kirby-animation"]);
        public ISprite KirbyRunningLeft()
        {
            return new AnimatedSprite(_spriteBatch, _animations["kirby-animation"])
            {
                Effects = SpriteEffects.FlipHorizontally
            };
        }
        public ISprite KirbyAttackRight() => new AnimatedSprite(_spriteBatch, _animations["kirby-fight-animation"]);
        public ISprite KirbyAttackLeft()
        {
            return new AnimatedSprite(_spriteBatch, _animations["kirby-fight-animation"])
            {
                Effects = SpriteEffects.FlipHorizontally
            };
        }
    }
}