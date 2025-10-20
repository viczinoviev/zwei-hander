using Microsoft.Xna.Framework;

namespace ZweiHander.CollisionFiles
{
    /// <summary>
    /// Base class that handles the common collision handler logic.
    /// </summary>
    public abstract class CollisionHandlerAbstract : ICollisionHandler
    {
        public Rectangle CollisionBox { get; set; }

        protected CollisionHandlerAbstract()
        {
            // Subscribe to the singleton CollisionManager as soon as is created
            CollisionManager.Instance.AddCollider(this);
        }

        public abstract void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo);

        public abstract void UpdateCollisionBox();

        public virtual void Unsubscribe()
        {
            CollisionManager.Instance.RemoveCollider(this);
        }
    }
}