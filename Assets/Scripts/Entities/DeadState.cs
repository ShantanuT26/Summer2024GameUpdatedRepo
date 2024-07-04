using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected Enemy1 enemy1;
    protected DeadStateData d_DeadState;
    public DeadState(Entity entity, FiniteStateMachine fsm, string animVarName, DeadStateData d_DeadState, Enemy1 enemy1) : base(entity, fsm, animVarName)
    {
        this.d_DeadState = d_DeadState;
        this.enemy1 = enemy1;
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
    }

    public override void EndAction()
    {
        base.EndAction();
    }
}
