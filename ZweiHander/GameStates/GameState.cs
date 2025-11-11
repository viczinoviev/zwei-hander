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
        public bool IsPaused => _paused;

        public event System.Action<bool> PausedChanged;

        public void SetPaused(bool paused)
        {
            if (_paused == paused) return;
            _paused = paused;
            PausedChanged?.Invoke(_paused);
        }
    }
}
