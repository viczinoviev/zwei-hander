using Microsoft.Xna.Framework;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Commands
{
    public class PlayerTeleportCommand : ICommand
    {
        private readonly IPlayer _player;
        private readonly Vector2 _targetPosition;

        public PlayerTeleportCommand(IPlayer player, Vector2 targetPosition)
        {
            _player = player;
            _targetPosition = targetPosition;
        }

        public void Execute()
        {
            _player.Position = _targetPosition;
            _player.ForceUpdateCollisionBox();
        }
    }
}
