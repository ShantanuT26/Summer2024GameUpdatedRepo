using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_StunState : StunState
{
    private Enemy2 enemy2;
    public Enemy2_StunState(Entity entity, FiniteStateMachine fsm, string animVarName, StunStateData d_StunState, 
        Enemy2 enemy2) : base(entity, fsm, animVarName, d_StunState)
    {
        this.enemy2 = enemy2;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if(Time.time > stunTimeStart+d_StunState.stunTime)
        {
            //shoot arrow if player is in max dist, or beyond max dist
            //melee attack if in melee attack range, or if in min agro range
        }
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
        if(isInPlayerMeleeAttackDist)
        {
            fsm.ChangeState(enemy2.meleeAttackState);
        }
        else if(isInPlayerMinDist)
        {
            fsm.ChangeState(enemy2.walkingState);
        }
        else if(isInPlayerMaxDist)
        {
            fsm.ChangeState(enemy2.playerDetectedState);
        }
        else
        {
            fsm.ChangeState(enemy2.lookForPlayerState);
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
