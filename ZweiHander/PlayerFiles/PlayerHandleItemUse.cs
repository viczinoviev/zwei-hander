using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;

namespace ZweiHander.PlayerFiles
{
    public class PlayerHandleItemUse
    {
        private readonly Player _player;
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerCollisionHandler _collisionHandler;
        private readonly List<SoundEffect> _sounds;

        public PlayerHandleItemUse(Player player, PlayerStateMachine stateMachine, PlayerCollisionHandler collisionHandler, ContentManager content)
        {
            _player = player;
            _stateMachine = stateMachine;
            _collisionHandler = collisionHandler;
            _sounds = [
                content.Load<SoundEffect>("Audio/SwordAttack"),
                content.Load<SoundEffect>("Audio/Fireball")
            ];
        }

        public float HandleItemUse(UsableItem itemInput)
        {
            float actionDuration = 0f;
            Vector2 itemPosition = _player.Position;
            Vector2 itemVelocity = _stateMachine.LastDirection * 300f;
            if (itemInput == UsableItem.Bow && _player.InventoryCount(typeof(Bow)) > 0)
            {
                actionDuration = 1000f;
                _sounds[0].Play();
                _player.ItemManager.GetItem(
                    "Arrow",
                    life: 1.1,
                    position: itemPosition,
                    velocity: itemVelocity,
                    properties: [ItemProperty.DeleteOnEnemy,
                    ItemProperty.DeleteOnBlock,
                    ItemProperty.CanDamageEnemy]
                );
            }
            else if (itemInput == UsableItem.Boomerang)
            {
                actionDuration = 800f;
                _sounds[0].Play();
                _player.ItemManager.GetItem(
                    "Boomerang",
                    life: -1f,
                    position: itemPosition,
                    velocity: itemVelocity,
                    acceleration: -itemVelocity * 0.9f,
                    properties: [ItemProperty.DeleteOnBlock,
                         ItemProperty.CanDamageEnemy],
                    extras: [() => _player.Position, _collisionHandler]
                );
            }
            else if (itemInput == UsableItem.Bomb)
            {
                actionDuration = 300f;
                _sounds[0].Play();
                if (_player.InventoryCount(typeof(Bomb)) > 0)
                {
                    _player.ItemManager.GetItem(
                        "Bomb",
                        life: 3.3f,
                        position: itemPosition + _stateMachine.LastDirection * 30f,
                        velocity: Vector2.Zero,
                        acceleration: Vector2.Zero
                    );
                    _player.Inventory[typeof(Bomb)]--;
                }

            }
            else if (itemInput == UsableItem.RedCandle && _player.InventoryCount(typeof(RedCandle)) > 0)
            {
                actionDuration = 1000f;
                _sounds[1].Play();
                _player.ItemManager.GetItem(
                    "Fire",
                    life: 6f,
                    position: itemPosition,
                    velocity: itemVelocity * 0.11f,
                    acceleration: -itemVelocity * 0.1f
                );
            }

            return actionDuration;

        }
    }
}
