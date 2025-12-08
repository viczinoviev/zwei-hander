namespace ZweiHander.Commands
{
    public class MapItemGottenCommand : ICommand
    {
        private readonly Game1 _game;

        public MapItemGottenCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.HUDManager.mapItemGotten();
        }
    }
}