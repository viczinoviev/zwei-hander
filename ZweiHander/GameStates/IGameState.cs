using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.GameStates
{
    public interface IGameState
    {
        bool IsPaused { get; }
        void SetPaused(bool paused);
        event System.Action<bool> PausedChanged;

        GameMode CurrentMode { get; }
        void SetMode(GameMode mode);
        event System.Action<GameMode> ModeChanged;
    }
}
