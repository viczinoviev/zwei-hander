using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using ZweiHander.Enemy;
using ZweiHander.Enemy.EnemyStorage;
using ZweiHander.Items;
using ZweiHander.PlayerFiles;

namespace ZweiHander.CollisionFiles
{
    public class EnemyCollisionHandler : CollisionHandlerAbstract
    {
        private const int Damage = 5;
        private const int hitCoolDown = 50;
        /// <summary>
        /// The enemy this handler manages
        /// </summary>
        public readonly IEnemy _enemy;

        private readonly SoundEffect enemyHurt;



        private readonly SoundEffectInstance currentSFX;
        public EnemyCollisionHandler(IEnemy enemy, ContentManager sfxPlayer)
        {
            _enemy = enemy;
            enemyHurt = sfxPlayer.Load<SoundEffect>("Audio/EnemyHurt");
            currentSFX = enemyHurt.CreateInstance();
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            //Block collision
            if (other is BlockCollisionHandler blockCollisionHandler)
            {
                HandleBlockCollision(blockCollisionHandler,collisionInfo);
            }
            //Item collision
            if (other is ItemCollisionHandler itemCollisionHandler)
            {
                HandleItemCollision(itemCollisionHandler);
            }
            //Player collision
            if (other is PlayerCollisionHandler playerCollisionHandler)
            {
                HandlePlayerCollision(playerCollisionHandler);
            }
            //Enemy collision
            if (other is EnemyCollisionHandler)
            {
                HandleEnemyCollision(collisionInfo);
            }
        }

        public override void UpdateCollisionBox()
        {
            //Get the collision box for the specific enemy
            CollisionBox = _enemy.GetCollisionBox();
        }

        private void HandleBlockCollision(BlockCollisionHandler blockCollisionHandler, CollisionInfo collisionInfo)
        {
            //If enemy is running into a block, prevent enemy from going into the block
                if (_enemy is not Wallmaster)
                {
                    Vector2 newPosition = _enemy.Position + collisionInfo.ResolutionOffset;
                    _enemy.Position = newPosition;
                    _enemy.Face = (int)collisionInfo.Normal;

                    UpdateCollisionBox();
                }
                else
                {
                    if (blockCollisionHandler._customCollisionBox != null)
                    {
                        Vector2 newPosition = _enemy.Position + collisionInfo.ResolutionOffset;
                        _enemy.Position = newPosition;
                        _enemy.Face = (int)collisionInfo.Normal;

                        UpdateCollisionBox();
                    }
                }
        }
        private void HandleItemCollision(ItemCollisionHandler itemCollisionHandler)
        {
            //If the item can hurt enemies, hurt the enemy.
                if (itemCollisionHandler.Item.HasProperty(ItemProperty.CanDamageEnemy))
                {
                    if(!(_enemy.HitcoolDown > 0)){
                        _enemy.HitcoolDown = hitCoolDown;
                        enemyHurt.Play();
                    _enemy.TakeDamage(Damage);
                }
                }
        }
        private void HandlePlayerCollision(PlayerCollisionHandler playerCollisionHandler)
        {
            //If the player is attacking, hurt the enemy
                if (playerCollisionHandler._player.CurrentState == PlayerState.Attacking)
                {
                    if(!(_enemy.HitcoolDown > 0)){
                        _enemy.HitcoolDown = hitCoolDown;

                        if (currentSFX.State == SoundState.Stopped)
                        {
                            currentSFX.Play();
                        }
                    //if the enemy has died, set this handler to be removed
                    _enemy.TakeDamage(Damage);
                    }
                }
        }
        private void HandleEnemyCollision(CollisionInfo collisionInfo)
        {
            //If enemy is running into another enemy, prevent enemy from going into the enemy, unless this is a bladetrap(unmoving)
                if (_enemy is not BladeTrap)
                {
                    Vector2 newPosition = _enemy.Position + collisionInfo.ResolutionOffset;
                    _enemy.Position = newPosition;

                    UpdateCollisionBox();
                }
        }
    }
}