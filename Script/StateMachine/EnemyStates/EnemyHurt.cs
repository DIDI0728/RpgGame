using Godot;
using System;

public partial class EnemyHurt : State
{


	public override void Enter()
	{
		base.Enter();
		character.UpdateAnimatation();
	}


	public override void Update()
	{
		base.Update();
		if (!character.animatedSprite2D.IsPlaying())
		{
			parentStateMachine.SwitchTo("Idle");
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
