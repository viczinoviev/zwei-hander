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
        /// Reference to dungeon for room checking
        /// </summary>
        private Dungeon _dungeon;

        /// <summary>
		/// Speed of camera smoothing, between 0 and 1, where 1 is instant movement
		/// </summary>
        private float SmoothSpeed = 0.1f;

        /// <summary>
        /// The room camera is currently looking at
        /// </summary>
        private Room _currentRoom;

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

        /// <summary>
        /// Sets the dungeon for camera to check room the player is in
        /// </summary>
        public void SetDungeon(Dungeon dungeon)
        {
            _dungeon = dungeon;
        }

        public void Update(Vector2 target)
        {
            // Smoothly interpolate camera position towards desired position
            Position += (DesiredPosition - Position) * SmoothSpeed;

            if (_dungeon == null) return;

            Room targetRoom = _dungeon.GetRoomAtPosition(target);

            // change camera if player entered new room
            if (targetRoom != null && targetRoom != _currentRoom)
            {
                _currentRoom = targetRoom;
                CenterCameraOnRoom();
            }
        }

        /// <summary>
        /// Centers the camera on the current room
        /// </summary>
        private void CenterCameraOnRoom()
        {
            if (_currentRoom == null) return;

            Vector2 roomCenter = _currentRoom.Position + _currentRoom.Size / 2f;

            DesiredPosition = roomCenter - new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
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
