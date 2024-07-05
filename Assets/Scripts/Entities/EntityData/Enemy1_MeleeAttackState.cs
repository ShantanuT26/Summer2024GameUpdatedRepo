using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_MeleeAttackState : MeleeAttackState
{
    protected Enemy1 enemy1;
    public Enemy1_MeleeAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, MeleeAttackStateData d_MeleeAttackState, 
        Transform meleeAttackPosition, Enemy1 enemy1) : base(entity, fsm, animVarName, d_MeleeAttackState, 
            meleeAttackPosition)
    {
        this.enemy1 = enemy1;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if(isInPlayerMinDist && !isAttacking)
        {
            fsm.ChangeState(enemy1.myPlayerDetectedState);
        }
        else if(!(isInPlayerMaxDist)&&!isAttacking)
        {
            fsm.ChangeState(enemy1.lookForPlayerState);
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

    public override void DoDamage()
    {
        base.DoDamage();
        Debug.Log("DoingDamage");
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
