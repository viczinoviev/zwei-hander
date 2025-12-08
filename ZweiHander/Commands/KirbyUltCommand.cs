using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.FriendlyNPC;

namespace ZweiHander.Commands
{
    public class KirbyUltCommand : ICommand
    {
        private Game1 _game;

        public KirbyUltCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.GameKirby.StartUlt();
        }
    }
}
