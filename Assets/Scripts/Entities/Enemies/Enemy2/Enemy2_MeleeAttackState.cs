using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_MeleeAttackState : MeleeAttackState
{
    private Enemy2 enemy2;
    public Enemy2_MeleeAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, MeleeAttackStateData d_MeleeAttackState, 
        Transform meleeAttackPosition, Enemy2 enemy2) : base(entity, fsm, animVarName, d_MeleeAttackState, meleeAttackPosition)
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
        if(isInPlayerMaxDist&&!isAttacking)
        {
            if(isInPlayerMeleeAttackDist)
            {
                //stay
            }
            else if(isInPlayerMinDist)
            {
                fsm.ChangeState(enemy2.walkingState);
            }
        }
        else if(!isInPlayerMaxDist&&!isAttacking)
        {
            fsm.ChangeState(enemy2.lookForPlayerState);
        }
    }

    public override void BeginAction()
    {
        base.BeginAction();
    }

    public override void DoDamage()
    {
        base.DoDamage();
        isAttacking = true;
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
