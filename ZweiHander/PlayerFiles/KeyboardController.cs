using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ZweiHander.Commands;

namespace ZweiHander.PlayerFiles
{
    public class KeyboardController : IController
    {
        private readonly Player _player;
        private KeyboardState _previousKeyboardState;
        private Dictionary<Keys, Action> _keyBindings;
        private Dictionary<Keys, ICommand> _commandBindings;


        public KeyboardController(Player player)
        {
            _player = player;
            InitializeKeyBindings();
        }

        private void InitializeKeyBindings()
        {
            _keyBindings = new Dictionary<Keys, Action>
        {
            { Keys.W, () => _player.MoveUp() },
            { Keys.Up, () => _player.MoveUp() },
            { Keys.S, () => _player.MoveDown() },
            { Keys.Down, () => _player.MoveDown() },
            { Keys.A, () => _player.MoveLeft() },
            { Keys.Left, () => _player.MoveLeft() },
            { Keys.D, () => _player.MoveRight() },
            { Keys.Right, () => _player.MoveRight() },
            { Keys.Z, () => _player.Attack() },
            { Keys.N, () => _player.Attack() },
            { Keys.D1, () => _player.UseItem1() },
            { Keys.D2, () => _player.UseItem2() },
            { Keys.D3, () => _player.UseItem3() },
            { Keys.D4, () => _player.UseItem4() }
        };
        }

        public void BindKey(Keys key, ICommand command)
        {
            #pragma warning disable //Ignoring the one warning here
            _commandBindings ??= new Dictionary<Keys, ICommand>();
            #pragma warning restore

            _commandBindings[key] = command;
        }


        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Clear input buffer each frame
            _player.ClearInputBuffer();

            // Read all current inputs and add to buffer
            foreach (var keyBinding in _keyBindings)
            {
                if (currentKeyboardState.IsKeyDown(keyBinding.Key))
                {
                    keyBinding.Value();
                }
            }

            // Handle command bindings
            if (_commandBindings != null)
            {
                foreach (var commandBinding in _commandBindings)
                {
                    if (currentKeyboardState.IsKeyDown(commandBinding.Key) && !_previousKeyboardState.IsKeyDown(commandBinding.Key))
                    {
                        commandBinding.Value.Execute();
                    }
                }
            }

            _previousKeyboardState = currentKeyboardState;
        }
    }
}