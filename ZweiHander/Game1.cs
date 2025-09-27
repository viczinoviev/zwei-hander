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
        private ISprite _block;
        private ISprite _treasure;
        // END TEST

        private Player _gamePlayer;
        private KeyboardController _keyboardController;

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
            PlayerSprites _linkSprites = new PlayerSprites(Content, _spriteBatch);
            BlockSprites _blockSprites = new BlockSprites(Content, _spriteBatch);
            TreasureSprites _treasureSprites = new TreasureSprites(Content, _spriteBatch);

            _block = _blockSprites.BlockTile();
            _treasure = _treasureSprites.HeartContainer();

            //END TEST

            _gamePlayer = new Player(_linkSprites);
            _keyboardController = new KeyboardController(_gamePlayer);
            _gamePlayer.Position = new Vector2(400, 300);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // This is needed to update the frames for the animation
            _block.Update(gameTime);
            //END TEST

            _keyboardController.Update();
            _gamePlayer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            // Draws the sprite at the passed in coordinates

            _block.Draw(new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.75f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.25f
                )
            );

            _treasure.Draw(new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.25f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.25f
                )
            );
            //END TEST

            _gamePlayer.Draw(_spriteBatch);

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
