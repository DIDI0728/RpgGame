using Godot;
using System;
using Godot.Collections;

public partial class RandomRegion : Sprite2D
{


	[Export] private Array<Rect2> rectRegions;
	RandomNumberGenerator random=new RandomNumberGenerator();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		var randomIndex=random.RandiRange(0,rectRegions.Count-1);

		this.RegionRect=rectRegions[randomIndex];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
