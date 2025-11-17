using Microsoft.Xna.Framework.Input;

namespace ZweiHander.Input
{
    public class KeyboardInputHandler
    {
        private KeyboardState _previousState;
        private KeyboardState _currentState;

        public KeyboardInputHandler()
        {
            _previousState = Keyboard.GetState();
            _currentState = _previousState;
        }

        public void Update()
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public void Reset()
        {
            _previousState = Keyboard.GetState();
            _currentState = _previousState;
        }

        public bool IsKeyPressed(Keys key)
        {
            return _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key);
        }

        public bool IsAnyKeyPressed(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (IsKeyPressed(key))
                    return true;
            }
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }
    }
}
