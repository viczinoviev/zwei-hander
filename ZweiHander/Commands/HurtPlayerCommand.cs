namespace ZweiHander.Commands
{
    public class HurtPlayerCommand : ICommand
    {
        private Game1 _game;

        public HurtPlayerCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.GamePlayer.TakeDamage();
        }
    }
}
