namespace ZweiHander.Commands;

public class ChangeItemCommand : ICommand
{
    private Game1 _game;
    private int _direction;

    public ChangeItemCommand(Game1 game, int direction)
    {
        _game = game;
        _direction = direction;
    }

    public void Execute()
    {
        // Extra "+ _game.ItemCount" since C# has negative remainders which we do not want
        _game.ItemIndex = (_game.ItemIndex + _direction + _game.ItemCount) % _game.ItemCount;
    }
}