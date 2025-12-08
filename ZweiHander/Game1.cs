using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using ZweiHander.CollisionFiles;
using ZweiHander.Commands;
using ZweiHander.Enemy;
using ZweiHander.FriendlyNPC;
using ZweiHander.GameStates;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.HUD;
using ZweiHander.Items;
using ZweiHander.Map;
using ZweiHander.PlayerFiles;

namespace ZweiHander
{
    public class Game1 : Game
    {
        //Hey team!
        // Hey hows it going?
        private GameState _gameState;
        public bool gamePaused = false;
        public HUDManager HUDManager => _hudManager;
        readonly private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera.Camera _camera;
        private HUDManager _hudManager;

        private Player _gamePlayer;
        private KeyboardController _keyboardController;
        private TitleScreenController _titleScreenController;
        private GameOverScreen _gameOverScreen;
        private GameWonScreen _gameWonScreen;

        private Kirby _kirby;
        private KirbySprites _kirbySprites;

        //Sprites and factories
        private PlayerSprites _linkSprites;
        private HUDSprites _hudSprites;
        private TreasureSprites _treasureSprites;
        private ItemSprites _itemSprites;
        private TitleSprites _titleSprites;
        private EnemySprites _enemySprites;
        private BossSprites _bossSprites;
        private NPCSprites _npcSprites;

        private Universe _universe;
        private CsvAreaConstructor _areaConstructor;

        private DebugRenderer _debugRenderer;
        private Song bgm;

        public Player GamePlayer => _gamePlayer;

        public Kirby GameKirby => _kirby;

        public EnemyManager HordeManager;

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

            _gameState.PausedChanged += p => _hudManager?.SetHUDOpen(p);
            _gameState.ModeChanged += OnGameModeChanged;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize camera
            _camera = new Camera.Camera(GraphicsDevice.Viewport);

            _titleSprites = new TitleSprites(Content, _spriteBatch);
            _titleScreenController = new TitleScreenController();
            _gameOverScreen = new GameOverScreen(Content, GraphicsDevice);
            _gameWonScreen = new GameWonScreen(Content, GraphicsDevice);

            // This line will load all of the sprites into the program through an xml file
            _linkSprites = new PlayerSprites(Content, _spriteBatch);
            _hudSprites = new HUDSprites(Content, _spriteBatch);
            _treasureSprites = new TreasureSprites(Content, _spriteBatch);
            _itemSprites = new ItemSprites(Content, _spriteBatch);
            _bossSprites = new BossSprites(Content, _spriteBatch);
            _enemySprites = new EnemySprites(Content, _spriteBatch);
            _npcSprites = new NPCSprites(Content, _spriteBatch);

            _debugRenderer = new DebugRenderer();
            _debugRenderer.Initialize(GraphicsDevice);

            bgm = Content.Load<Song>("Audio/DungeonTheme");
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;
        }

        private void OnGameModeChanged(GameMode newMode)
        {
            if (newMode == GameMode.Playing || newMode == GameMode.Horde)
            {
                GameSetUp();
            }
            else if (newMode == GameMode.GameOver)
            {
                _gameOverScreen.Reset();
            }
            else if (newMode == GameMode.GameWon)
            {
                _gameWonScreen.Reset();
            }
            else if (newMode == GameMode.TitleScreen)
            {
                _titleScreenController.Reset();
            }
        }

