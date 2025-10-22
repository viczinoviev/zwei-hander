using System;

namespace ZweiHander.Items;
/// <summary>
/// All boolean Item Properties.
/// </summary>
[Flags]
public enum ItemProperty : Int16
{
    /// <summary>
    /// Player can pick this item up
    /// </summary>
    CanBePickedUp = 0x0,
    /// <summary>
    /// On collision with anything, this is deleted
    /// </summary>
    DeleteOnCollision = 0x1,
    /// <summary>
    /// Damages the player on collision with it
    /// </summary>
    CanDamagePlayer = 0x2,
    /// <summary>
    /// Damages an enemy on collision with it
    /// </summary>
    CanDamageEnemy = 0x4,
    /// <summary>
    /// Does not move
    /// </summary>
    Stationary = 0x8,
    /// <summary>
    /// On collision with player, this is deleted
    /// </summary>
    DeleteOnPlayer = 0x10,
    /// <summary>
    /// On collision with enemy, this is deleted
    /// </summary>
    DeleteOnEnemy = 0x20,
    /// <summary>
    /// Rotated in same direction as velocity; UNTESTED
    /// </summary>
    FacingVelocity = 0x40,
    /// <summary>
    /// On collision with block, this is deleted
    /// </summary>
    DeleteOnBlock = 0x80,
    /// <summary>
    /// On collision with block, its velocity is reversed
    /// </summary>
    BounceOnBlock = 0x100,
    /// <summary>
    /// On collision with block, acceleration=velocity=(0,0)
    /// </summary>
    StopOnBlock = 0x200,
    /// <summary>
    /// Item meant to be collected by player
    /// </summary>
    Collectable = Stationary | DeleteOnPlayer | CanBePickedUp
}