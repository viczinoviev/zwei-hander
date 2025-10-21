using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ZweiHander.Commands
{
    public class HurtPlayerCommand : ICommand
    {
        private Game1 _game;

        public HurtPlayerCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.GamePlayer.TakeDamage();
        }

        public void Update(GameTime gameTime)
        {
            //Player handles TakeDamage logic
        }
    }
}
