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
            
            // This line will load all of the sprites into the program through an xml file
            LinkSprites _linkSprites = new LinkSprites(Content, _spriteBatch);
            
            // This will return the AnimatedSprite of link doing a sword attack
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
            // This is needed to update the frames for the animation
            _link.Update(gameTime);
            //END TEST

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            //TEST: Link sprites, Should be called in the player class
            // Draws the sprite at the passed in coordinates
            _link.Draw(new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.5f
                )
            );
            //END TEST

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
