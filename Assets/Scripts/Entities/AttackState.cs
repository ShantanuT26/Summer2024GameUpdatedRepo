using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected bool isAttacking;
    public AttackState(Entity entity, FiniteStateMachine fsm, string animVarName) : base(entity, fsm, animVarName)
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
        isAttacking = true;
        entity.SetVelocity(0f);
        entity.attackEventReceiver.attackState = this;
        
    }

    public override void EndAction()
    {
        base.EndAction();
        
    }
    public virtual void DoDamage()
    {

    }
    public virtual void FinishAttack()
    {
        isAttacking = false;
    }
}
