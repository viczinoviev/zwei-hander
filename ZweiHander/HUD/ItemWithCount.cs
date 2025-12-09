using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;
using ZweiHander.PlayerFiles;

namespace ZweiHander.HUD
{
    public class ItemWithCount : IHUDComponent
    {
        private readonly ISprite _itemSprite;
        private readonly Vector2 _position;
        private readonly IPlayer _player;
        private readonly Type _itemType;
        private readonly NumberSprite _numberSprite;
        private readonly ISprite _x;

        public ItemWithCount(HUDSprites _hudSprites, ISprite itemSprite, Vector2 position, IPlayer player, Type itemType)
        {
            _itemSprite = itemSprite;
            _position = position;
            _player = player;
            _itemType = itemType;
            _numberSprite = new NumberSprite(0, _hudSprites, 2);
            _x = _hudSprites.XSymbol();
        }

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

