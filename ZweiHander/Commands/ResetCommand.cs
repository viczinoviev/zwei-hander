namespace ZweiHander.Commands
{
    public class ResetCommand : ICommand
    {
        private readonly Game1 _game;

        public ResetCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.GameSetUp();
        }
    }
}
