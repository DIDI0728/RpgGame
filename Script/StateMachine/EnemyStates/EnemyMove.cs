using Godot;
using System;

public partial class EnemyMove : State
{

	[Export] public NavigationAgent2D navigationAgent2D;
	public Vector2 direction;
	[Export]public float Speed;
	public const float MaxSpeed = 50.0f;
	public const float MinSpeed = 20.0f;

	[Export]public Timer playerPosUpdateTimer;
	// Called when the node enters the scene tree for the first time.
	public override void OnReady()
	{
		base.OnReady();
		
		//playerPosUpdateTimer=GetParent().GetParent().FindChild("PlayerPosUpdateTimer") as Timer;
		//navigationAgent2D=GetParent().GetParent().FindChild("NavigationAgent2D") as NavigationAgent2D;
		playerPosUpdateTimer = GetNode<Timer>("../../PlayerPosUpdateTimer");
		navigationAgent2D =GetNode<NavigationAgent2D>("../../NavigationAgent2D");
		
	}

	public override void Enter()
	{
		base.Enter();
		playerPosUpdateTimer.Start();

	}

	public override void Exit()
	{
		base.Exit();
		playerPosUpdateTimer.Stop();
	}

	//状态机调用
	public override void UpdatePhysics(float delta)
	{
		base.UpdatePhysics(delta);
		direction = character.GlobalPosition.DirectionTo(navigationAgent2D.GetNextPathPosition());

		if (!navigationAgent2D.IsTargetReached())
		{
			character.Velocity = character.Velocity.Lerp(direction * Speed, (float)delta);
			character.MoveAndSlide();
		}

	}

	//状态机调用
	public override void Update()
	{
		base.Update();
		character.UpdateAnimatation();
		Speed=new RandomNumberGenerator().RandfRange(MinSpeed,MaxSpeed);
		
	}


	//引擎自动调用，非进入状态机调用
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);


	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.//引擎自动调用，非进入状态机调用
	public override void _Process(double delta)
	{
		character.UpdateAnimatation();
		navigationAgent2D.TargetPosition = (character as Enemy).player.GlobalPosition;
	}

		public void OnPlayerPosUpdateTimerTimeout()
	{
		navigationAgent2D.TargetPosition = (character as Enemy).player.GlobalPosition;
	}

	public void OnNavigationAgent2DVelocityComputed(Vector2 SafeVelocity)
	{
		if(parentStateMachine.currentState==this && !navigationAgent2D.IsTargetReached())
            character.Velocity += SafeVelocity * (float)GetPhysicsProcessDeltaTime();
		
	}
}
