using Godot;
using System;

public partial class PlayerIdle : State
{

    public override void UpdatePhysics(float delta)
    {
        base.UpdatePhysics(delta);

       
    }

    public override void Update()
    {
        base.Update();
       
         character.UpdateAnimatation();

         //切换至Run状态
        if (character.inputDirection.Length() > 0)
        {
            base.parentStateMachine.SwitchTo("Run");
            return;
        }



        //切换至Attack状态 
        if (Input.IsActionJustPressed("Attack"))
        {
            base.parentStateMachine.SwitchTo("Attack");
            return;
        }
            
       

    }
}
