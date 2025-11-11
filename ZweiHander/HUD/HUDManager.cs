using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Numerics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.HUD
{
    public class HUDManager
    {
        public bool Paused
        {
            get => _paused;
            set => SetPaused(value);
        }
        private List<IHUDComponent> _components;
        private IHUDComponent _headsUpHud;
        private IHUDComponent _healthDisplay;
        private IHUDComponent _inventoryHUD;
        private IHUDComponent _mapHUD;
        private HUDSprites _hudSprites;
        private IPlayer _player;
        private bool _paused;

        // HUD layout constants

        public const int HEADS_UP_HUD_X = 256;
        public const int HEADS_UP_HUD_Y = 56;
        

        private int HUD_HEIGHT = 112; // Height of HUD area at top of screen
        
        private const int HEALTH_DISPLAY_X_OFFSET = 104; // X position for health display
        private int HEALTH_DISPLAY_Y = 72; // Y position for health display
        

        
        public HUDManager(IPlayer player, HUDSprites hudSprites, bool gamePaused)
        {

            _player = player;
            _hudSprites = hudSprites;
            _paused = gamePaused;

            BuildHUD();

            
        }

        private void BuildHUD()
        {
            int screen_center_x = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            _components = new List<IHUDComponent>();

            if (!_paused)//normal game state
            {
                _headsUpHud = new HeadsUpHUD(_hudSprites, new Vector2(screen_center_x, HEADS_UP_HUD_Y));
                HUD_HEIGHT = 112;
                HEALTH_DISPLAY_Y = 72;
            }
            else//paused game state
            {
                _inventoryHUD = new InventoryHUD(_hudSprites, new Vector2(screen_center_x, HEADS_UP_HUD_Y), _player);
                _mapHUD = new MapHUD(_hudSprites, new Vector2(screen_center_x, 245));
                _headsUpHud = new HeadsUpHUD(_hudSprites, new Vector2(screen_center_x, 380));
                HEALTH_DISPLAY_Y = 396;
                HUD_HEIGHT = 1000;
                _components.Add(_inventoryHUD);
                _components.Add(_mapHUD);
            }
            // Create health display
            _healthDisplay = new HealthDisplay(
                _player,
                _hudSprites,
                new Vector2(screen_center_x + HEALTH_DISPLAY_X_OFFSET, HEALTH_DISPLAY_Y)
            );
            _components.Add(_headsUpHud);
            _components.Add(_healthDisplay);
        }

        public void SetPaused(bool paused)
        {
            if (_paused == paused)
                return; // No need to rebuild if nothing changed

            _paused = paused;
            BuildHUD();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw black background for HUD area
            Texture2D pixel = CreatePixelTexture(spriteBatch.GraphicsDevice);
            
            spriteBatch.Draw(
                pixel,
                new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, (int)HUD_HEIGHT),
                Color.Black
            );

            // Draw all HUD components
            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }
        

        /// <summary>
        /// Helper method to create a 1x1 white pixel texture for drawing rectangles
        /// </summary>
        private Texture2D CreatePixelTexture(GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
            return texture;
        }
    }
}
