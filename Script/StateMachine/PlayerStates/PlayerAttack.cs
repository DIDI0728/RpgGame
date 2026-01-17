using Godot;
using System;
using System.Xml.Serialization;

public partial class PlayerAttack : State
{

	public Area2D AttackHitBox;

	public CollisionShape2D facedirectionCollisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//AttackHitBox = GetTree().CurrentScene.FindChild("AttackHitBox") as Area2D;
		AttackHitBox =GetNode<Area2D>("../../AttackHitBox");
		foreach (var shape in AttackHitBox.GetChildren())
		{
			if (shape is CollisionShape2D collisionShape)
			{
				collisionShape.Disabled = true;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void Enter()
	{
		base.Enter();
		//播放攻击动画
		character.UpdateAnimatation();
		//启用当前朝向的攻击碰撞体
		var facingDirection = character.GetFacingDirection();
		var collisionShape = AttackHitBox.GetNode<CollisionShape2D>("CollisionShape2D_" + facingDirection);
		facedirectionCollisionShape=collisionShape;
		if (collisionShape != null)
		{
			collisionShape.Disabled = false;
		}

		
	}

	public override void Exit()
	{
		base.Exit();
		//禁用所有攻击碰撞体
		foreach (var shape in AttackHitBox.GetChildren())
		{
			if (shape is CollisionShape2D collisionShape)
			{
				collisionShape.Disabled = true;
			}
		}
	}

	public override void UpdatePhysics(float delta)
	{
		base.UpdatePhysics(delta);

	   
	}

	public override void Update()
	{
		base.Update();
		if(character.animatedSprite2D.Frame==2)
			facedirectionCollisionShape.Disabled=false;
		else if(character.animatedSprite2D.Frame==5)
			facedirectionCollisionShape.Disabled=true;


		//攻击动画播放完毕后，切换至Idle状态
		if(!character.animatedSprite2D.IsPlaying())
		{
			parentStateMachine.SwitchTo("Idle");
			return;
		}

	}

	public void OnAttackHitBoxAreaEntered(Area2D area2D)
	{
		GD.Print("攻击命中:"+area2D.GetParent().Name);
		area2D.GetParent<BaseCharacter>().TakeDamage(character.attackDamge);
		
	}


	
}
