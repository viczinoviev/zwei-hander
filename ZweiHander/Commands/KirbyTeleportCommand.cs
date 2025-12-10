using Microsoft.Xna.Framework;
using ZweiHander.FriendlyNPC;

namespace ZweiHander.Commands
{
    public class KirbyTeleportCommand(IKirby kirby, Vector2 targetPosition) : ICommand
    {
        private readonly IKirby _kirby = kirby;
        private readonly Vector2 _targetPosition = targetPosition;

        public void Execute()
        {
            if (_kirby != null)
            {
                _kirby.Position = _targetPosition;
            }
        }
    }
}
