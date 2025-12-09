using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items.ItemStorages;
using ZweiHander.PlayerFiles;

namespace ZweiHander.HUD
{
    /// <summary>
    /// Displays the player's health as heart containers
    /// </summary>
    public class HeadsUpHUD : IHUDComponent
    {
        private readonly ISprite _headsUpDisplayHUD;
        private readonly ItemWithCount _bombs;
        private readonly NumberSprite _keys;
        private readonly NumberSprite _rupies;
        private readonly Vector2 _position;
        private readonly IPlayer _player;
        private readonly ItemWithCount _test;

        public HeadsUpHUD(HUDSprites hudSprites, Vector2 position, IPlayer player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _headsUpDisplayHUD = hudSprites.HeadsUpHUD();
            _position = position; // Position is determined by HUDManager
            _player = player;
            _keys = (NumberSprite)hudSprites.Number(0, 3);
            _rupies = (NumberSprite)hudSprites.Number(0, 3);
            _bombs = new ItemWithCount(hudSprites, hudSprites.Bomb(), position, _player, typeof(Bomb));
        }

        public void Update(GameTime gameTime)
        {
            _bombs.Update(gameTime);
            _keys.SetNumber(_player.InventoryCount(typeof(Key)));
            _rupies.SetNumber(_player.InventoryCount(typeof(Rupy)));
        }

        public void Draw(Vector2 offset)
        {
            _headsUpDisplayHUD.Draw(_position + offset);
            _bombs.Draw(_position + new Vector2(-472, -22) + offset);
            _keys.Draw(_position + new Vector2(-22, 18) + offset);
            _rupies.Draw(_position + new Vector2(-40, -16) + offset);
        }
    }
}
