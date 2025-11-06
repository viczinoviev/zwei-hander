using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;

namespace ZweiHander.CollisionFiles
{
    public class BlockCollisionHandler : CollisionHandlerAbstract
    {
        // The actual block this handler is taking care of
        private readonly Block _block;

        public BlockCollisionHandler(Block block)
        {
            _block = block;
            UpdateCollisionBox();
        }

        // What happens when something hits this block
        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            // Nuthin for now
        }

        // Updates where this block can be hit
        public override void UpdateCollisionBox()
        {
            collisionBox = _block.GetBlockHitbox();
        }
    }
}