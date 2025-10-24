using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;

namespace ZweiHander.CollisionFiles
{
    /// <summary>
    /// Handles collisions for the player
    /// </summary>
    public class PlayerCollisionHandler : CollisionHandlerAbstract
    {
        /// <summary>
        /// The player this handler is watching over
        /// </summary>
        public readonly Player _player;

        /// <summary>
        /// How big the player's collision box is (for now a rectangle 24 by 24 px)
        /// </summary>
        private const int COLLISION_SIZE = 24;

        public PlayerCollisionHandler(Player player)
        {
            _player = player;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            // If the player hit a block, stop them from going through it
            if (other is BlockCollisionHandler)
            {

                Vector2 newPosition = _player.Position + collisionInfo.ResolutionOffset;
                _player.SetPositionFromCollision(newPosition);

                UpdateCollisionBox();
            }

            // If the player hit a damaging item, apply damage
            if (other is ItemCollisionHandler itemHandler)
            {
                if (itemHandler.HasProperty(ItemProperty.CanDamagePlayer))
                {
                    _player.TakeDamage();
                }
            }
        }

        public override void UpdateCollisionBox()
        {

            
            CollisionBox = new Rectangle(
                (int)(_player.Position.X - COLLISION_SIZE / 2),
                (int)(_player.Position.Y - COLLISION_SIZE / 2),
                COLLISION_SIZE,
                COLLISION_SIZE
            );
        }
    }
}