using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;

namespace ZweiHander.CollisionFiles
{
    /// <summary>
    /// Handles collisions for blocks.
    /// </summary>
    public class BlockCollisionHandler : CollisionHandlerAbstract
    {
        /// <summary>
        /// The actual block this handler is taking care of
        /// </summary>
        private readonly Block _block;

        public BlockCollisionHandler(Block block)
        {
            _block = block;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            // Nuthin for now
        }

        public override void UpdateCollisionBox()
        {
            CollisionBox = _block.GetBlockHitbox();
        }
    }
}