namespace ZweiHander.Commands
{
    public class PreviousInventoryItemCommand(Game1 game) : ICommand
    {
        private readonly Game1 _game = game;

        public void Execute()
        {
            _game.HUDManager.SelectPreviousInventoryItem();
        }
    }
}
