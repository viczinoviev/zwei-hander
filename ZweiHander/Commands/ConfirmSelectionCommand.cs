namespace ZweiHander.Commands
{
    public class ConfirmInventoryItemCommand : ICommand
    {
        private readonly Game1 _game;

        public ConfirmInventoryItemCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.HUDManager.ConfirmInventorySelection();
        }
    }
}