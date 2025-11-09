using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;

namespace ZweiHander.HUD
{
    public class HUDManager
    {
        private readonly List<IHUDComponent> _components;
        private readonly IHUDComponent _headsUpHud;
        private readonly IHUDComponent _healthDisplay;


        // HUD layout constants

        public const int HEADS_UP_HUD_X = 256;
        public const int HEADS_UP_HUD_Y = 56;
        

        private const int HUD_HEIGHT = 112; // Height of HUD area at top of screen
        
        private const int HEALTH_DISPLAY_X_OFFSET = 104; // X position for health display
        private const int HEALTH_DISPLAY_Y = 72; // Y position for health display
        

        
        public HUDManager(IPlayer player, HUDSprites hudSprites)
        {
            int screen_center_x = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
            _components = new List<IHUDComponent>();
            
            _headsUpHud = new HeadsUpHUD(hudSprites, new Vector2(screen_center_x, HEADS_UP_HUD_Y));
            _components.Add(_headsUpHud);

            // Create health display
            _healthDisplay = new HealthDisplay(
                player,
                hudSprites,
                new Vector2(screen_center_x + HEALTH_DISPLAY_X_OFFSET, HEALTH_DISPLAY_Y)
            );

            _components.Add(_healthDisplay);
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
