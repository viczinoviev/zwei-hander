using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.HUD;

namespace ZweiHander.Commands
{
    public class MapItemGottenCommand : ICommand
    {
        private readonly Game1 _game;

        public MapItemGottenCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.HUDManager.mapItemGotten();
        }
    }
}