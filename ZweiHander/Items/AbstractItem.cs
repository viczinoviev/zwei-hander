using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Vector2 = Microsoft.Xna.Framework.Vector2;

using ZweiHander.Graphics;

namespace ZweiHander.Items;

/// <summary>
/// All Item classes implement this base class.
/// </summary>
public abstract class AbstractItem : IItem
{
    /// <summary>
    /// The sprite associated with this item.
    /// </summary>
    protected ISprite _sprite;

    public Vector2 position { get; set; } = default;

    public Vector2 velocity { get; set; } = default;

    public Vector2 acceleration { get; set; } = default;


    public virtual void Update(GameTime time)
    {
        float dt = (float) time.ElapsedGameTime.TotalSeconds;
        velocity += dt * acceleration;
        position += dt * velocity + (dt * dt / 2) * acceleration;
        _sprite.Update(time);
    }

    public void Draw()
    {
        _sprite.Draw(position);
    }
}