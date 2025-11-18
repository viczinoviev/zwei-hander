using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Input;

namespace ZweiHander.GameStates
{
    public class GameWonScreen
    {
        private readonly GameWonController _controller;
        private readonly SpriteFont _font;
        private readonly GraphicsDevice _graphicsDevice;

        public GameWonScreen(ContentManager content, GraphicsDevice graphicsDevice)
        {
            KeyboardInputHandler inputHandler = new KeyboardInputHandler();
            _controller = new GameWonController(inputHandler);
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

            string GameWonText = "GameWon";
            string quitText = "Press Q or ESC to quit";
            string restartText = "Press SPACE to restart";

            float instructionScale = 0.5f;
            float lineSpacing = 30f;

            Vector2 GameWonSize = _font.MeasureString(GameWonText);
            Vector2 quitSize = _font.MeasureString(quitText) * instructionScale;
            Vector2 restartSize = _font.MeasureString(restartText) * instructionScale;

            float totalHeight = GameWonSize.Y + lineSpacing + quitSize.Y + lineSpacing + restartSize.Y;
            float startY = (_graphicsDevice.Viewport.Height - totalHeight) / 2.0f;

            Vector2 GameWonPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - GameWonSize.X) / 2.0f,
                startY
            );

            Vector2 quitPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - quitSize.X) / 2.0f,
                startY + GameWonSize.Y + lineSpacing
            );

            Vector2 restartPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - restartSize.X) / 2.0f,
                startY + GameWonSize.Y + lineSpacing + quitSize.Y + lineSpacing
            );

            spriteBatch.DrawString(_font, GameWonText, GameWonPosition, Color.White);
            spriteBatch.DrawString(_font, quitText, quitPosition, Color.White, 0f, Vector2.Zero, instructionScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, restartText, restartPosition, Color.White, 0f, Vector2.Zero, instructionScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);

            spriteBatch.End();
        }
    }
}
