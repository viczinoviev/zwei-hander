using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

public class Player : IPlayer
{
    private PlayerStateMachine _stateMachine;
    private PlayerHandler _handler;
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 100f;
    public PlayerInput CurrentInput { get; set; } = PlayerInput.None;
    public PlayerState CurrentState => _stateMachine.CurrentState;

    public Player(PlayerSprites playerSprites)
    {
        _stateMachine = new PlayerStateMachine(this);
        _handler = new PlayerHandler(playerSprites, this, _stateMachine);
        Position = Vector2.Zero;
    }

    public void Update(GameTime gameTime)
    {
        _stateMachine.Update(gameTime);
        _handler.Update(gameTime);
    }

    public void MoveUp()
    {
        CurrentInput = PlayerInput.MovingUp;
    }

    public void MoveDown()
    {
        CurrentInput = PlayerInput.MovingDown;
    }

    public void MoveLeft()
    {
        CurrentInput = PlayerInput.MovingLeft;
    }

    public void MoveRight()
    {
        CurrentInput = PlayerInput.MovingRight;
    }

    public void Attack()
    {
        CurrentInput = PlayerInput.Attacking;
    }

    public void Idle()
    {
        CurrentInput = PlayerInput.None;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _handler.Draw(spriteBatch);
    }
}

public enum PlayerState
{
    Idle,
    MovingUp,
    MovingDown,
    MovingLeft,
    MovingRight,
    Attacking
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum PlayerInput
{
    None,
    MovingUp,
    MovingDown,
    MovingLeft,
    MovingRight,
    Attacking
}