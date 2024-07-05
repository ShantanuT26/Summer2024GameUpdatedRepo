using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_IdleState : IdleState
{
    private Enemy2 enemy2;
    public Enemy2_IdleState(Entity entity, FiniteStateMachine fsm, string animVarName, IdleStateData d_IdleState, 
        Enemy2 enemy2) : base(entity, fsm, animVarName, d_IdleState)
    {
        this.enemy2 = enemy2;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate(); 
        if(isInPlayerMinDist)
        {
            fsm.ChangeState(enemy2.playerDetectedState);
        }
        else if(Time.time > actionStartTime + d_IdleState.maxIdleTime)
        {
            if(entity.flipNow)
            {
                entity.Flip();
                entity.SetFlipNow(false);
            }
            fsm.ChangeState(enemy2.walkingState);
        }
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
