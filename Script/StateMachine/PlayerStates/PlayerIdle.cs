using Godot;
using System;

public partial class PlayerIdle : State
{

    public override void UpdatePhysics(float delta)
    {
        base.UpdatePhysics(delta);

        //切换至Run状态
        if (character.inputDirection.Length() > 0)
            base.parentStateMachine.SwitchTo("Run");
    }

    public override void Update()
    {
        base.Update();
         //切换Idle动画
        /* character.AnimationName="Idle_"+character.GetFacingDirection();
        character.animatedSprite2D.Play(character.AnimationName); */
        character.UpdateAnimatataion();
    }
}
