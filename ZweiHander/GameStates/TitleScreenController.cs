using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace ZweiHander.GameStates
{
    /// <summary>
    /// Handles input during the title screen state
    /// </summary>
    public class TitleScreenController
    {
        private KeyboardState _previousKeyState;

        public TitleScreenController()
        {
            _previousKeyState = Keyboard.GetState();
        }

        public void Reset()
        {
            _previousKeyState = Keyboard.GetState();
        }

        public int ShouldStartGame()
        {
            KeyboardState currentKeyState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyState.GetPressedKeys();
            Keys[] previousPressedKeys = _previousKeyState.GetPressedKeys();

            // Check if any new key was pressed this frame
            bool anyKeyPressed = pressedKeys.Any(key => !previousPressedKeys.Contains(key));

            if (anyKeyPressed)
            {
                // Check if the pressed key is not Q or Escape
                bool isQuitKey = currentKeyState.IsKeyDown(Keys.Q) || currentKeyState.IsKeyDown(Keys.Escape);

                _previousKeyState = currentKeyState;
                if(!isQuitKey){
                if (currentKeyState.IsKeyDown(Keys.H)){
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            _previousKeyState = currentKeyState;
            return 0;
        }
    }
}
