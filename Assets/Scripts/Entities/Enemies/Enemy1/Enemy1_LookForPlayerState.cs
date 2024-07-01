using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 enemy1;
    public Enemy1_LookForPlayerState(Entity entity, FiniteStateMachine fsm, string animVarName, LookForPlayerStateData d_LookForPLayerState, Enemy1 enemy1) : base(entity, fsm, animVarName, d_LookForPLayerState)
    {
        this.enemy1 = enemy1;   
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if(Time.time > lastFlipCompleted + d_LookForPLayerState.timeBetweenFlips)
        {
            if(FlipsCompleted<d_LookForPLayerState.numFlips)
            {
                entity.Flip();
                FlipsCompleted++;
                lastFlipCompleted = Time.time;
                if(entity.CheckPlayerMinDist())
                {
                    fsm.ChangeState(enemy1.myPlayerDetectedState);
                }
            }
            else
            {
                fsm.ChangeState(enemy1.myWalkState);
            }
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
