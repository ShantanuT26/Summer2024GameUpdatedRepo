using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_LookForPlayerState : LookForPlayerState
{
    private Enemy2 enemy2;

    public Enemy2_LookForPlayerState(Entity entity, FiniteStateMachine fsm, string animVarName, LookForPlayerStateData d_LookForPLayerState,
        Enemy2 enemy2) : base(entity, fsm, animVarName, d_LookForPLayerState)
    {
        this.enemy2 = enemy2; 
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if (Time.time > lastFlipCompleted + d_LookForPLayerState.timeBetweenFlips)
        {
            if (FlipsCompleted < d_LookForPLayerState.numFlips)
            {
                entity.Flip();
                FlipsCompleted++;
                lastFlipCompleted = Time.time;

                if (entity.CheckPlayerMinDist())
                {
                    fsm.ChangeState(enemy2.walkingState);
                }
                else if(entity.CheckPlayerMaxDist())
                {
                    fsm.ChangeState(enemy2.playerDetectedState);
                }
            }
            else
            {
                fsm.ChangeState(enemy2.walkingState);
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
