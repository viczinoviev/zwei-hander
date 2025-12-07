using Microsoft.Xna.Framework;
using ZweiHander.FriendlyNPC;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Commands
{
    public class KirbyTeleportCommand(IKirby kirby, Vector2 targetPosition) : ICommand
    {
        private readonly IKirby _kirby = kirby;
        private readonly Vector2 _targetPosition = targetPosition;

        public void Execute()
        {
            _kirby.Position = _targetPosition;
        }
    }
}
