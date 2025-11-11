using Microsoft.Xna.Framework;

namespace ZweiHander.CollisionFiles
{
    // Base class that handles the common collision handler logic
    public abstract class CollisionHandlerAbstract : ICollisionHandler
    {
        // Where this thing can be hit
        public Rectangle collisionBox { get; set; }
        
        // If this Handler is done and needs to be deleted
        public bool Dead { get; set; } = false;

        protected CollisionHandlerAbstract()
        {
            // Subscribe to the singleton CollisionManager as soon as is created
            CollisionManager.Instance.AddCollider(this);
        }

        // What happens when this thing hits something else (each type does it differently)
        public abstract void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo);

        // Updates where this thing can be hit
        public abstract void UpdateCollisionBox();

        // Call this to stop this thing from being part of collisions
        public virtual void Unsubscribe()
        {
            CollisionManager.Instance.RemoveCollider(this);
        }
    }
}