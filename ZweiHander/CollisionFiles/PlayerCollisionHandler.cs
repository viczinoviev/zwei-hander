using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;

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
        private readonly Random _random;

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
            _random = new Random();
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
                        case Fairy:
                            int healthChange = _random.Next(-2, 3);
                            if (healthChange > 0) _player.Heal(healthChange);
                            if (healthChange < 0) _player.TakeDamage(-healthChange);
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

        /// <summary>
        /// Given an intended movement vector, calculates a safe movement vector that preactively avoids clipping into blocks.
        /// </summary>
        public Vector2 CalculateSafeMovement(Vector2 intendedMovement)
        {
            if (intendedMovement.LengthSquared() < 0.0001f)
                return Vector2.Zero;

            Vector2 safeMovement = intendedMovement;

            // Try moving on X axis first
            if (Math.Abs(intendedMovement.X) > 0.0001f)
            {
                Vector2 xOnlyPosition = _player.Position + new Vector2(intendedMovement.X, 0);
                Rectangle xTestBox = new Rectangle(
                    (int)(xOnlyPosition.X - COLLISION_SIZE / 2),
                    (int)(xOnlyPosition.Y - COLLISION_SIZE / 2),
                    COLLISION_SIZE,
                    COLLISION_SIZE
                );

                var xCollisions = CollisionManager.Instance.CheckCollisionsForOne(this, xTestBox);
                
                float maxXOffset = 0f;
                foreach (var (handler, info) in xCollisions)
                {
                    if (handler is BlockCollisionHandler)
                    {
                        if (Math.Abs(info.ResolutionOffset.X) > Math.Abs(maxXOffset))
                            maxXOffset = info.ResolutionOffset.X;
                    }
                }

                if (Math.Abs(maxXOffset) > 0.001f)
                    safeMovement.X += maxXOffset;
            }

            // Try moving on Y axis (with the X movement already applied)
            if (Math.Abs(intendedMovement.Y) > 0.0001f)
            {
                Vector2 xyPosition = _player.Position + new Vector2(safeMovement.X, intendedMovement.Y);
                Rectangle yTestBox = new Rectangle(
                    (int)(xyPosition.X - COLLISION_SIZE / 2),
                    (int)(xyPosition.Y - COLLISION_SIZE / 2),
                    COLLISION_SIZE,
                    COLLISION_SIZE
                );

                var yCollisions = CollisionManager.Instance.CheckCollisionsForOne(this, yTestBox);
                
                float maxYOffset = 0f;
                foreach (var (handler, info) in yCollisions)
                {
                    if (handler is BlockCollisionHandler)
                    {
                        if (Math.Abs(info.ResolutionOffset.Y) > Math.Abs(maxYOffset))
                            maxYOffset = info.ResolutionOffset.Y;
                    }
                }

                if (Math.Abs(maxYOffset) > 0.001f)
                    safeMovement.Y += maxYOffset;
            }

            return safeMovement;
        }
    }
}