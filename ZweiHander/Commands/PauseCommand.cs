namespace ZweiHander.Commands
{
    public class PauseCommand : ICommand
    {
        private readonly Game1 _game;

        public PauseCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.gamePaused = !_game.gamePaused;
        }
    }
}