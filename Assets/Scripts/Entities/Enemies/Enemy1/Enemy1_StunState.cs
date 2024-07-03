using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_StunState : StunState
{
    private Enemy1 enemy1;
    
    public Enemy1_StunState(Entity entity, FiniteStateMachine fsm, string animVarName, StunStateData d_StunState, 
        Enemy1 enemy1) : base(entity, fsm, animVarName, d_StunState)
    {
        this.enemy1 = enemy1;   
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if(Time.time > stunTimeStart + d_StunState.stunTime)
        {
            if(entity.CheckMeleeAttackDist())
            {
                fsm.ChangeState(enemy1.myMeleeAttackState);
            }
            else if(entity.CheckPlayerMinDist())
            {
                fsm.ChangeState(enemy1.myPlayerDetectedState);
            }
            else if(!(entity.CheckPlayerMaxDist()))
            {
                fsm.ChangeState(enemy1.lookForPlayerState);
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
