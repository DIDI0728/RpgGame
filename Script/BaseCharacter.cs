using Godot;
using System;

public partial class BaseCharacter : CharacterBody2D
{

	public Vector2 inputDirection = Vector2.Zero;   //当前键盘输入方向

	public AnimatedSprite2D animatedSprite2D;
	public String FacingDirection = "Down";   //当前角色朝向
	public String AnimationName;  //播放的动画名称

	public StateMachine stateMachine;
	[Export] public bool ShowDebugInfo = true;

	public override void _Ready()
	{
		base._Ready();
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		stateMachine = GetNode<StateMachine>("StateMachine");
	}

	//获取角色当前朝向
	public virtual String GetFacingDirection()
	{
		if (inputDirection == Vector2.Zero)
			return FacingDirection;

		if (inputDirection.Y > 0)
			FacingDirection = "Down";
		else if (inputDirection.Y < 0)
			FacingDirection = "Up";
		else
		{
			if (inputDirection.X > 0)
				FacingDirection = "Right";
			else FacingDirection = "Left";
		}
		return FacingDirection;
	}

	public virtual void UpdateAnimatataion()

	{
		animatedSprite2D.Play(stateMachine.currentState.Name + "_" + GetFacingDirection());
	}
}
