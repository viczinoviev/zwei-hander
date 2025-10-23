using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using ZweiHander.CollisionFiles;
using ZweiHander.Commands;
using ZweiHander.Enemy;
using ZweiHander.Enemy.EnemyStorage;
using ZweiHander.Environment;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander
{
    public class Game1 : Game
    {
    //Hey team!
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Block _block;
        private IEnemy _enemy;
        private IItem _item;


        private Player _gamePlayer;
        private KeyboardController _keyboardController;

        private HurtPlayerCommand _hurtPlayerCommand;


        //Sprites and factories
        private PlayerSprites _linkSprites;
        private BlockSprites _blockSprites;
        private TreasureSprites _treasureSprites;
        private EnemySprites _enemySprites;
        private EnemyManager _enemyManager;
        private BossSprites _bossSprites;
        private NPCSprites _npcSprites;
        private ItemSprites _itemSprites;
        private BlockFactory _blockFactory;
        private ItemManager _itemManager; //The item that is rotated through
        private ItemManager _projectileManager; //Any projectiles from enemies or player
        private BorderManager _borderManager;
        private BorderManager _borderManager2;

        //Stores all the blocks created
        private List<Block> _blockList;
        private int _blockIndex = 0;

        //Stores all the enemies created
        private List<IEnemy> _enemyList;
        private int _enemyIndex = 0;

        //dummy position for treasure, item, block, and enemy
        Vector2 treasurePosition;
        Vector2 itemPosition;
        Point blockPosition;
        Vector2 enemyPosition;

        //Expose 
        public List<Block> BlockList => _blockList;
        public Block Block { get => _block; set => _block = value; }
        public int BlockIndex { get => _blockIndex; set => _blockIndex = value; }

        public List<IEnemy> EnemyList => _enemyList;
        public IEnemy Enemy { get => _enemy; set => _enemy = value; }
        public int EnemyIndex { get => _enemyIndex; set => _enemyIndex = value; }

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

        public Player GamePlayer => _gamePlayer;

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
            _itemSprites = new ItemSprites(Content, _spriteBatch);
            _blockFactory = new BlockFactory(32, _blockSprites, _linkSprites);
            _enemySprites = new EnemySprites(Content, _spriteBatch);
            _bossSprites = new BossSprites(Content, _spriteBatch);
            _npcSprites = new NPCSprites(Content, _spriteBatch);
            _itemManager = new ItemManager(_itemSprites, _treasureSprites, _bossSprites);
            _projectileManager = new ItemManager(_itemSprites, _treasureSprites, _bossSprites);
            _enemyManager = new EnemyManager(_enemySprites, _projectileManager, _bossSprites,_npcSprites);
            _borderManager = new BorderManager(_blockSprites,new Vector2(47,175));
            _borderManager2 = new BorderManager(_blockSprites, new Vector2(559, 175));

            GameSetUp();
        }

        /// <summary>
        /// Sets up the initial game state
        /// </summary>
        public void GameSetUp()
        {
            itemPosition = new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.5f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.75f
                );
            blockPosition = new Point(10, 6);
            enemyPosition = new Vector2(
                GraphicsDevice.PresentationParameters.BackBufferWidth * 0.75f,
                GraphicsDevice.PresentationParameters.BackBufferHeight * 0.25f
                );

            //Create and load the Block List
            string mapPath = Path.Combine(Content.RootDirectory, "Maps","map1.csv"); // CSV location
            _blockList = CsvMapHandler.LoadMap(mapPath, _blockFactory);

            //Border creation for now(Will change shortly)
            _borderManager.CreateWall(WallName.WallNorthLeft);
            _borderManager.CreateWall(WallName.WallWestTop); 
            _borderManager.CreateWall(WallName.LockedDoorTileNorth); 
            _borderManager.CreateWall(WallName.WallTileWest); 
            _borderManager.CreateWall(WallName.WallWestBottom); 
            _borderManager.CreateWall(WallName.WallSouthLeft);
            _borderManager.CreateWall(WallName.DoorTileSouth); 
            _borderManager.CreateWall(WallName.WallSouthRight);
            _borderManager.CreateWall(WallName.WallNorthRight);
            _borderManager.CreateWall(WallName.WallEastTop);
            _borderManager.CreateWall(WallName.WallEastBottom);
            _borderManager.CreateWall(WallName.HoleInWallEast);

            //2nd game room(will move all room creation into a different file later)
            _borderManager2.CreateWall(WallName.WallNorthLeft);
            _borderManager2.CreateWall(WallName.WallWestTop);
            _borderManager2.CreateWall(WallName.LockedDoorTileNorth);
            _borderManager2.CreateWall(WallName.HoleInWallWest);
            _borderManager2.CreateWall(WallName.WallWestBottom);
            _borderManager2.CreateWall(WallName.WallSouthLeft);
            _borderManager2.CreateWall(WallName.DoorTileSouth);
            _borderManager2.CreateWall(WallName.WallSouthRight);
            _borderManager2.CreateWall(WallName.WallNorthRight);
            _borderManager2.CreateWall(WallName.WallEastTop);
            _borderManager2.CreateWall(WallName.WallEastBottom);
            _borderManager2.CreateWall(WallName.WallTileEast);

            _itemManager.Clear();
            _items = [
                _itemManager.GetItem(ItemType.Heart, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Boomerang, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Arrow, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.HeartContainer, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Rupy, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Compass, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Map, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Key, -1, position: itemPosition),
                _itemManager.GetItem(ItemType.Fairy, -1, position: itemPosition, velocity: new(0, -10.0f))
            ];
            //Create enemy list
            _enemyList =
            [
                _enemy = _enemyManager.GetEnemy("Darknut", enemyPosition),
                _enemy = _enemyManager.GetEnemy("Gel", enemyPosition),
                _enemy = _enemyManager.GetEnemy("Goriya", enemyPosition),
                _enemy = _enemyManager.GetEnemy("Keese", enemyPosition),
                _enemy = _enemyManager.GetEnemy("Stalfos", enemyPosition),
                _enemy = _enemyManager.GetEnemy("Rope",enemyPosition),
                _enemy = _enemyManager.GetEnemy("Wallmaster",enemyPosition),
                _enemy = _enemyManager.GetEnemy("Zol",enemyPosition),
                _enemy = _enemyManager.GetEnemy("Dodongo",enemyPosition),
                _enemy = _enemyManager.GetEnemy("Aquamentus",enemyPosition),
                _enemy = _enemyManager.GetEnemy("BladeTrap",enemyPosition),
                _enemy = _enemyManager.GetEnemy("OldMan", enemyPosition),
            ];



            _block = _blockList[0];
            _enemy = _enemyList[0];
            ItemIndex = 0;

            //END TEST

            _gamePlayer = new Player(_linkSprites, _itemSprites, _treasureSprites);
            _gamePlayer.Position = new Vector2(450, 350);

            //Set up KeyboardController
             _keyboardController = new KeyboardController(_gamePlayer);
             _hurtPlayerCommand = new HurtPlayerCommand(this);
             _keyboardController.BindKey(Keys.R, new ResetCommand(this));
             _keyboardController.BindKey(Keys.Q, new QuitCommand(this));
            // _keyboardController.BindKey(Keys.T, new ChangeBlockCommand(this, -1));
            // _keyboardController.BindKey(Keys.Y, new ChangeBlockCommand(this, +1));
            // _keyboardController.BindKey(Keys.U, new ChangeItemCommand(this, -1));
            // _keyboardController.BindKey(Keys.I, new ChangeItemCommand(this, +1));
             _keyboardController.BindKey(Keys.O, new ChangeEnemyCommand(this, -1));
             _keyboardController.BindKey(Keys.P, new ChangeEnemyCommand(this, +1));
            _keyboardController.BindKey(Keys.E, _hurtPlayerCommand);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();




            for (int i = 0; i < _blockList.Count; i++)
            {
                _blockList[i].Update(gameTime);
            }

            _item.Update(gameTime);
            _enemyManager.Update(gameTime);
            _projectileManager.Update(gameTime);


            _keyboardController.Update();
            _gamePlayer.Update(gameTime);
            _hurtPlayerCommand.Update(gameTime);

            // Update collision system - keeps everything synced
            CollisionManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            //Draws the map
            _blockFactory.Draw();
            _borderManager.Draw();
            _borderManager2.Draw();

            _enemyManager.Draw();


            _item.Draw();

            _projectileManager.Draw();

            

            _gamePlayer.Draw(_spriteBatch);

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
