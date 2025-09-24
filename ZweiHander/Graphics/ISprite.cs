using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZweiHander.Graphics;

public interface ISprite
{
    /// <summary>
    /// Draws this sprite on screen.
    /// </summary>
    /// <param name="position">The xy-coordinate location to draw this sprite on the screen.</param>
    public void Draw(Vector2 position);

    /// <summary>
    /// Updates this sprite.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime);
}
