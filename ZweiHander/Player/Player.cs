using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZweiHander.Graphics;
using ZweiHander.Graphics.SpriteStorages;

public class Player : IPlayer
{
    public PlayerStateMachine _stateMachine;
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 100f;

    public Player(PlayerSprites playerSprites)
    {
        _stateMachine = new PlayerStateMachine(playerSprites, this);
        Position = Vector2.Zero;
    }

    public void Update(GameTime gameTime)
    {
        _stateMachine.Update(gameTime);
    }

    public void MoveUp()
    {
        _stateMachine.SetState(PlayerState.MovingUp);
    }

    public void MoveDown()
    {
        _stateMachine.SetState(PlayerState.MovingDown);
    }

    public void MoveLeft()
    {
        _stateMachine.SetState(PlayerState.MovingLeft);
    }

    public void MoveRight()
    {
        _stateMachine.SetState(PlayerState.MovingRight);
    }

    public void Attack()
    {
        _stateMachine.SetState(PlayerState.Attacking);
    }

    public void Idle()
    {
        _stateMachine.SetState(PlayerState.Idle);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _stateMachine.Draw(spriteBatch);
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