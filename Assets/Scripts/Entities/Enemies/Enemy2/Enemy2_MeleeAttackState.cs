using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_MeleeAttackState : MeleeAttackState
{
    public Enemy2_MeleeAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, MeleeAttackStateData d_MeleeAttackState, Transform meleeAttackPosition) : base(entity, fsm, animVarName, d_MeleeAttackState, meleeAttackPosition)
    {
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

    public override void EndAction()
    {
        base.EndAction();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
