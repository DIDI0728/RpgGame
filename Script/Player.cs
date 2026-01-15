using Godot;
using System;
using System.Transactions;

public partial class Player : BaseCharacter
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    }

}
