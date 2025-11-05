using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using System;

namespace ZweiHander.HUD
{
    /// <summary>
    /// Displays the player's Item slot A
    /// </summary>
    public class ItemSlots : IHUDComponent
    {
        private readonly IPlayer _player;
        private readonly HUDSprites _hudSprites;
        private readonly Vector2 _position;
        private const float SlotSpacing = 64f; // Space between hearts, not gaps!
        
        private ISprite _itemSlotA;
        private ISprite _itemSlotB;
        
        public ItemSlots(IPlayer player, HUDSprites hudSprites, Vector2 position)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _position = position; // Position is determined by HUDManager
            _itemSlotA = hudSprites.ItemSlotA();
            _itemSlotB = hudSprites.ItemSlotB();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _itemSlotA.Draw(_position);
            _itemSlotB.Draw(_position + new Vector2(SlotSpacing, 0));
        }
    }
}
