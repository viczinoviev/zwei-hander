namespace ZweiHander.Environment;
/// <summary>
/// Enum to define different types of blocks
/// </summary>
public enum BlockType
{
    Solid,      // Blocks that are solid and cannot be moved
    Pushable,   // Blocks that can be pushed by the player
    Breakable,  // Blocks that can be broken/destroyed
    Decorative  // Non-collidable decorative blocks
}