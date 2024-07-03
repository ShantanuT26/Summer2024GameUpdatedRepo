using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected StunStateData d_StunState;
    protected float stunTimeStart;
    public StunState(Entity entity, FiniteStateMachine fsm, string animVarName, StunStateData d_StunState) : base(entity, fsm, animVarName)
    {
        this.d_StunState = d_StunState;
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
        entity.SetAnimBool(animVarName, true);
        stunTimeStart = Time.time;
    }

    public override void EndAction()
    {
        base.EndAction();
        entity.SetAnimBool(animVarName, false);
    }
}
