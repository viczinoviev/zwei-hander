using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ZweiHander.Commands;

public class KeyboardController : IController
{
    private Player _player;
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
            { Keys.D2, () => _player.UseItem2() }
        };
    }

    public void BindKey(Keys key, ICommand command)
    {
        if (_commandBindings == null)
            _commandBindings = new Dictionary<Keys, ICommand>();

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