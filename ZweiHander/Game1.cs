using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ZweiHander.Camera;
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
    // Hey hows it going?
        readonly private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera.Camera _camera;

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
        private Dungeon _dungeon;

        //Stores all the blocks created
        private List<Block> _blockList;

        //dummy position for treasure, item, block, and enemy
        Vector2 enemyPosition;


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

            // Initialize camera
            _camera = new Camera.Camera(GraphicsDevice.Viewport);

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
            enemyPosition = new Vector2(700, 300);

            //Create and load the Block List
            string mapPath = Path.Combine(Content.RootDirectory, "Maps","map1.csv"); // CSV location
            _blockList = CsvMapHandler.LoadMap(mapPath, _blockFactory);

            //TODO: this is for testing, find a way to not have it hard coded
            _dungeon = new Dungeon();
            Room room1 = new Room(new Vector2(47, 175), new Vector2(Room.ROOM_WIDTH, Room.ROOM_HEIGHT));
            Room room2 = new Room(new Vector2(559, 175), new Vector2(Room.ROOM_WIDTH, Room.ROOM_HEIGHT));
            _dungeon.AddRoom(room1);
            _dungeon.AddRoom(room2);
            _camera.SetDungeon(_dungeon);

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
            _enemyManager.Clear();
            //Create enemy list
            _enemyManager.GetEnemy("Darknut", enemyPosition);
            _enemyManager.GetEnemy("Darknut", new Vector2(enemyPosition.X + 30, enemyPosition.Y));
            _enemyManager.GetEnemy("Darknut", new Vector2(enemyPosition.X + 60, enemyPosition.Y + 30));
            _enemyManager.GetEnemy("Darknut", new Vector2(enemyPosition.X, enemyPosition.Y + 60));
            _enemyManager.GetEnemy("Goriya", new Vector2(enemyPosition.X + 90, enemyPosition.Y + 30));
            _enemyManager.GetEnemy("Goriya", new Vector2(enemyPosition.X + 90, enemyPosition.Y + 90));
            _enemyManager.GetEnemy("Gel", new Vector2(enemyPosition.X - 300, enemyPosition.Y + 30));
            _enemyManager.GetEnemy("Gel", new Vector2(enemyPosition.X - 330, enemyPosition.Y + 30));
            _enemyManager.GetEnemy("Gel", new Vector2(enemyPosition.X - 360, enemyPosition.Y + 60));
            _enemyManager.GetEnemy("Gel", new Vector2(enemyPosition.X - 390, enemyPosition.Y + 90));
            _enemyManager.GetEnemy("Gel", new Vector2(enemyPosition.X - 420, enemyPosition.Y + 60));
            _enemyManager.GetEnemy("Gel", new Vector2(enemyPosition.X - 440, enemyPosition.Y + 30));
            //END TEST

            _gamePlayer = new (_linkSprites, _itemSprites, _treasureSprites);
            _gamePlayer.Position = new Vector2(450, 350);

            //Set up KeyboardController
             _keyboardController = new KeyboardController(_gamePlayer);
             _hurtPlayerCommand = new HurtPlayerCommand(this);
             _keyboardController.BindKey(Keys.R, new ResetCommand(this));
             _keyboardController.BindKey(Keys.Q, new QuitCommand(this));
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
            _itemManager.Update(gameTime);
            _enemyManager.Update(gameTime);
            _projectileManager.Update(gameTime);


            _keyboardController.Update();
            _gamePlayer.Update(gameTime);
            _hurtPlayerCommand.Update(gameTime);

            // Update collision system - keeps everything synced
            CollisionManager.Instance.Update(gameTime);

            // Update camera to follow player
            _camera.Update(_gamePlayer.Position);

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: _camera.GetTransformMatrix() //matrix used to change rendering position based on camera position
            );


            //Draws the map
            _blockFactory.Draw();
            _borderManager.Draw();
            _borderManager2.Draw();

            _enemyManager.Draw();


            _itemManager.Draw();

            _projectileManager.Draw();



            _gamePlayer.Draw(_spriteBatch);

            base.Draw(gameTime);

            _spriteBatch.End();
        }
    }
}
