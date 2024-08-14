using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_RangedAttackState : RangedAttackState
{
    private Enemy2 enemy2;

    public Enemy2_RangedAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, RangedAttackStateData d_RangedAttackState,
        GameObject projectile, Enemy2 enemy2) : base(entity, fsm, animVarName, d_RangedAttackState, projectile)
    {
        this.enemy2 = enemy2;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
