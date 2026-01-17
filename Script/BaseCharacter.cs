using Godot;
using System;
using System.Numerics;

public partial class BaseCharacter : CharacterBody2D
{

	public Godot.Vector2 inputDirection = Godot.Vector2.Zero;   //当前键盘输入方向

	public AnimatedSprite2D animatedSprite2D;
	public String FacingDirection = "Down";   //当前角色朝向
	public String AnimationName;  //播放的动画名称

	public StateMachine stateMachine;//状态机


	[Export] public int maxHealth = 100;
	public bool IsDead;
	private int currentHealth;
	public int CurrentHealth
	{
		get => currentHealth;
		set
		{
			currentHealth = value;
			currentHealth = Math.Clamp(value, 0, maxHealth);
			if (currentHealth <= 0)
			{
				IsDead = true;
				GD.Print(Name + "角色死亡");
			}
		}
	}

	[Export] public int attackDamge = 50;

	[Export] public bool ShowDebugInfo = true;

	public override void _Ready()
	{
		base._Ready();
		CurrentHealth = maxHealth;
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		stateMachine = GetNode<StateMachine>("StateMachine");
	}

	//获取角色当前朝向
	public virtual String GetFacingDirection()
	{
		if (inputDirection == Godot.Vector2.Zero)
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

	public virtual void UpdateAnimatation()

	{
		animatedSprite2D.Play(stateMachine.currentState.Name + "_" + GetFacingDirection());
	}

	public void TakeDamage(int damage)
	{
		if (CurrentHealth <= 0)
		{
			stateMachine.SwitchTo("Dead");
			return;
		} 
		CurrentHealth -= damage;
		stateMachine.SwitchTo("Hurt");
		GD.Print(Name + "受到伤害:" + damage + "当前生命值:" + CurrentHealth);
	}
}
