using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;

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

        public ISprite Kirby() => new AnimatedSprite(_spriteBatch, _animations["kirby-animation"]);
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