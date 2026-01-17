using Godot;
using System;

public partial class EnemyDead : State
{


	public override void Enter()
	{
		base.Enter();
		character.UpdateAnimatation();
		if(character.animatedSprite2D.IsPlaying()==false)
		{
			character.QueueFree();
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
