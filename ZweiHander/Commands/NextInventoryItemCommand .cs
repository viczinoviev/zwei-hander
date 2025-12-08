namespace ZweiHander.Commands
{
    public class NextInventoryItemCommand : ICommand
    {
        private readonly Game1 _game;

        public NextInventoryItemCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.HUDManager.SelectNextInventoryItem();
        }
    }
}