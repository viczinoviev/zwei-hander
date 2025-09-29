using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Enemy;
using ZweiHander.Environment;

namespace ZweiHander
{
    public class Game1 : Game
    {
    //Hey team!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // TEST: Link Sprites, this should be contained in the Link Class.
        private Block _block;
        private ISprite _treasure;
        private IEnemy _enemy;
        private ISprite _item;
        // END TEST

        private Player _gamePlayer;
        private KeyboardController _keyboardController;

        //Sprites and factories
        private PlayerSprites _linkSprites;
        private BlockSprites _blockSprites;
        private TreasureSprites _treasureSprites;
        private EnemySprites _enemySprites;
        private EnemyFactory _enemyFactory;
        private ItemSprites _itemSprites;
        private BlockFactory _blockFactory;

        //dummy position for treasure and item
        Vector2 treasurePosition;
        Vector2 itemPosition;
        Point blockPosition;
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
            _linkSprites = new PlayerSprites(Content, _spriteBatch);
            _blockSprites = new BlockSprites(Content, _spriteBatch);
            _treasureSprites = new TreasureSprites(Content, _spriteBatch);
            _enemySprites = new EnemySprites(Content, _spriteBatch);
            _enemyFactory = new EnemyFactory(_enemySprites);
            _itemSprites = new ItemSprites(Content, _spriteBatch);
            _blockFactory = new BlockFactory(32, _blockSprites);

            GameSetUp();
            
            
        }

        /// <summary>
        /// Sets up the initial game state
        /// </summary>
        private void GameSetUp()
        {
            treasurePosition = new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.25f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.25f
                );
            itemPosition = new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.75f
                );
            blockPosition = new Point(10, 6);

            _block = _blockFactory.CreateBlock(BlockName.SolidCyanTile, blockPosition);
            _treasure = _treasureSprites.HeartContainer();
            _enemy = _enemyFactory.GetEnemy("Darknut", new Vector2(300, 300));
            _item = _itemSprites.Boomerang();

            //END TEST

            _gamePlayer = new Player(_linkSprites);
            _keyboardController = new KeyboardController(_gamePlayer);
            _gamePlayer.Position = new Vector2(400, 300);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // This is needed to update the frames for the animation
            _block.Update(gameTime);
            _item.Update(gameTime);
            _enemy.Update(gameTime);
            //END TEST

            _keyboardController.Update();
            _gamePlayer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            // Draws the sprite at the passed in coordinates

            _block.Draw();

            
            _treasure.Draw(treasurePosition);

            _enemy.Draw();

            
            _item.Draw(itemPosition);

            //END TEST

            _gamePlayer.Draw(_spriteBatch);

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
