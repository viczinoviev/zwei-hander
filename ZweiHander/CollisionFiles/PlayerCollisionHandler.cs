using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

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

        private SoundEffect PlayerHurt;
        private SoundEffect ItemPickup;

        private SoundEffectInstance currentSFX;

        /// <summary>
        /// How big the player's collision box is (for now a rectangle 24 by 24 px)
        /// </summary>
        private const int COLLISION_SIZE = 24;

        public PlayerCollisionHandler(Player player,ContentManager sfxPlayer)
        {
            _player = player;
            PlayerHurt = sfxPlayer.Load<SoundEffect>("Audio/PlayerHurt");
            ItemPickup = sfxPlayer.Load<SoundEffect>("Audio/ItemPickup");
            currentSFX = PlayerHurt.CreateInstance();
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
                if (itemHandler.Item.HasProperty(ItemProperty.CanDamagePlayer))
                {
                    _player.TakeDamage();
                    if (currentSFX.State == SoundState.Stopped)
                    {
                        currentSFX.Play();
                    }
                }

                if (itemHandler.Item.HasProperty(ItemProperty.CanBePickedUp))
                {
                    ItemPickup.Play();
                    switch (itemHandler.Item)
                    {
                        case HeartContainer:
                            _player.IncreaseMaxHealth(2);
                            break;
                        case Heart:
                            _player.Heal(2);
                            break;
                        case Bomb:
                            _player.AddItemToInventory(itemHandler.Item.ItemType, 10);
                            break;
                        default:
                            _player.AddItemToInventory(itemHandler.Item.ItemType);
                            break;
                    }                   
                    itemHandler.Item.Kill();
                }
            }

            if (other is EnemyCollisionHandler enemy)
            {
                _player.TakeDamage();
                if (currentSFX.State == SoundState.Stopped)
                    {
                        currentSFX.Play();
                    }
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