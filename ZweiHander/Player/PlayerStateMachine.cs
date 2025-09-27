using System;
using Microsoft.Xna.Framework;

public class PlayerStateMachine : IStateMachine
{
	private IController controller;
	private IPlayer player;

	public PlayerStateMachine(IController controller, IPlayer player)
	{
		this.controller = controller;
		this.player = player;
	}

	public void Update(GameTime gameTime)
	{
		controller.Update();
		player.Update(gameTime);
	}
}