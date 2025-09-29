using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Commands
{
    public class QuitCommand : ICommand
    {
        private Game1 _game;

        public QuitCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Exit();
        }
    }
}