        /// <summary>
        /// Sets up the initial game state
        /// </summary>
        public void GameSetUp()
        {
            //Clears all the Colliders first
            CollisionManager.Instance.ClearAllColliders();

            _gamePlayer = new Player(this, _linkSprites, _itemSprites, _treasureSprites, Content);
            _kirbySprites = new KirbySprites(Content, _spriteBatch);

            SetCameraCommand moveCameraToPlayer = new(_camera, _gamePlayer);
            moveCameraToPlayer.Execute();

            // Universe creates its own manager instances
            _universe = new Universe(
                new EnemySprites(Content, _spriteBatch),
                new BossSprites(Content, _spriteBatch),
                new NPCSprites(Content, _spriteBatch),
                _itemSprites,
                _treasureSprites,
                new BlockSprites(Content, _spriteBatch),
                _linkSprites,
                Content,
                _camera
            );

            _kirby = new Kirby(
                _gamePlayer,
                _universe.EnemyManager,
                _kirbySprites,
                _gamePlayer.Position,
                Content
            );
            _universe.SetPlayer(_gamePlayer);
            _universe.SetKirby(_kirby);
            _universe.SetupPortalManager(_camera);
            _universe.SetupLockedEntranceManager(_camera);

            _areaConstructor = new CsvAreaConstructor();

            if(_gameState.CurrentMode == GameMode.Horde){
                string mapPath2 = Path.Combine(Content.RootDirectory, "Maps", "HordeDungeon.csv");
                Area HordeArea = _areaConstructor.LoadArea(mapPath2, _universe, _camera, "HordeDungeon");
                _universe.AddArea(HordeArea);
                _universe.SetCurrentLocation("HordeDungeon", 1);
                ItemManager projectileManager = new(_itemSprites,_treasureSprites,_bossSprites);
                HordeManager = new(_enemySprites,projectileManager,_bossSprites,_npcSprites,Content);
            }
            else{
                string mapPath = Path.Combine(Content.RootDirectory, "Maps", "testDungeon1.csv");
                Area testArea = _areaConstructor.LoadArea(mapPath, _universe, _camera, "TestDungeon");
                _universe.AddArea(testArea);
                _universe.SetCurrentLocation("TestDungeon", 1);
            }
            _gamePlayer.Position = _universe.CurrentRoom.GetPlayerSpawnPoint();
            _kirby.Position = _gamePlayer.Position;




            moveCameraToPlayer.Execute();

            _keyboardController = new KeyboardController(_gamePlayer);
            _keyboardController.BindKey(Keys.R, new ResetCommand(this));
            _keyboardController.BindKey(Keys.Q, new QuitCommand(this));
            _keyboardController.BindKey(Keys.E, new HurtPlayerCommand(this));
            _keyboardController.BindKey(Keys.I, new InventoryCommand(this));
            _keyboardController.BindKey(Keys.P, new PauseCommand(this));
            _keyboardController.BindKey(Keys.OemComma, new PreviousInventoryItemCommand(this));
            _keyboardController.BindKey(Keys.OemPeriod, new NextInventoryItemCommand(this));
            _keyboardController.BindKey(Keys.X, new ConfirmInventoryItemCommand(this));
            _keyboardController.BindKey(Keys.U, new KirbyUltCommand(this));
            // Initialize HUD Manager
            _hudManager = new HUDManager(_gamePlayer, _hudSprites, gamePaused);
            _hudManager.SetUniverse(_universe);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_gameState.CurrentMode != GameMode.GameOver)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }

            if (_gameState.CurrentMode == GameMode.TitleScreen)
            {
                int mode = _titleScreenController.ShouldStartGame();
                if (mode == 1)
                {
                    _gameState.SetMode(GameMode.Playing);
                }
                else if(mode == 2)
                {
                    _gameState.SetMode(GameMode.Horde);
                }
            }
            else if (_gameState.CurrentMode == GameMode.GameOver)
            {
                _gameOverScreen.Update();

                if (_gameOverScreen.ShouldQuit())
                {
                    Exit();
                }
                else if (_gameOverScreen.ShouldReturnToTitle())
                {
                    _gameState.SetMode(GameMode.TitleScreen);
                }
            }
            else if (_gameState.CurrentMode == GameMode.GameWon)
            {
                _gameWonScreen.Update();

                if (_gameWonScreen.ShouldQuit())
                {
                    Exit();
                }
                else if (_gameWonScreen.ShouldReturnToTitle())
                {
                    _gameState.SetMode(GameMode.TitleScreen);
                }
            }
            else if (_gameState.CurrentMode == GameMode.Playing)
            {
                // Always update keyboard and HUD
                _keyboardController.Update();
                _hudManager.Update(gameTime);

                // Update stuff when game is running
                if (!gamePaused)
                {
                    if (_gamePlayer.CurrentHealth <= 0)
                    {
                        SoundEffect gameOverSFX = Content.Load<SoundEffect>("Audio/GameOver");
                        gameOverSFX.Play();
                        _gameState.SetMode(GameMode.GameOver);
                    }
                    if (_gamePlayer.InventoryCount(typeof(Items.ItemStorages.Triforce)) > 0)
                    {
                        SoundEffect gameWonSFX = Content.Load<SoundEffect>("Audio/SuperSuccess");
                        gameWonSFX.Play();
                        _gameState.SetMode(GameMode.GameWon);
                    }
                    _universe.Update(gameTime);

                    _gamePlayer.Update(gameTime);

                    _kirby.Update(gameTime);

                    CollisionManager.Instance.Update(gameTime);

                    _camera.Update(gameTime, _gamePlayer.Position);
                }
            }
            else if(_gameState.CurrentMode == GameMode.Horde)
            {
                 // Always update keyboard and HUD
                _keyboardController.Update();
                _hudManager.Update(gameTime);

                // Update stuff when game is running
                if (!gamePaused)
                {
                    if(_gamePlayer.CurrentHealth <= 0)
                    {
                        SoundEffect gameOverSFX = Content.Load<SoundEffect>("Audio/GameOver");
                        gameOverSFX.Play();
                        _gameState.SetMode(GameMode.GameOver);
                    }
                    if (_gamePlayer.InventoryCount(typeof(Items.ItemStorages.Triforce)) > 0)
                    {
                        SoundEffect gameWonSFX = Content.Load<SoundEffect>("Audio/SuperSuccess");
                        gameWonSFX.Play();
                        _gameState.SetMode(GameMode.GameWon);
                    }
                    _universe.Update(gameTime);

                    _gamePlayer.Update(gameTime);

                    _kirby.Update(gameTime);

                    CollisionManager.Instance.Update(gameTime);

                    _camera.Update(gameTime, _gamePlayer.Position);
                    if (_universe.EnemyManager.IsEmpty())
                    {
                        _universe.EnemyManager.MakeEnemy("Aquamentus",new Vector2(300,300));
                    }
            }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (_gameState.CurrentMode == GameMode.TitleScreen)
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _titleSprites.Title().Draw(new Vector2(
                    GraphicsDevice.Viewport.Width / 2.0f,
                    GraphicsDevice.Viewport.Height / 2.0f
                    ));
                _spriteBatch.End();
            }
            else if (_gameState.CurrentMode == GameMode.GameOver)
            {
                _gameOverScreen.Draw(_spriteBatch);
            }
            else if (_gameState.CurrentMode == GameMode.GameWon)
            {
                _gameWonScreen.Draw(_spriteBatch);
            }
            else if (_gameState.CurrentMode == GameMode.Playing || _gameState.CurrentMode == GameMode.Horde)
            {
                // Draw world with camera transform
                _spriteBatch.Begin(
                    samplerState: SamplerState.PointClamp,
                    transformMatrix: _camera.GetTransformMatrix()
                );

                _universe.Draw();
                _kirby.Draw();
                _gamePlayer.Draw(_spriteBatch);


                _debugRenderer.DrawWorldDebug(_spriteBatch, _universe);

                _spriteBatch.End();

                _spriteBatch.Begin(
                    samplerState: SamplerState.PointClamp
                );
                _hudManager.Draw(_spriteBatch);

                _debugRenderer.DrawScreenDebug(_spriteBatch);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
