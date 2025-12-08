using ZweiHander.PlayerFiles;

namespace ZweiHander.Commands
{
    public class SetCameraCommand(Camera.Camera camera, IPlayer player) : ICommand
    {
        private readonly Camera.Camera _camera = camera;
        private readonly IPlayer _player = player;

        public void Execute()
        {
            _camera.SetPositionImmediate(_player.Position);
        }
    }
}
