using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData d_MeleeAttackState;

    public MeleeAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, Transform attackPosition) : base(entity, fsm, animVarName, attackPosition)
    {
        this.d_MeleeAttackState = d_MeleeAttackState;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
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
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPosition.position, 
            d_MeleeAttackState.attackRadius, d_MeleeAttackState.whatIsPlayer);
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
