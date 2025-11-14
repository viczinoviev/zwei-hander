using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.GameStates
{
    public class GameState : IGameState
    {
        private bool _paused;
        private GameMode _currentMode;

        public bool IsPaused => _paused;
        public GameMode CurrentMode => _currentMode;

        public event System.Action<bool> PausedChanged;
        public event System.Action<GameMode> ModeChanged;

        public GameState()
        {
            _currentMode = GameMode.TitleScreen;
        }

        public void SetPaused(bool paused)
        {
            if (_paused == paused) return;
            _paused = paused;
            PausedChanged?.Invoke(_paused);
        }

        public void SetMode(GameMode mode)
        {
            if (_currentMode == mode) return;
            _currentMode = mode;
            ModeChanged?.Invoke(_currentMode);
        }
    }
}
