using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Commands
{
    public class ChangeBlockCommand : ICommand
    {
        private Game1 _game;
        private int _direction;

        public ChangeBlockCommand(Game1 game, int direction)
        {
            _game = game;
            _direction = direction; 
        }

        public void Execute()
        {
            _game.BlockIndex += _direction;
            int lastIndex = _game.BlockList.Count-1;

            if (_game.BlockIndex < 0)
            {
                _game.BlockIndex = lastIndex;
            }
            else if (_game.BlockIndex > lastIndex)
            {
                _game.BlockIndex = 0;
            }

            //Updates the block 
            _game.Block = _game.BlockList[_game.BlockIndex];
        }
    }
}
