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
        private readonly HealthDisplay _healthDisplay;

        // HUD layout constants
        private const float HUD_HEIGHT = 88f; // Height of HUD area at top of screen
        private const float HEALTH_DISPLAY_X = 640f; // X position for health display
        private const float HEALTH_DISPLAY_Y = 64f; // Y position for health display
        
        public HUDManager(IPlayer player, HUDSprites hudSprites)
        {
            _components = new List<IHUDComponent>();

            // Create health display
            _healthDisplay = new HealthDisplay(
                player,
                hudSprites,
                new Vector2(HEALTH_DISPLAY_X, HEALTH_DISPLAY_Y)
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
