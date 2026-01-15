using Godot;
using System;

public partial class PlayerRun : State
{


	public const float Speed = 200.0f;

	public const float Accerate = 10.0f;

	public override void UpdatePhysics(float delta)
	{
		base.UpdatePhysics(delta);

		//切换至Idle节点
		if (character.inputDirection == Vector2.Zero)
		{
			parentStateMachine.SwitchTo("Idle");
			return;
		}
		

		//角色移动
		character.Velocity = character.Velocity.Lerp(character.inputDirection * Speed, Accerate * (float)delta);
		character.MoveAndSlide();


	}

	public override void Update()
	{
		base.Update();
		
		//切换至Run动画
		/* character.AnimationName="Run_"+character.GetFacingDirection();
		character.animatedSprite2D.Play(character.AnimationName); */
		character.UpdateAnimatataion();

	}

}
