using Microsoft.Xna.Framework;
using ZweiHander.PlayerFiles;

namespace ZweiHander.Commands
{
    public class SetCameraCommand : ICommand
    {
        private readonly Camera.Camera _camera;
        private readonly IPlayer _player;

        public SetCameraCommand(Camera.Camera camera, IPlayer player)
        {
            _camera = camera;
            _player = player;
        }

        public void Execute()
        {
            _camera.SetPositionImmediate(_player.Position);
        }
    }
}
