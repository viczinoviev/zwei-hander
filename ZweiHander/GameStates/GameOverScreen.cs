using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Input;

namespace ZweiHander.GameStates
{
    public class GameOverScreen
    {
        private readonly GameOverController _controller;
        private readonly SpriteFont _font;
        private readonly GraphicsDevice _graphicsDevice;

        public GameOverScreen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            KeyboardInputHandler inputHandler = new KeyboardInputHandler();
            _controller = new GameOverController(inputHandler);
            _font = content.Load<SpriteFont>("Fonts/GameOverFont");
            _graphicsDevice = graphicsDevice;
        }

        public void Reset()
        {
            _controller.Reset();
        }

        public void Update(GameTime gameTime)
        {
            _controller.Update(gameTime);
        }

        public bool ShouldReturnToTitle()
        {
            return _controller.ShouldReturnToTitle();
        }

        public bool ShouldQuit()
        {
            return _controller.ShouldQuit();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            string gameOverText = "GAMEOVER";
            string quitText = "Press Q or ESC to quit";
            string restartText = "Press SPACE to restart";

            float instructionScale = 0.5f;
            float lineSpacing = 30f;

            Vector2 gameOverSize = _font.MeasureString(gameOverText);
            Vector2 quitSize = _font.MeasureString(quitText) * instructionScale;
            Vector2 restartSize = _font.MeasureString(restartText) * instructionScale;

            float totalHeight = gameOverSize.Y + lineSpacing + quitSize.Y + lineSpacing + restartSize.Y;
            float startY = (_graphicsDevice.Viewport.Height - totalHeight) / 2.0f;

            Vector2 gameOverPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - gameOverSize.X) / 2.0f,
                startY
            );

            Vector2 quitPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - quitSize.X) / 2.0f,
                startY + gameOverSize.Y + lineSpacing
            );

            Vector2 restartPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - restartSize.X) / 2.0f,
                startY + gameOverSize.Y + lineSpacing + quitSize.Y + lineSpacing
            );

            spriteBatch.DrawString(_font, gameOverText, gameOverPosition, Color.White);
            spriteBatch.DrawString(_font, quitText, quitPosition, Color.White, 0f, Vector2.Zero, instructionScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, restartText, restartPosition, Color.White, 0f, Vector2.Zero, instructionScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);

            spriteBatch.End();
        }
    }
}
