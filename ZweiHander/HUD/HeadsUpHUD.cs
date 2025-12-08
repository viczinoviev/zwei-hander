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
        private readonly NumberSprite _bombs;
        private readonly NumberSprite _keys;
        private readonly Vector2 _position;
        private readonly IPlayer _player;

        public HeadsUpHUD(HUDSprites hudSprites, Vector2 position, IPlayer player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _headsUpDisplayHUD = hudSprites.HeadsUpHUD();
            _position = position; // Position is determined by HUDManager
            _player = player;
            _bombs = (NumberSprite)hudSprites.Number(0, 3);
            _keys = (NumberSprite)hudSprites.Number(0, 3);
        }

        public void Update(GameTime gameTime)
        {
            _bombs.SetNumber(_player.InventoryCount(typeof(Bomb)));
            _keys.SetNumber(_player.InventoryCount(typeof(Key)));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            _headsUpDisplayHUD.Draw(_position + offset);
            _bombs.Draw(_position + new Vector2(-22, 35) + offset);
            _keys.Draw(_position + new Vector2(-22, 18) + offset);
        }
    }
}
