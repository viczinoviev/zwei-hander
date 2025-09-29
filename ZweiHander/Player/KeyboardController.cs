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
            { Keys.Space, () => _player.Attack() }
            
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
        bool anyKeyPressed = false;

        foreach (var keyBinding in _keyBindings)
        {
            if (currentKeyboardState.IsKeyDown(keyBinding.Key))
            {
                keyBinding.Value();
                anyKeyPressed = true;
                break;
            }
        }

        if (!anyKeyPressed && _commandBindings != null)
        {
            foreach (var commandBinding in _commandBindings)
            {
                if (currentKeyboardState.IsKeyDown(commandBinding.Key) && !_previousKeyboardState.IsKeyDown(commandBinding.Key))
                {
                    commandBinding.Value.Execute();
                    anyKeyPressed = true;
                    break;
                }
            }
        }

        if (!anyKeyPressed)
        {
            _player.Idle();
        }

        _previousKeyboardState = currentKeyboardState;
    }
}