using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy1;
    public Enemy1_PlayerDetectedState(Entity entity, FiniteStateMachine fsm, string animVarName, PlayerDetectedStateData d_PlayerDetectedState, 
        Enemy1 enemy1) : base(entity, fsm, animVarName, d_PlayerDetectedState)
    {
        this.enemy1 = enemy1;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
        if (entity.CheckMeleeAttackDist())
        {
            fsm.ChangeState(enemy1.myMeleeAttackState);
        }
        if (Time.time > actionStartTime + d_PlayerDetectedState.timeUntilCharge)
        {
             fsm.ChangeState(enemy1.myChargeState);
        }
        else if(entity.CheckPlayerMaxDist()==false)
        {
            fsm.ChangeState(enemy1.lookForPlayerState);
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
}
