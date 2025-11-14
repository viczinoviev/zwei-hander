using Microsoft.Xna.Framework;
using ZweiHander.Enemy;
using ZweiHander.PlayerFiles;
using ZweiHander.Items;
using Microsoft.Xna.Framework.Audio;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using System;
using ZweiHander.Enemy.EnemyStorage;

namespace ZweiHander.CollisionFiles
{
    public class EnemyCollisionHandler : CollisionHandlerAbstract
    {
        /// <summary>
        /// The enemy this handler manages
        /// </summary>
        public readonly IEnemy _enemy;

        private SoundEffect enemyHurt;
        public EnemyCollisionHandler(IEnemy enemy,ContentManager sfxPlayer)
        {
            _enemy = enemy;
            enemyHurt = sfxPlayer.Load<SoundEffect>("Audio/Hurt");
            UpdateCollisionBox();
        }

        public override void OnCollision(ICollisionHandler other, CollisionInfo collisionInfo)
        {
            //Block collision
            if (other is BlockCollisionHandler)
            {
                //If enemy is running into a block, prevent enemy from going into the block
                Vector2 newPosition = _enemy.Position + collisionInfo.ResolutionOffset;
                _enemy.Position = newPosition;

                UpdateCollisionBox();
            }
            //Item collision
            if (other is ItemCollisionHandler itemCollisionHandler)
            {
                //If the item can hurt enemies, hurt the enemy.
                if (itemCollisionHandler.Item.HasProperty(ItemProperty.CanDamageEnemy))
                {
                    enemyHurt.Play();
                    _enemy.Hitpoints -= 5;
                    
                    //if the enemy has died, set this handler to be removed
                    if (_enemy.Hitpoints <= 0)
                    {
                        Dead = true;
                        if (_enemy is BladeTrap bladeTrap)
                        {
                            bladeTrap.home1CollisionHandler.Dead = true;
                            bladeTrap.home2CollisionHandler.Dead = true;
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
                    _enemy.Hitpoints -= 5;
                    enemyHurt.Play();
                    //if the enemy has died, set this handler to be removed
                    if (_enemy.Hitpoints <= 0)
                    {
                        Dead = true;
                    }
                }
            }
            //Enemy collision
            if (other is EnemyCollisionHandler)
            {
                //If enemy is running into another enemy, prevent enemy from going into the enemy
                Vector2 newPosition = _enemy.Position + collisionInfo.ResolutionOffset;
                _enemy.Position = newPosition;

                UpdateCollisionBox();
                
            }
        }

        public override void UpdateCollisionBox()
        {
            //Get the collision box for the specific enemy
            collisionBox = _enemy.GetCollisionBox();
        }
    }
}