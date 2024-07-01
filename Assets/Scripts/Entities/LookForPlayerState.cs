using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected LookForPlayerStateData d_LookForPLayerState;
    protected int FlipsCompleted;
    protected float lastFlipCompleted;
    public LookForPlayerState(Entity entity, FiniteStateMachine fsm, string animVarName, LookForPlayerStateData d_LookForPLayerState) : base(entity, fsm, animVarName)
    {
        this.d_LookForPLayerState= d_LookForPLayerState;
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
        lastFlipCompleted = actionStartTime;
        entity.SetAnimBool(animVarName, true);
        entity.SetVelocity(0f);
        FlipsCompleted = 0;
    }

    public override void EndAction()
    {
        base.EndAction();
        entity.SetAnimBool(animVarName, false);
        FlipsCompleted = 0;
    }
}
