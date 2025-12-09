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
        protected readonly Vector2 _position;
        protected readonly IPlayer _player;
        protected readonly Type _itemType;
        protected readonly NumberSprite _numberSprite;

        protected ItemWithCount(ISprite itemSprite, Vector2 position, IPlayer player, Type itemType)
        {
            _itemSprite = itemSprite;
            _position = position;
            _player = player;
            _itemType = itemType;
        }

        public void Draw(Vector2 offset)
        {
            _itemSprite.Draw(_position + offset);
        }

        public void Update(GameTime gameTime)
        {
            _numberSprite.SetNumber(_player.InventoryCount(_itemType));
        }
    }
}

