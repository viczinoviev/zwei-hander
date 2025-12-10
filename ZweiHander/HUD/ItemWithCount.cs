using Microsoft.Xna.Framework;
using System;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;

namespace ZweiHander.HUD
{
    public class ItemWithCount(
        HUDSprites _hudSprites,
        ISprite itemSprite,
        Vector2 position,
        IPlayer player,
        Type itemType
    ) : IHUDComponent
    {
        private readonly ISprite _itemSprite = itemSprite;
        private readonly Vector2 _position = position;
        private readonly IPlayer _player = player;
        private readonly Type _itemType = itemType;
        private readonly NumberSprite _numberSprite = new(0, _hudSprites, 2);
        private readonly ISprite _x = _hudSprites.XSymbol();

        public void Draw(Vector2 offset)
        {
            _itemSprite.Draw(_position + offset);
            _x.Draw(_position + new Vector2(_itemSprite.Width, 0) + offset);
            _numberSprite.Draw(_position + new Vector2(_itemSprite.Width + _x.Width + 8, 0) + offset);
        }

        public void Update(GameTime gameTime)
        {
            _numberSprite.SetNumber(_player.InventoryCount(_itemType));
        }
    }
}

