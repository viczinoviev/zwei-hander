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
        private float _flashDuration = 1f;
        private float _flashInterval = 0.075f;
        private float _timeElapsed = 0f;
        private float _intervalElapsed = 0f;
        private bool _isFlashing = false;

        public HurtPlayerCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _timeElapsed = 0f;
            _intervalElapsed = 0f;
            _isFlashing = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isFlashing) return;

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timeElapsed += delta;
            _intervalElapsed += delta;

            if (_timeElapsed < _flashDuration)
            {
                if (_intervalElapsed >= _flashInterval)
                {
                    _game.GamePlayer.Color = _game.GamePlayer.Color == Color.White ? new Color(0f, 0f, 0f, 0f) : Color.White;
                    _intervalElapsed = 0f;
                }
                

            }
            else
            {
                _isFlashing = false;
                _game.GamePlayer.Color = Color.White;

            }
        }
    }
}
