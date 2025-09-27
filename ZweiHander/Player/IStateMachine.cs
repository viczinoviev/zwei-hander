using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IStateMachine
{
    void Update(GameTime gameTime);
    void SetState(PlayerState state);
    void Draw(SpriteBatch spriteBatch);
}