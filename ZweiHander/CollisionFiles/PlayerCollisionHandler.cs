using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;

namespace ZweiHander.CollisionFiles
{
    // Handles collisions for the player
    public class PlayerCollisionHandler : CollisionHandlerAbstract
    {
        // The player this handler is watching over
        private readonly Player _player;
        // How big the player's collision box is (for now a rectangle 24 by 24 px)
        private const int COLLISION_SIZE = 24;

        public PlayerCollisionHandler(Player player)
        {
            _player = player;
            UpdateCollisionBox();
        }

        // What happens when the player bumps into something
        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            // If the player hit a block, stop them from going through it
            if (other is BlockCollisionHandler)
            {
                                
                Vector2 newPosition = _player.Position + collisionInfo.ResolutionOffset;
                _player.SetPositionFromCollision(newPosition);
                
                UpdateCollisionBox();
            }
        }

        // Updates where the player can be hit based on where they are
        public override void UpdateCollisionBox()
        {

            
            collisionBox = new Rectangle(
                (int)(_player.Position.X - COLLISION_SIZE / 2),
                (int)(_player.Position.Y - COLLISION_SIZE / 2),
                COLLISION_SIZE,
                COLLISION_SIZE
            );
        }
    }
}