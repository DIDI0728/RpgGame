using Godot;
using System;

public partial class StateMachine : Node
{
	[Export] public State currentState;

	[Export]public Label StateLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var state in this.GetChildren())
		{
			var childState = state as State;
			childState.parentStateMachine = this;
			childState.character=GetParent() as BaseCharacter;
			childState.OnReady();
		}

		currentState = GetChild(0) as State;
		currentState.Enter();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		currentState.Update();

	}

    public override void _PhysicsProcess(double delta)
    {
		currentState.UpdatePhysics((float)delta);
        
    }

	public void SwitchTo(String nextStateName)
	{
		State nextState=GetNode(nextStateName) as State;
		if (nextState == null)
		{
			GD.Print("未找到下一状态节点");
		}else
		{
			currentState.Exit();
			currentState=nextState;
			currentState.Enter();
		}
		
	}

}
