using Godot;
using System;

public partial class EnemyIdle : State
{

	public float playerDetectionRadius = 100.0f;
	public Polygon2D polygon2D;

	public override void _Ready()
	{
		base._Ready();
		polygon2D = GetTree().CurrentScene.FindChild("Polygon2D") as Polygon2D;
	}

    public override void Enter()
    {
        base.Enter();
		if(character.ShowDebugInfo)
			CreatePolygonCircle();
		else polygon2D.Polygon = null;
    }

	public override void Exit()
	{
		base.Exit();
		polygon2D.Polygon = null;
	}


	public override void UpdatePhysics(float delta)
	{
		base.UpdatePhysics(delta);

		// 安全地把 character 转为 Enemy 以访问 player 字段
		var enemy = character as Enemy;
		if (enemy == null || enemy.player == null)
			return;

		// 检测玩家是否进入敌人侦测范围（使用距离比较）
		if (enemy.GlobalPosition.DistanceTo(enemy.player.GlobalPosition) <= playerDetectionRadius)
		{
			parentStateMachine.SwitchTo("Move");
		}
		/* if (enemy.GlobalPosition.DistanceTo(enemy.player.GlobalPosition) > playerDetectionRadius)
		{
			parentStateMachine.SwitchTo("Idle");
			CreatePolygonCircle();
		} */
	}

	public override void Update()
    {
        base.Update();
         //切换动画
		character.UpdateAnimatataion();
    }

	public void CreatePolygonCircle(){
		var points = new Vector2[36];
		for (int i = 0; i < 36; i++)
		{
			float angle = Mathf.DegToRad(i * 10);
			points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * playerDetectionRadius;
		}
		polygon2D.Polygon = points;
	}
}
