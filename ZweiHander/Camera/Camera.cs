using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Camera
{
    public class Camera(Viewport viewport)
    {
        /// <summary>
        /// Position of the center point of camera
        /// </summary>
        public Vector2 Position { get; private set; } = Vector2.Zero;
        /// <summary>
        /// Dimentions of the game screen
        /// </summary>
        public Viewport Viewport { get; private set; } = viewport;

        /// <summary>
        /// Updates camera position to center <target>
        /// </summary>
        public void Update(Vector2 target)
        {
            Position = target - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
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
