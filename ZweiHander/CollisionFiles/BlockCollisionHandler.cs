using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;

namespace ZweiHander.CollisionFiles
{
    public class BlockCollisionHandler : CollisionHandlerAbstract
    {
        // The actual block this handler is taking care of
        private readonly Block _block;
        
        // Custom collision box for borders or other objects that don't use block-based collision
        private readonly Rectangle? _customCollisionBox;

        // Standard constructor for blocks
        public BlockCollisionHandler(Block block)
        {
            _block = block;
            _customCollisionBox = null;
            UpdateCollisionBox();
        }

        // Overloaded constructor for custom collision boxes (used by borders)
        public BlockCollisionHandler(Rectangle customCollisionBox)
        {
            _block = null;
            _customCollisionBox = customCollisionBox;
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
            if (_customCollisionBox.HasValue)
            {
                collisionBox = _customCollisionBox.Value;
            }
            else if (_block != null)
            {
                collisionBox = _block.GetBlockHitbox();
            }
        }
    }
}