using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;

namespace ZweiHander.PlayerFiles
{
    public class Player : IPlayer
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerHandler _handler;
        private readonly ItemManager _itemManager;
        private Vector2 _position;

        public Vector2 Position 
        { 
            get => _position; 
            set 
            { 
                _position = value;
                _handler?.UpdateCollisionBox();
            } 
        }
        
        public float Speed { get; set; } = 100f;
        public HashSet<PlayerInput> InputBuffer { get; private set; } = [];
        public PlayerState CurrentState => _stateMachine.CurrentState;
        public ItemManager ItemManager => _itemManager;

        public Color Color
        {
            get => _handler.Color;
            set => _handler.Color = value;
        }
        public Player(PlayerSprites playerSprites, ItemSprites itemSprites, TreasureSprites treasureSprites)
        {
            _itemManager = new ItemManager(itemSprites, treasureSprites);
            _stateMachine = new PlayerStateMachine(this);
            _handler = new PlayerHandler(playerSprites, this, _stateMachine);
            _stateMachine.SetPlayerHandler(_handler);
            Position = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            _stateMachine.Update(gameTime);
            _handler.Update(gameTime);
            _itemManager.Update(gameTime);
        }

        public void AddInput(PlayerInput input)
        {
            InputBuffer.Add(input);
        }

        public void ClearInputBuffer()
        {
            InputBuffer.Clear();
        }


        public void SetPositionFromCollision(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public void MoveUp()
        {
            AddInput(PlayerInput.MovingUp);
        }

        public void MoveDown()
        {
            AddInput(PlayerInput.MovingDown);
        }

        public void MoveLeft()
        {
            AddInput(PlayerInput.MovingLeft);
        }

        public void MoveRight()
        {
            AddInput(PlayerInput.MovingRight);
        }

        public void Attack()
        {
            AddInput(PlayerInput.Attacking);
        }

        public void UseItem1()
        {
            AddInput(PlayerInput.UsingItem1);
        }

        public void UseItem2()
        {
            AddInput(PlayerInput.UsingItem2);
        }

        public void UseItem3()
        {
            AddInput(PlayerInput.UsingItem3);
        }

        public void Idle()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _handler.Draw(spriteBatch);
            _itemManager.Draw();
        }


    }
}