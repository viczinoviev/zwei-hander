using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.FriendlyNPC;

namespace ZweiHander.Commands
{
    public class KirbyUltCommand(Game1 game) : ICommand
    {
        private readonly Game1 _game = game;

        public void Execute()
        {
            if (_game.GameKirby != null) 
            { 
                _game.GameKirby.StartUlt();
            }
        }
    }
}
