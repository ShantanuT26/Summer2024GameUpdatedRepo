using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_ChargeState : ChargeState
{
    private Enemy1 enemy1;
    public Enemy1_ChargeState(Entity entity, FiniteStateMachine fsm, string animVarName, ChargeStateData d_ChargeState, Enemy1 enemy1) : base(entity, fsm, animVarName, d_ChargeState)
    {
        this.enemy1 = enemy1;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if(isTouchingWall || !isTouchingGround)
        {
            Debug.Log("enteringlookforplayerstate; isTouchingGround = " + isTouchingGround);
            fsm.ChangeState(enemy1.lookForPlayerState);
        }
        if(entity.CheckMeleeAttackDist())
        {
            fsm.ChangeState(enemy1.myMeleeAttackState);
        }
        else if(Time.time > actionStartTime + d_ChargeState.chargeTime)
        {
            if(!(entity.CheckPlayerMinDist()))
            {
                fsm.ChangeState(enemy1.lookForPlayerState);
            }
            else
            {
                fsm.ChangeState(enemy1.myPlayerDetectedState);
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
