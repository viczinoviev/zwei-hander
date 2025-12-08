using Microsoft.Xna.Framework;
using System;
using ZweiHander.CollisionFiles;

namespace ZweiHander.Items.ItemStorages;
/// <summary>
/// DeleteOnBlock, infinite life<br></br>
/// use ItemHelper.BoomerangTrajectory<br></br>
/// Phase 0: Move out at given acceleration until velocity switches direction<br></br>
/// Phase 1: Return to thrower at increasing speeds<br></br>
/// EXTRAS: (Func&lt;Vector2&gt; throwerPosition, ICollisionHandler thrower, double ReturnAcceleration = |Acceleration|)
/// </summary>
public class Boomerang : AbstractItem
{
    protected override ItemProperty Properties { get; set; } = ItemProperty.DeleteOnBlock;

    protected override double Life { get; set; } = -1;

    /// <summary>
    /// Sign of boomerang velocity, for seeing when to switch phase
    /// </summary>
    protected Vector2 Signs { get; set; } = Vector2.Zero;

    /// <summary>
    /// "Acceleration" for returning to player
    /// </summary>
    protected double ReturnAcceleration { get; set; } = 0;

    /// <summary>
    /// Speed for returning to player
    /// </summary>
    protected double ReturnSpeed { get; set; } = 0f;

    /// <summary>
    /// Reference to ThrowerPosition
    /// </summary>
    protected Func<Vector2> ThrowerPositon { get; set; }

    /// <summary>
    /// Collision Handler for thrower
    /// </summary>
    protected ICollisionHandler Thrower { get; set; }

    public Boomerang(ItemConstructor itemConstructor)
        : base(itemConstructor)
    {
        Sprites = [itemConstructor.ItemSprites.Boomerang()];
        ThrowerPositon = (Func<Vector2>)itemConstructor.Extras[0];
        Thrower = (ICollisionHandler)itemConstructor.Extras[1];
        if (itemConstructor.Extras.Count > 2)
        {
            ReturnAcceleration = (double)itemConstructor.Extras[2];
        }
        else
        {
            ReturnAcceleration = Acceleration.Length();
        }
        Setup();
    }

    public override void Update(GameTime time)
    {
        base.Update(time);
        if (Phase == 0)
        {
            if (Signs == Vector2.Zero)
            {
                Signs = new(Math.Sign(Velocity.X), Math.Sign(Velocity.Y));
            }
            if (ReturnAcceleration <= 0)
            {
                ReturnAcceleration = Acceleration.Length();
            }
            if (Math.Sign(Velocity.X) != Signs.X || Math.Sign(Velocity.Y) != Signs.Y)
            {
                Phase++;
            }
        }
        else
        {

            double dt = time.ElapsedGameTime.TotalSeconds;
            ReturnSpeed += ReturnAcceleration * dt;
            Vector2 difference = ThrowerPositon() - Position;
            Velocity = (float)ReturnSpeed * difference / difference.Length();
        }
    }

    protected override void OnPhaseChange()
    {
        if (Phase == 1)
        {
            Acceleration = Vector2.Zero;
        }
    }

    public override void HandleCollision(ICollisionHandler other, CollisionInfo collisionInfo)
    {
        base.HandleCollision(other, collisionInfo);
        if (Phase == 1)
        {
            if (other == Thrower)
            {
                Kill();
            }
        }
    }

    /// <summary>
    /// Calculates the trajectory needed to go a certain distance over a certain period.
    /// </summary>
    /// <param name="distance">Furhtest distance to go.</param>
    /// <param name="time">Time it takes to return.</param>
    /// <returns>Necessary velocity and acceleration magnitudes.</returns>
    public static (float Velocity, float Acceleration) Trajectory(float distance, float time)
    {
        return (4f * distance / time, -8f * distance / (time * time));
    }
}
