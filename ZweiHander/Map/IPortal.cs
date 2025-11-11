using Microsoft.Xna.Framework;

namespace ZweiHander.Map
{
    public interface IPortal
    {
        int PortalId { get; }
        Vector2 Position { get; }
        Rectangle TriggerArea { get; }
        Room ParentRoom { get; }
        Area ParentArea { get; }
    }
}
