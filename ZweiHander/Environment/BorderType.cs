namespace ZweiHander.Environment;

/// <summary>
/// Enum for border types
/// </summary>
public enum BorderType
{
    Solid,              // Full 32x32 collision
    EntranceLeft,       // 32x32 with 24x24 cut-out on left side
    EntranceRight,      // 32x32 with 24x24 cut-out on right side
    EntranceUp,         // 32x32 with 24x24 cut-out on top side
    EntranceDown,       // 32x32 with 24x24 cut-out on bottom side
    Decorative          // No collision
}
