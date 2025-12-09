using Microsoft.Xna.Framework;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Commands
{
    public class PlayerTeleportCommand(IPlayer player, Vector2 targetPosition) : ICommand
    {
        private readonly IPlayer _player = player;
        private readonly Vector2 _targetPosition = targetPosition;

        public void Execute()
        {
            _player.Position = _targetPosition;
            _player.ForceUpdateCollisionBox();
        }
    }
}
