using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using ZweiHander.CollisionFiles;
using ZweiHander.Items;
using ZweiHander.Items.ItemStorages;

namespace ZweiHander.PlayerFiles
{
    public class PlayerHandleItemUse(Player player, PlayerStateMachine stateMachine, PlayerCollisionHandler collisionHandler, ContentManager content)
    {
        private readonly List<SoundEffect> _sounds = [
                content.Load<SoundEffect>("Audio/SwordAttack"),
                content.Load<SoundEffect>("Audio/Fireball")
            ];

        public float HandleItemUse(UsableItem itemInput)
        {
            float actionDuration = 0f;
            Vector2 itemPosition = player.Position;
            Vector2 itemVelocity = stateMachine.LastDirection * 300f;
            if (itemInput == UsableItem.Bow && player.InventoryCount(typeof(Bow)) > 0)
            {
                actionDuration = 1000f;
                _sounds[0].Play();
                player.ItemManager.GetItem(
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
                player.ItemManager.GetItem(
                    "Boomerang",
                    life: -1f,
                    position: itemPosition,
                    velocity: itemVelocity,
                    acceleration: -itemVelocity * 0.9f,
                    properties: [ItemProperty.DeleteOnBlock,
                         ItemProperty.CanDamageEnemy],
                    extras: [() => player.Position, collisionHandler]
                );
            }
            else if (itemInput == UsableItem.Bomb)
            {
                actionDuration = 300f;
                _sounds[0].Play();
                if (player.InventoryCount(typeof(Bomb)) > 0)
                {
                    player.ItemManager.GetItem(
                        "Bomb",
                        life: 3.3f,
                        position: itemPosition + (stateMachine.LastDirection * 30f),
                        velocity: Vector2.Zero,
                        acceleration: Vector2.Zero
                    );
                    player.Inventory[typeof(Bomb)]--;
                }

            }
            else if (itemInput == UsableItem.RedCandle && player.InventoryCount(typeof(RedCandle)) > 0)
            {
                actionDuration = 1000f;
                _sounds[1].Play();
                player.ItemManager.GetItem(
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
