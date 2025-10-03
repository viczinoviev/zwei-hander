using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Commands
{
    public class ChangeEnemyCommand : ICommand
    {
        private Game1 _game;
        private int _direction;

        public ChangeEnemyCommand(Game1 game, int direction)
        {
            _game = game;
            _direction = direction; 
        }

        public void Execute()
        {
            _game.EnemyIndex += _direction;
            int lastIndex = _game.EnemyList.Count-1;

            if (_game.EnemyIndex < 0)
            {
                _game.EnemyIndex = lastIndex;
            }
            else if (_game.EnemyIndex > lastIndex)
            {
                _game.EnemyIndex = 0;
            }

            //Updates the Enemy 
            _game.Enemy = _game.EnemyList[_game.EnemyIndex];
        }
    }
}
