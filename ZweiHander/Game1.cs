using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ZweiHander.Camera;
using ZweiHander.CollisionFiles;
using ZweiHander.Commands;
using ZweiHander.Enemy;
using ZweiHander.Enemy.EnemyStorage;
using ZweiHander.Environment;
using ZweiHander.GameStates;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.HUD;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander
{
    public class Game1 : Game
    {
        //Hey team!
        // Hey hows it going?
        private IGameState _gameState;
        public bool gamePaused = false;
        public HUDManager HUDManager => _hudManager;
        readonly private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera.Camera _camera;
        private HUDManager _hudManager;

        private Player _gamePlayer;
        private KeyboardController _keyboardController;

        private HurtPlayerCommand _hurtPlayerCommand;



        //Sprites and factories
        private PlayerSprites _linkSprites;
        private HUDSprites _hudSprites;
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
        
        private Universe _universe;
        private CsvAreaConstructor _areaConstructor;

        private Texture2D _debugPixel;
        //Backround Song
        private Song bgm;

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
            _gameState = new GameState();
            Services.AddService<IGameState>(_gameState);

            // if HUD needs to mirror pause state:
            _gameState.PausedChanged += p => _hudManager.SetPaused(p);


            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize camera
            _camera = new Camera.Camera(GraphicsDevice.Viewport);

            //TEST: Link Sprites, Should be part of link initialization

            // This line will load all of the sprites into the program through an xml file
            _linkSprites = new PlayerSprites(Content, _spriteBatch);
            _hudSprites = new HUDSprites(Content, _spriteBatch);
            _blockSprites = new BlockSprites(Content, _spriteBatch);
            _treasureSprites = new TreasureSprites(Content, _spriteBatch);
            _itemSprites = new ItemSprites(Content, _spriteBatch);
            _enemySprites = new EnemySprites(Content, _spriteBatch);
            _bossSprites = new BossSprites(Content, _spriteBatch);
            _npcSprites = new NPCSprites(Content, _spriteBatch);
            
            // Create separate manager instances for Game1 use
            _blockFactory = new BlockFactory(32, _blockSprites, _linkSprites);
            _itemManager = new ItemManager(_itemSprites, _treasureSprites, _bossSprites);
            _projectileManager = new ItemManager(_itemSprites, _treasureSprites, _bossSprites);
            _enemyManager = new EnemyManager(_enemySprites, _projectileManager, _bossSprites, _npcSprites, Content);

            _debugPixel = new Texture2D(GraphicsDevice, 1, 1);
            _debugPixel.SetData(new[] { Color.White });

            // Initialize debug texture for collision manager
            CollisionManager.Instance.InitializeDebugTexture(GraphicsDevice);
            bgm = Content.Load<Song>("Audio/DungeonTheme");
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;

            GameSetUp();
        }

        /// <summary>
        /// Sets up the initial game state
        /// </summary>
        public void GameSetUp()
        {
            _gamePlayer = new Player(_linkSprites, _itemSprites, _treasureSprites);
            _gamePlayer.Position = new Vector2(100, 100);

            // Universe creates its own manager instances
            _universe = new Universe(
                _enemySprites,
                _bossSprites,
                _npcSprites,
                _itemSprites,
                _treasureSprites,
                _blockSprites,
                _linkSprites,

                Content
            );
            _universe.SetPlayer(_gamePlayer);
            _universe.SetupPortalManager(_camera);
            _areaConstructor = new CsvAreaConstructor();

            string mapPath = Path.Combine(Content.RootDirectory, "Maps", "testDungeon1.csv");
            Area testArea = _areaConstructor.LoadArea(mapPath, _universe, _gamePlayer, _camera, "TestDungeon");

            _universe.AddArea(testArea);
            _universe.SetCurrentLocation("TestDungeon", 1);

            _keyboardController = new KeyboardController(_gamePlayer);
            _hurtPlayerCommand = new HurtPlayerCommand(this);
            _keyboardController.BindKey(Keys.R, new ResetCommand(this));
            _keyboardController.BindKey(Keys.Q, new QuitCommand(this));
            _keyboardController.BindKey(Keys.E, _hurtPlayerCommand);
            _keyboardController.BindKey(Keys.I, new InventoryCommand(this));
            // Initialize HUD Manager
            _hudManager = new HUDManager(_gamePlayer, _hudSprites, gamePaused);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _universe.Update(gameTime);
            _itemManager.Update(gameTime);
            _projectileManager.Update(gameTime);

            _keyboardController.Update();
            _gamePlayer.Update(gameTime);
            _hurtPlayerCommand.Update(gameTime);

            CollisionManager.Instance.Update(gameTime);

            _camera.Update(gameTime, _gamePlayer.Position);

            base.Update(gameTime);
            // Update HUD
            _hudManager.Update(gameTime);

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw world with camera transform
            _spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: _camera.GetTransformMatrix()
            );

            _universe.Draw();
            _projectileManager.Draw();
            _gamePlayer.Draw(_spriteBatch);

            DrawDebugGrid();
            
            // Draw debug collision boxes if enabled
            CollisionManager.Instance.DrawDebugCollisionBoxes(_spriteBatch);

            base.Draw(gameTime);

            _spriteBatch.End();
        }

        private void DrawDebugGrid()
        {
            int gridSize = 32;
            int dotSize = 2;
            int gridRange = 20; 

            for (int y = -gridRange; y <= gridRange; y++)
            {
                Rectangle dot = new Rectangle(0 - dotSize / 2, y * gridSize - dotSize / 2, dotSize, dotSize);
                _spriteBatch.Draw(_debugPixel, dot, Color.Yellow);
            }

            for (int x = -gridRange; x <= gridRange; x++)
            {
                Rectangle dot = new Rectangle(x * gridSize - dotSize / 2, 0 - dotSize / 2, dotSize, dotSize);
                _spriteBatch.Draw(_debugPixel, dot, Color.Yellow);
            }

            Rectangle origin = new Rectangle(-4, -4, 8, 8);
            _spriteBatch.Draw(_debugPixel, origin, Color.Green);
        }

        private void DrawDebugRectangle(Rectangle rect, Color color)
        {
            _spriteBatch.Draw(_debugPixel, rect, color);
        }
    }
}
