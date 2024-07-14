using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected IdleStateData d_IdleState;
    protected bool isIdle;

    public IdleState(Entity entity, FiniteStateMachine fsm, string animVarName, IdleStateData d_IdleState) : base(entity, fsm, animVarName)
    {
        this.d_IdleState = d_IdleState;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
    }

    public override void BeginAction()
    {
        base.BeginAction();
        isIdle = true;
        entity.SetVelocity(0f);
        entity.SetAnimBool(animVarName, isIdle);
    }

    public override void EndAction()
    {
        base.EndAction();
        isIdle = false;
        entity.SetAnimBool(animVarName, isIdle);
    }
}
