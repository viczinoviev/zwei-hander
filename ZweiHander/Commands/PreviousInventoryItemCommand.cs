namespace ZweiHander.Commands
{
    public class PreviousInventoryItemCommand : ICommand
    {
        private readonly Game1 _game;

        public PreviousInventoryItemCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.HUDManager.SelectPreviousInventoryItem();
        }
    }
}
