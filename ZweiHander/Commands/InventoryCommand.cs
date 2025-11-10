using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.HUD;

namespace ZweiHander.Commands
{
    public class InventoryCommand : ICommand
    {
        private readonly Game1 _game;

        public InventoryCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            // Flip the pause state
            _game.gamePaused = !_game.gamePaused;

            // Update HUDManager's paused value
            _game.HUDManager.Paused = _game.gamePaused;
        }
    }
}
