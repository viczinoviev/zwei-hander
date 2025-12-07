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
            bool open = !_game.HUDManager.IsHUDOpen;

            // Toggle inventory HUD
            _game.HUDManager.IsHUDOpen = open;

            // Pause or unpause the world as well
            _game.gamePaused = open;
        }
    }
}