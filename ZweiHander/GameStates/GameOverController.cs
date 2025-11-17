using Microsoft.Xna.Framework;

namespace ZweiHander.GameStates
{
    public class GameOverController
    {
        private float _timer;
        private const float GAME_OVER_DURATION = 4.0f;

        public GameOverController()
        {
            _timer = 0f;
        }

        public void Reset()
        {
            _timer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool ShouldReturnToTitle()
        {
            return _timer >= GAME_OVER_DURATION;
        }
    }
}
