using System.Diagnostics;
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
            //Item collision
            if (other is ItemCollisionHandler itemCollisionHandler)
            {
                //If the item can hurt enemies, hurt the enemy.
                if (itemCollisionHandler.Item.HasProperty(ItemProperty.CanDamageEnemy))
                {
                    if(!(_enemy.HitcoolDown > 0)){
                        _enemy.HitcoolDown = hitCoolDown;
                        enemyHurt.Play();
                        _enemy.Hitpoints -= Damage;

                        //if the enemy has died, set this handler to be removed
                        if (_enemy.Hitpoints <= 0)
                        {
                            Dead = true;
                        }
                    }
                }
            }
            //Player collision
            if (other is PlayerCollisionHandler playerCollisionHandler)
            {
                //If the player is attacking, hurt the enemy
                if (playerCollisionHandler._player.CurrentState == PlayerState.Attacking)
                {
                    if(!(_enemy.HitcoolDown > 0)){
                        _enemy.HitcoolDown = hitCoolDown;
                        _enemy.Hitpoints -= Damage;
                        if (currentSFX.State == SoundState.Stopped)
                        {
                            currentSFX.Play();
                        }
                        //if the enemy has died, set this handler to be removed
                        if (_enemy.Hitpoints <= 0)
                        {
                            Dead = true;
                        }
                    }
                }
            }
            //Enemy collision
            if (other is EnemyCollisionHandler)
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

        public override void UpdateCollisionBox()
        {
            //Get the collision box for the specific enemy
            CollisionBox = _enemy.GetCollisionBox();
        }
    }
}