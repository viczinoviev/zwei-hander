using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;

namespace ZweiHander.CollisionFiles
{
    public class BlockCollisionHandler : CollisionHandlerAbstract
    {
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
            collisionBox = _block.GetBlockHitbox();
        }
    }
}