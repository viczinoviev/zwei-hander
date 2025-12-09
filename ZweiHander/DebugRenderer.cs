using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.CollisionFiles;
using ZweiHander.Map;

namespace ZweiHander
{
    public class DebugRenderer
    {
        private Texture2D _debugTexture;
        private readonly System.Collections.Generic.List<Rectangle> _clickableRectangles = [];

        public bool ShowCollisionBoxes { get; set; } = false;
        public bool ShowRoomBounds { get; set; } = false;
        public bool ShowGrid { get; set; } = false;
        public bool ShowClickableRectangles { get; set; } = false;

        public void Initialize(GraphicsDevice graphicsDevice)
        {
            if (_debugTexture == null)
            {
                _debugTexture = new Texture2D(graphicsDevice, 1, 1);
                _debugTexture.SetData([Color.White]);
            }
        }

        public void DrawWorldDebug(SpriteBatch spriteBatch, Universe universe)
        {
            if (ShowGrid)
            {
                DrawGrid(spriteBatch);
            }

            if (ShowCollisionBoxes)
            {
                DrawCollisionBoxes(spriteBatch);
            }

            if (ShowRoomBounds && universe?.CurrentRoom != null)
            {
                DrawRoomBounds(spriteBatch, universe.CurrentRoom);
            }
        }

        public void DrawScreenDebug(SpriteBatch spriteBatch)
        {
            if (ShowClickableRectangles)
            {
                DrawClickableRectangles(spriteBatch);
            }
        }

        public void AddClickableRectangle(Rectangle rect)
        {
            _clickableRectangles.Add(rect);
        }

        public void ClearClickableRectangles()
        {
            _clickableRectangles.Clear();
        }

        private void DrawClickableRectangles(SpriteBatch spriteBatch)
        {
            if (_debugTexture == null) return;

            Color fillColor = Color.Cyan * 0.3f;
            Color outlineColor = Color.Cyan * 0.8f;
            const int thickness = 2;

            foreach (var rect in _clickableRectangles)
            {
                spriteBatch.Draw(_debugTexture, rect, fillColor);

                spriteBatch.Draw(_debugTexture, new Rectangle(rect.X, rect.Y, rect.Width, thickness), outlineColor);
                spriteBatch.Draw(_debugTexture, new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness), outlineColor);
                spriteBatch.Draw(_debugTexture, new Rectangle(rect.X, rect.Y, thickness, rect.Height), outlineColor);
                spriteBatch.Draw(_debugTexture, new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), outlineColor);
            }
        }

        private void DrawCollisionBoxes(SpriteBatch spriteBatch)
        {
            if (_debugTexture == null) return;

            Color debugColor = Color.Red * 0.5f;

            foreach (var collider in CollisionManager.Instance.GetAllColliders())
            {
                if (collider?.Dead != false)
                    continue;

                spriteBatch.Draw(_debugTexture, collider.CollisionBox, debugColor);
            }
        }

        private void DrawRoomBounds(SpriteBatch spriteBatch, Room room)
        {
            if (_debugTexture == null || !room.IsLoaded) return;

            Rectangle bounds = new(
                (int)room.Position.X,
                (int)room.Position.Y,
                (int)room.Size.X,
                (int)room.Size.Y
            );

            Color fillColor = Color.Lime * 0.3f;
            Color outlineColor = Color.Lime * 0.8f;
            const int thickness = 2;

            spriteBatch.Draw(_debugTexture, bounds, fillColor);

            spriteBatch.Draw(_debugTexture, new Rectangle(bounds.X, bounds.Y, bounds.Width, thickness), outlineColor);
            spriteBatch.Draw(_debugTexture, new Rectangle(bounds.X, bounds.Bottom - thickness, bounds.Width, thickness), outlineColor);
            spriteBatch.Draw(_debugTexture, new Rectangle(bounds.X, bounds.Y, thickness, bounds.Height), outlineColor);
            spriteBatch.Draw(_debugTexture, new Rectangle(bounds.Right - thickness, bounds.Y, thickness, bounds.Height), outlineColor);
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            if (_debugTexture == null) return;

            const int gridSize = 32;
            const int dotSize = 2;
            const int gridRange = 20;

            for (int y = -gridRange; y <= gridRange; y++)
            {
                Rectangle dot = new(0 - (dotSize / 2), (y * gridSize) - (dotSize / 2), dotSize, dotSize);
                spriteBatch.Draw(_debugTexture, dot, Color.Yellow);
            }

            for (int x = -gridRange; x <= gridRange; x++)
            {
                Rectangle dot = new((x * gridSize) - (dotSize / 2), 0 - (dotSize / 2), dotSize, dotSize);
                spriteBatch.Draw(_debugTexture, dot, Color.Yellow);
            }

            Rectangle origin = new(-4, -4, 8, 8);
            spriteBatch.Draw(_debugTexture, origin, Color.Green);
        }
    }
}
