using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

namespace ZweiHander
{
    public class Game1 : Game
    {
    //Hey team!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // TEST: Link Sprites, this should be contained in the Link Class.
        private LinkSprites _linkSprites;
        private ISprite _link;
        // END TEST

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //TEST: Link Sprites, Should be part of link initialization
            LinkSprites _linkSprites = new LinkSprites(Content, _spriteBatch);
            _link = _linkSprites.LinkAttackSword();
            //END TEST

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //TEST: Link sprites, should be contained in Link Update
            _link.Update(gameTime);
            //END TEST

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            //TEST: Link sprites, logic to be decided
            _link.Draw();
            //END TEST

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
