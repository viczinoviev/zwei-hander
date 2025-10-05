using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.Items;

public class Player : IPlayer
{
    private PlayerStateMachine _stateMachine;
    private PlayerHandler _handler;
    private ItemManager _itemManager;
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 100f;
    public HashSet<PlayerInput> InputBuffer { get; private set; } = new HashSet<PlayerInput>();
    public PlayerState CurrentState => _stateMachine.CurrentState;
    public ItemManager ItemManager => _itemManager;

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

    public void Idle()
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _handler.Draw(spriteBatch);
        _itemManager.Draw();
    }
}

public enum PlayerState
{
    Idle,
    Moving,
    Attacking,
    UsingItem
}

public enum PlayerInput
{
    None,
    MovingUp,
    MovingDown,
    MovingLeft,
    MovingRight,
    Attacking,
    UsingItem1,
    UsingItem2
}