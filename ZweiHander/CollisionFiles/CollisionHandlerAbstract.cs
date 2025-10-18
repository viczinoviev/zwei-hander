using Microsoft.Xna.Framework;

namespace ZweiHander.CollisionFiles
{
    public abstract class CollisionHandlerAbstract : ICollisionHandler
    {
        public Rectangle collisionBox { get; set; }

        protected CollisionHandlerAbstract()
        {
            // Subscribe to the singleton CollisionManager as soon as is created
            CollisionManager.Instance.AddCollider(this);
        }

        public abstract void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo);

        public abstract void UpdateCollisionBox();

        // Cleanup method
        public virtual void Unsubscribe()
        {
            CollisionManager.Instance.RemoveCollider(this);
        }
    }
}