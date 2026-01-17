using Godot;
using System;

public  abstract partial class State : Node
{

	[Export]public StateMachine parentStateMachine;

	[Export]public BaseCharacter character;

	public virtual void UpdatePhysics(float delta)
	{
		
	}

	public virtual void Update()
	{
		if (character.ShowDebugInfo)
		{
			if(parentStateMachine.StateLabel!=null)
				parentStateMachine.StateLabel.Text=parentStateMachine.currentState.Name+"/"+character.CurrentHealth;
			
		}
		else parentStateMachine.StateLabel.Text="";	
			
		
	}

	public virtual void Enter()
	{
		//GD.Print("Enter"+this.Name);
	}

	public virtual void Exit()
	{
		//GD.Print("Exit"+this.Name);
	}

	public virtual void OnReady()
	{
		
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
