using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_IdleState : IdleState
{
    private NPC npc;

    public NPC_IdleState(Entity entity, FiniteStateMachine fsm, string animVarName, IdleStateData d_IdleState,
        NPC npc) : base(entity, fsm, animVarName, d_IdleState)
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
        if(!isTouchingGround || isTouchingWall)
        {
            if(Time.time > actionStartTime + d_IdleState.maxIdleTime)
            {
                entity.Flip();
                fsm.ChangeState(npc.walkingState);
            }
        }
        else
        {
            if (Time.time > actionStartTime + d_IdleState.maxIdleTime)
            {
                entity.Flip();
                fsm.ChangeState(npc.walkingState);
            }
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
