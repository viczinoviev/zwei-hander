using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using ZweiHander.Graphics;

namespace ZweiHander.Map
{
    public class Border
    {
        private WallName _name;
        WallType _wallType;
        private Vector2 _position;
        private ISprite _sprite;
        private bool collision = true;
        public Border(WallName name, WallType type, Vector2 position, ISprite sprite)
        {
            _name = name;
            _wallType = type;
            _position = position;  
            _sprite = sprite;
        }
        public void Draw()
        {
            _sprite.Draw(_position);
        }
        public bool IsCollidable()
        {
            // Decorative blocks do not collide
            if (_wallType == WallType.Decorative) { collision = false; }
            return collision;
        }
    }
}
