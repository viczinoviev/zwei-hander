using Microsoft.Xna.Framework;
using ZweiHander.Graphics;

namespace ZweiHander.Damage;
public class DamageDisplay
{
    protected int Damage;
    protected double Duration;
    protected double Countdown;
    protected NumberSprite Sprite;
    protected Vector2 Position;
    public bool Finished => Countdown <= 0;

    public DamageDisplay(int damage, Vector2 position, double duration = 1, float size = 1f)
    {
        Damage = damage;
        Duration = duration;
        Countdown = duration;
        Sprite = new(damage)
        {
            Scale = new(size, size)
        };
        Position = position;
    }

    public void Update(GameTime gameTime)
    {
        Countdown -= gameTime.ElapsedGameTime.TotalSeconds;
        Countdown = Countdown < 0 ? 0 : Countdown;
        Sprite.Update(gameTime);
    }

    public void Draw()
    {
        Color color = Sprite.Color;
        color.A = (byte)(Countdown / Duration * 255);
        Sprite.Color = color;
        Sprite.Draw(Position);
    }
}
