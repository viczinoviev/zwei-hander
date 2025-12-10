using Microsoft.Xna.Framework.Input;
using ZweiHander.Input;

namespace ZweiHander.GameStates
{
    public class GameOverController(KeyboardInputHandler inputHandler)
    {
        public void Reset()
        {
            inputHandler.Reset();
        }

        public void Update()
        {
            inputHandler.Update();
        }

        public bool ShouldReturnToTitle()
        {
            return inputHandler.IsKeyPressed(Keys.Space);
        }

        public bool ShouldQuit()
        {
            return inputHandler.IsAnyKeyPressed(Keys.Q, Keys.Escape);
        }
    }
}
