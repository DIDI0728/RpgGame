using Godot;
using System;

public partial class PlayerRun : State
{


	public const float Speed = 200.0f;

	public const float Accerate = 10.0f;

	public override void UpdatePhysics(float delta)
	{
		base.UpdatePhysics(delta);

	
		

		//角色移动
		character.Velocity = character.Velocity.Lerp(character.inputDirection * Speed, Accerate * (float)delta);
		character.MoveAndSlide();


	}

	public override void Update()
	{
		base.Update();
		
		//更新动画
		character.UpdateAnimatation();


		//切换至Attack状态 
		if (Input.IsActionJustPressed("Attack"))
		{
           
			base.parentStateMachine.SwitchTo("Attack");
			return;
			
		}
           


		//切换至Idle状态
		if (character.inputDirection == Vector2.Zero)
		{
			parentStateMachine.SwitchTo("Idle");
			return;
		}

	

	}

}
