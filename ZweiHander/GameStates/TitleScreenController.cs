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

        /// <summary>
        /// Checks if any valid key was pressed to start the game
        /// </summary>
        /// <returns>True if player pressed any key except Q or Escape</returns>
        public bool ShouldStartGame()
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
                return !isQuitKey;
            }

            _previousKeyState = currentKeyState;
            return false;
        }
    }
}
