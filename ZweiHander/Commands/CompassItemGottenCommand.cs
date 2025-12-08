namespace ZweiHander.Commands
{
    public class CompassItemGottenCommand : ICommand
    {
        private readonly Game1 _game;

        public CompassItemGottenCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.HUDManager.compassItemGotten();
        }
    }
}