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

        /// <summary>
		/// Flag to check if initial camera setup has been completed
		/// </summary>
        private bool initialSetUpCompleted = false;

        public Camera(Viewport viewport)
        {
            Viewport = viewport;
            Position = Vector2.Zero;
            DesiredPosition = Vector2.Zero;


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
        }

        public void Update(GameTime gameTime, Vector2 target)
        {
            // Set desired position to center on player
            DesiredPosition = target - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);

            // Smoothly interpolate camera position towards desired position
            Position += (DesiredPosition - Position) * SmoothSpeed;
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