using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ZweiHander.Commands;
using ZweiHander.Enemy;
using ZweiHander.Environment;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;

namespace ZweiHander
{
    public class Game1 : Game
    {
    //Hey team!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // TEST: Link Sprites, this should be contained in the Link Class.
        private Block _block;
        private IEnemy _enemy;
        private IItem _item;
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
        private ItemManager _itemManager; //The item that is rotated through
        private ItemManager _projectileManager; //Any projectiles from enemies or player

        //Stores all the blocks created
        private List<Block> _blockList;
        private int _blockIndex = 0;

        //dummy position for treasure and item
        Vector2 treasurePosition;
        Vector2 itemPosition;
        Point blockPosition;

        //Expose 
        public List<Block> BlockList => _blockList;
        public Block Block { get => _block; set => _block = value; }
        public int BlockIndex { get => _blockIndex; set => _blockIndex = value; }

        private List<IItem> _items;
        private int _itemIndex = 0;
        /// <summary>
        /// Index for current item.
        /// </summary>
        public int ItemIndex { get => _itemIndex; set { _itemIndex = value; _item = _items[value];  } }

        /// <summary>
        /// Number of items available.
        /// </summary>
        public int ItemCount { get => _itemManager.ItemCount; }

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
            _itemManager = new ItemManager(_itemSprites, _treasureSprites);
            _projectileManager = new ItemManager(_itemSprites, _treasureSprites);

            GameSetUp();
        }

        /// <summary>
        /// Sets up the initial game state
        /// </summary>
        public void GameSetUp()
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

            //Create and load the Block List
            _blockList = new List<Block>();
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.SolidCyanTile, blockPosition));
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.TexturedTile, blockPosition));
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.StatueTile1, blockPosition));
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.WhitePatternTile, blockPosition));
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.BrickTile, blockPosition));
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.BlockTile, blockPosition));
            _blockList.Add(_block = _blockFactory.CreateBlock(BlockName.StairTile, blockPosition));

            _items = [
                _itemManager.GetItem(ItemType.Heart, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Boomerang, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Arrow, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.HeartContainer, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Rupy, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Fairy, -1, position: itemPosition)
            ];

            _block = _blockList[0];
            _enemy = _enemyFactory.GetEnemy("Darknut", new Vector2(300, 300));
            _item = _items[_itemIndex];

            //END TEST

            _gamePlayer = new Player(_linkSprites);
            _gamePlayer.Position = new Vector2(400, 300);

            //Set up KeyboardController
            _keyboardController = new KeyboardController(_gamePlayer);
            _keyboardController.BindKey(Keys.R, new ResetCommand(this));
            _keyboardController.BindKey(Keys.Q, new QuitCommand(this));
            _keyboardController.BindKey(Keys.T, new ChangeBlockCommand(this, -1));
            _keyboardController.BindKey(Keys.Y, new ChangeBlockCommand(this, +1));
            _keyboardController.BindKey(Keys.U, new ChangeItemCommand(this, -1));
            _keyboardController.BindKey(Keys.I, new ChangeItemCommand(this, +1));
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
            _projectileManager.Update(gameTime);
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

            _enemy.Draw();

            
            _item.Draw();

            _projectileManager.Draw();

            //END TEST

            _gamePlayer.Draw(_spriteBatch);

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
