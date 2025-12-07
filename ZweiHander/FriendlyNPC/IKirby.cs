using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.FriendlyNPC
{
    public interface IKirby
    {
        Vector2 Position { get; set; }
        void Update(GameTime gameTime);
        void Draw();

    }
}
