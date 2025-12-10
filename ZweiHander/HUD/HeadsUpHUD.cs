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
        private readonly ItemWithCount _keys;
        private readonly ItemWithCount _blueKey;
        private readonly ItemWithCount _rupies;
        private readonly Vector2 _position;
        private readonly IPlayer _player;

        public HeadsUpHUD(HUDSprites hudSprites, Vector2 position, IPlayer player)
        {
            hudSprites = hudSprites ?? throw new ArgumentNullException(nameof(hudSprites));
            _headsUpDisplayHUD = hudSprites.HeadsUpHUD();
            _position = position; // Position is determined by HUDManager
            _player = player;
            _bombs = new ItemWithCount(hudSprites, hudSprites.Bomb(), position, _player, typeof(Bomb));
            _keys = new ItemWithCount(hudSprites, hudSprites.Key(), position, _player, typeof(Key));
            // Update typeof part
            _blueKey = new ItemWithCount(hudSprites, hudSprites.BlueKey(), position, _player, typeof(BlueKey));
            _rupies = new ItemWithCount(hudSprites, hudSprites.Rupy(), position, _player, typeof(Rupy));
        }

        public void Update(GameTime gameTime)
        {
            _bombs.Update(gameTime);
            _keys.Update(gameTime);
            _blueKey.Update(gameTime);
            _rupies.Update(gameTime);
        }

        public void Draw(Vector2 offset)
        {
            _headsUpDisplayHUD.Draw(_position + offset);
            _bombs.Draw(_position + new Vector2(-472, -22) + offset);
            _keys.Draw(_position + new Vector2(-750, -72) + offset);
            _blueKey.Draw(_position + new Vector2(-750, -42) + offset);
            _rupies.Draw(_position + new Vector2(-472, -72) + offset);
        }
    }
}
