using Microsoft.Xna.Framework;
using System;
using ZweiHander.Enemy;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.FriendlyNPC
{
    public class Kirby : IKirby
    {
        private readonly IPlayer _player;
        private readonly ISprite _kirbySprite;

        private readonly float _followDistance = 30f;
        private readonly float _speed = 80f;

        public Vector2 Position { get; set; }

        public Kirby(IPlayer player, KirbySprites kirbySprites, Vector2 startPosition)
        {
            _player = player;
            _kirbySprite = kirbySprites.Kirby();
            Position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 differenceVector = _player.Position - Position;
            float distanceFromPlayer = differenceVector.Length();
            float travelDistance = dt * _speed;

            differenceVector.Normalize();

            if (_followDistance < distanceFromPlayer)
            {
                Position += differenceVector*travelDistance;
            }

            _kirbySprite.Update(gameTime);
        }

        public void Draw()
        {
            _kirbySprite.Draw(Position);
        }
    }
}
