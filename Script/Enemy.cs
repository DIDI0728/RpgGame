using Godot;
using System;

public partial class Enemy : BaseCharacter
{

	Line2D line2;
	public BaseCharacter player;

	public Vector2 playerDirection;
	public float playerAngle;



	

    public override void _Ready()
    {
		base._Ready();
        line2=GetNode<Line2D>("Line2D");
		player=GetTree().CurrentScene.GetNode<BaseCharacter>("/root/Node2D/Level/Player");
		
    }

    public override void _Process(double delta)
    {
        playerDirection=player.GlobalPosition-GlobalPosition;

		Vector2[] vectors=line2.Points;
		vectors[0]=new Vector2(0,0);
		vectors[1]=playerDirection.Normalized()*40;
		if(ShowDebugInfo)
			line2.Points=vectors;
		else
			line2.Points=new Vector2[2];

		playerDirection.Y=-playerDirection.Y;
		playerAngle=Mathf.RadToDeg(playerDirection.Angle());
		if(playerAngle <0)
			playerAngle+=360;


		if (Input.IsKeyPressed(Key.V))
		{
			GD.Print(playerAngle);
		}

    }


	public override String GetFacingDirection()
	{
		FacingDirection="Up";
		if(playerAngle>135 && playerAngle <225) 
			FacingDirection="Left";	
		else if(playerAngle>225 && playerAngle <315)
			FacingDirection="Down";
		else if(playerAngle>315 || playerAngle <45)
			FacingDirection="Right";

		return FacingDirection;
	}



}
