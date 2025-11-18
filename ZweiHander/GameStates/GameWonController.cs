using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZweiHander.Input;

namespace ZweiHander.GameStates
{
    public class GameWonController
    {
        private readonly KeyboardInputHandler _inputHandler;

        public GameWonController(KeyboardInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public void Reset()
        {
            _inputHandler.Reset();
        }

        public void Update(GameTime gameTime)
        {
            _inputHandler.Update();
        }

        public bool ShouldReturnToTitle()
        {
            return _inputHandler.IsKeyPressed(Keys.Space);
        }

        public bool ShouldQuit()
        {
            return _inputHandler.IsAnyKeyPressed(Keys.Q, Keys.Escape);
        }
    }
}
