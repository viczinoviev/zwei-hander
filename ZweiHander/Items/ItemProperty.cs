namespace ZweiHander.Items;
/// <summary>
/// All boolean Item Properties.
/// </summary>
public enum ItemProperty
{
    /// <summary>
    /// Player can pick this item up
    /// </summary>
    CanBePickedUp,
    /// <summary>
    /// On collision with anything, this is deleted
    /// </summary>
    DeleteOnCollision,
    /// <summary>
    /// Damages the player on collision with it
    /// </summary>
    CanDamagePlayer,
    /// <summary>
    /// Damages an enemy on collision with it
    /// </summary>
    CanDamageEnemy,
    /// <summary>
    /// Does not move
    /// </summary>
    Stationary,
    /// <summary>
    /// On collision with player, this is deleted
    /// </summary>
    DeleteOnPlayer,
    /// <summary>
    /// On collision with enemy, this is deleted
    /// </summary>
    DeleteOnEnemy,
    /// <summary>
    /// Rotated in same direction as velocity
    /// </summary>
    FacingVelocity
}