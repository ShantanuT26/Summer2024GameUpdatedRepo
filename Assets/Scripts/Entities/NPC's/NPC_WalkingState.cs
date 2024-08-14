using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_WalkingState : WalkingState
{
    private NPC npc;

    public NPC_WalkingState(Entity entity, FiniteStateMachine fsm, string animVarName, WalkingStateData d_WalkState,
        NPC npc) : base(entity, fsm, animVarName, d_WalkState)
    {
        this.npc = npc;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
        if(Time.time > actionStartTime + d_WalkState.maxWalkTime)
        {
            fsm.ChangeState(npc.idleState);
        }
        if(isTouchingWall || !isTouchingGround)
        {
            fsm.ChangeState(npc.idleState);
        }
    }

    public override void BeginAction()
    {
        base.BeginAction();
    }

    public override void EndAction()
    {
        base.EndAction();
    }

    protected override void Checks()
    {
        base.Checks();
    }
}
