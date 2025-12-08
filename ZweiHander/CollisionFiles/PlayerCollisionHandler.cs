using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Environment;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;
using ZweiHander.Commands;

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

        private readonly SoundEffect PlayerHurt;
        private readonly SoundEffect ItemPickup;

        private readonly SoundEffectInstance currentSFX;
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
                HandleBlockCollision(collisionInfo);
            }

            // If the player hit a damaging item, apply damage
            if (other is ItemCollisionHandler itemHandler)
            {
                HandleItemCollision(itemHandler);
            }

            if (other is EnemyCollisionHandler)
            {
                HandleEnemyCollision();
            }
        }

        private void HandleBlockCollision(CollisionInfo collisionInfo)
        {
            Vector2 newPosition = _player.Position + collisionInfo.ResolutionOffset;
            _player.SetPositionFromCollision(newPosition);
            UpdateCollisionBox();
        }

        private void HandleItemCollision(ItemCollisionHandler itemHandler)
        {
            if (itemHandler.Item.HasProperty(ItemProperty.CanDamagePlayer))
            {
                HandlePlayerDamage();
            }

            if (itemHandler.Item.HasProperty(ItemProperty.CanBePickedUp))
            {
                HandleItemPickup(itemHandler);
            }
        }

        private void HandlePlayerDamage()
        {
            _player.TakeDamage();
            if (currentSFX.State == SoundState.Stopped)
            {
                currentSFX.Play();
            }
        }

        private void HandleItemPickup(ItemCollisionHandler itemHandler)
        {
            ItemPickup.Play();
            ApplyItemEffect(itemHandler.Item);
            itemHandler.Item.Kill();
        }

        private void ApplyItemEffect(IItem item)
        {
            switch (item)
            {
                case HeartContainer:
                    _player.IncreaseMaxHealth(2);
                    break;
                case Heart:
                    _player.Heal(2);
                    break;
                case Fairy:
                    _player.Heal(9999);
                    break;
                case Bomb:
                    _player.AddItemToInventory(item.ItemType, 10);
                    break;
                case MapItem:
                    ICommand makeMinimapVisible = new MapItemGottenCommand(_player.GameInstance);
                    makeMinimapVisible.Execute();
                    break;
                case Compass:
                    ICommand makeCompassVisible = new CompassItemGottenCommand(_player.GameInstance);
                    makeCompassVisible.Execute();
                    break;
                default:
                    _player.AddItemToInventory(item.ItemType);
                    break;
            }
        }

        private void HandleEnemyCollision()
        {
            HandlePlayerDamage();
        }

        public override void UpdateCollisionBox()
        {

            
            CollisionBox = new (
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
                float xOffset = CalculateAxisOffset(intendedMovement.X, 0, safeMovement.X);
                if (Math.Abs(xOffset) > 0.001f)
                    safeMovement.X += xOffset;
            }

            // Try moving on Y axis (with the X movement already applied)
            if (Math.Abs(intendedMovement.Y) > 0.0001f)
            {
                float yOffset = CalculateAxisOffset(safeMovement.X, intendedMovement.Y, safeMovement.X);
                if (Math.Abs(yOffset) > 0.001f)
                    safeMovement.Y += yOffset;
            }

            return safeMovement;
        }

        private float CalculateAxisOffset(float xMovement, float yMovement, float currentXMovement)
        {
            Vector2 testPosition = _player.Position + new Vector2(xMovement, yMovement);
            Rectangle testBox = CreateCollisionBoxAt(testPosition);
            var collisions = CollisionManager.Instance.CheckCollisionsForOne(testBox);
            return GetMaxResolutionOffset(collisions, xMovement, yMovement);
        }

        private Rectangle CreateCollisionBoxAt(Vector2 position)
        {
            return new Rectangle(
                (int)(position.X - COLLISION_SIZE / 2),
                (int)(position.Y - COLLISION_SIZE / 2),
                COLLISION_SIZE,
                COLLISION_SIZE
            );
        }

        private float GetMaxResolutionOffset(System.Collections.Generic.List<(ICollisionHandler, CollisionInfo)> collisions, float xMovement, float yMovement)
        {
            float maxOffset = 0f;
            bool isXAxis = Math.Abs(yMovement) < 0.0001f;

            foreach (var (handler, info) in collisions)
            {
                if (handler is BlockCollisionHandler)
                {
                    float offset = isXAxis ? info.ResolutionOffset.X : info.ResolutionOffset.Y;
                    if (Math.Abs(offset) > Math.Abs(maxOffset))
                        maxOffset = offset;
                }
            }

            return maxOffset;
        }
    }
}