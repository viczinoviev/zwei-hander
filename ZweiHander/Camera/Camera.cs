using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Map;

namespace ZweiHander.Camera
{
    public class Camera
    {
        /// <summary>
        /// Position of the center point of camera
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Desired position of the camera
        /// </summary>
        public Vector2 DesiredPosition { get; private set; }

        /// <summary>
        /// Dimensions of the game screen
        /// </summary>
        public Viewport Viewport { get; private set; }

        /// <summary>
		/// Speed of camera smoothing, between 0 and 1, where 1 is instant movement
		/// </summary>
        private float SmoothSpeed = 0.1f;

        private bool _isOverridden;
        private Vector2 _overrideStartPosition;
        private Vector2 _overrideTargetPosition;
        private float _overrideElapsedTime;
        private float _overrideDuration;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
            Position = Vector2.Zero;
            DesiredPosition = Vector2.Zero;
            _isOverridden = false;
        }

        /// <summary>
        /// Sets the smoothing speed of the camera
        /// </summary>
        public void SetSmoothSpeed(float speed)
        {
            SmoothSpeed = speed;
        }

        public void SetPositionImmediate(Vector2 playerPosition)
        {
            Vector2 cameraPosition = playerPosition - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
            Position = cameraPosition;
            DesiredPosition = cameraPosition;
            _isOverridden = false;
        }

        public void OverrideMotion(Vector2 targetWorldPosition, float duration)
        {
            _isOverridden = true;
            _overrideStartPosition = Position;
            _overrideTargetPosition = targetWorldPosition - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
            _overrideElapsedTime = 0f;
            _overrideDuration = duration;
        }

        public void CancelOverride()
        {
            _isOverridden = false;
        }

        public bool IsOverridden => _isOverridden;

        public void Update(GameTime gameTime, Vector2 target)
        {
            if (_isOverridden)
            {
                _overrideElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;
                float t = MathHelper.Clamp(_overrideElapsedTime / _overrideDuration, 0f, 1f);
                
                t = t * t * (3f - 2f * t);
                
                Position = Vector2.Lerp(_overrideStartPosition, _overrideTargetPosition, t);
                DesiredPosition = Position;

                if (_overrideElapsedTime >= _overrideDuration)
                {
                    _isOverridden = false;
                }
            }
            else
            {
                DesiredPosition = target - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
                Position += (DesiredPosition - Position) * SmoothSpeed;
            }
        }

        /// <summary>
        /// Gets the transformation matrix for SpriteBatch rendering.
        /// </summary>
        public Matrix GetTransformMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
        }
    }
}