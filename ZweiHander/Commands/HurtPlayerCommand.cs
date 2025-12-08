namespace ZweiHander.Commands
{
    public class HurtPlayerCommand(Game1 game) : ICommand
    {
        private readonly Game1 _game = game;

        public void Execute()
        {
            _game.GamePlayer.TakeDamage();
        }
    }
}
