using Godot;
using System;

public partial class Grass : Area2D
{

	Tween skewTween_back;
	Tween skewTween;
	Tween scaleTween;

	Vector2 startScale=new Vector2(1,1);
	Vector2 endScale=new Vector2(1,0.5f);
	Sprite2D sprite2D_back;
	Sprite2D sprite2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		
		//初始化补间动画并设置循环
		skewTween=GetTree().CreateTween().SetLoops();
		skewTween_back=GetTree().CreateTween().SetLoops();

		sprite2D_back=GetNode<Sprite2D>("Sprite2D2_back");
		sprite2D=GetNode<Sprite2D>("Sprite2D");

		var endSkew=Mathf.DegToRad(GD.RandRange(-10f,10f));
		var startSkew=-endSkew;


		skewTween.TweenProperty(sprite2D,"skew",endSkew,1.5).From(startSkew);
		skewTween.TweenProperty(sprite2D,"skew",startSkew,1.5).From(endSkew);
		skewTween.SetEase(Tween.EaseType.InOut);

		var endSkewBack=endSkew*0.5;
		var startSkewBack=-endSkewBack;
		
		skewTween_back.TweenProperty(sprite2D_back,"skew",endSkewBack,1.5).From(startSkewBack);
		skewTween_back.TweenProperty(sprite2D_back,"skew",startSkewBack,1.5).From(endSkewBack);
		skewTween_back.SetEase(Tween.EaseType.InOut);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void OnBodyEnterHandler(Node2D node2D)
	{
		createNewTween(endScale,0.1f);

	}

	public void OnBodyExitHandler(Node2D node2D)
	{
		createNewTween(startScale,0.5f);
	}

	public void createNewTween(Vector2 vector2,float duration)

	{
		if (scaleTween!=null)
		{
			scaleTween.Kill();
		}

		scaleTween=GetTree().CreateTween();
		scaleTween.TweenProperty(sprite2D,"scale",vector2,duration);
		scaleTween.SetEase(Tween.EaseType.Out);

	}
}
