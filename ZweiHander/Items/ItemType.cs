namespace ZweiHander.Items;
/// <summary>
/// All items that exist are within this.
/// </summary>
public enum ItemType
{
    /// <summary>
    /// Infinite, Can be picked up, stationary, delete on collision
    /// </summary>
    Compass,
    /// <summary>
    /// Infinite, Can be picked up, stationary, delete on collision
    /// </summary>
    Map,
    /// <summary>
    /// Infinite, Can be picked up, stationary, delete on collision
    /// </summary>
    Key,
    /// <summary>
    /// Infinite, Can be picked up, stationary, delete on collision
    /// </summary>
    Heart,
    /// <summary>
    /// 3s life, animation; use ItemHelper.BoomerangTrajectory
    /// </summary>
    Boomerang,
    /// <summary>
    /// 2s life, death sprite
    /// </summary>
    Arrow
}