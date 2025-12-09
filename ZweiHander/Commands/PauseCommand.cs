namespace ZweiHander.Commands
{
    public class PauseCommand(Game1 game) : ICommand
    {
        private readonly Game1 _game = game;

        public void Execute()
        {
            _game.gamePaused = !_game.gamePaused;
        }
    }
}