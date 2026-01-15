using Godot;
using System;

public partial class TileMapLayerGrass : TileMapLayer
{


	public PackedScene Grass=ResourceLoader.Load<PackedScene>("uid://xtapia6r0qon");

	RandomNumberGenerator random=new RandomNumberGenerator();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GD.Print(Grass);
		var offset=10;

		Visible=false;
		var cellArray=GetUsedCells();

		
		//GD.Print(cellArray);
		foreach (var cell in cellArray)
		{
			Vector2 newOffset =new Vector2(GD.RandRange(-offset,offset),(GD.RandRange(-offset,offset)));
			Area2D grass=Grass.Instantiate() as Area2D;
			grass.GlobalPosition=GlobalPosition+cell*32+new Vector2(16,16);
			GetParent().CallDeferred("add_child",grass);
			grass.GlobalPosition+=newOffset;

			grass.GetNode<Sprite2D>("Sprite2D").FlipH=random.RandiRange(0,1)==1;
			grass.GetNode<Sprite2D>("Sprite2D2_back").FlipH=random.RandiRange(0,1)==1;
		}

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
