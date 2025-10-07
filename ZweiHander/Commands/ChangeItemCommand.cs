namespace ZweiHander.Commands;

public class ChangeItemCommand(Game1 game, int direction) : ICommand
{
    private readonly Game1 _game = game;
    private readonly int _direction = direction;

    public void Execute()
    {
        // Extra "+ _game.ItemCount" since C# has negative remainders which we do not want
        _game.ItemIndex = (_game.ItemIndex + _direction + _game.ItemCount) % _game.ItemCount;
    }
}