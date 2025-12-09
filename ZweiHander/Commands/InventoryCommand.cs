namespace ZweiHander.Commands
{
    public class InventoryCommand(Game1 game) : ICommand
    {
        private readonly Game1 _game = game;

        public void Execute()
        {
            bool open = !_game.HUDManager.IsHUDOpen;

            // Toggle inventory HUD
            _game.HUDManager.IsHUDOpen = open;

            // Pause or unpause the world as well
            _game.gamePaused = open;
        }
    }
}