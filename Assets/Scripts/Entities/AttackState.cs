using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    public AttackState(Entity entity, FiniteStateMachine fsm, string animVarName, Transform attackPosition) : base(entity, fsm, animVarName)
    {
        this.attackPosition = attackPosition;
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

    }
}
