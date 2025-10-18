using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;

namespace ZweiHander.CollisionFiles
{
    public class PlayerCollisionHandler : CollisionHandlerAbstract
    {
        private readonly Player _player;
        private const int COLLISION_SIZE = 16; // 16x16px collision square

        public PlayerCollisionHandler(Player player)
        {
            _player = player;
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            // Check if the collider is an IBlock
            if (other is IBlock)
            {
                System.Console.WriteLine($"Collision offset: {collisionInfo.ResolutionOffset}, magnitude: {collisionInfo.ResolutionOffset.Length()}");
                
                // Apply the collision resolution offset
                Vector2 newPosition = _player.Position + collisionInfo.ResolutionOffset;
                _player.SetPositionFromCollision(newPosition);
                
                UpdateCollisionBox();
            }
        }

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