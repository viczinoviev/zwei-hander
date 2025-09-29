namespace ZweiHander.Items;

/// <summary>
/// Contains methods that are helpful for items.
/// </summary>
class ItemHelper
{
    /// <summary>
    /// Calculates the trajectory needed to go a certain distance over a certain period.
    /// </summary>
    /// <param name="distance">Furhtest distance to go.</param>
    /// <param name="time">Time it takes to return.</param>
    /// <returns>Necessary velocity and acceleration magnitudes.</returns>
    public static (float Velocity, float Acceleration) BoomerangTrajectory(float distance, float time)
    {
        return (4f * distance / time, 8f * distance / (time * time));
    }
}