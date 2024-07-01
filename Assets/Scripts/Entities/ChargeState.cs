using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected ChargeStateData d_ChargeState;
    protected bool isTouchingWall;
    protected bool isTouchingGround;
    public ChargeState(Entity entity, FiniteStateMachine fsm, string animVarName, ChargeStateData d_ChargeState) : base(entity, fsm, animVarName)
    {
        this.d_ChargeState = d_ChargeState;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();

    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
        //entity.SetVelocity(d_ChargeState.chargeSpeed);
        isTouchingWall = entity.CheckWall();
        isTouchingGround = entity.CheckGround();
    }

    public override void BeginAction()
    {
        base.BeginAction();
        isTouchingWall = entity.CheckWall();
        isTouchingGround = entity.CheckGround();
        entity.SetAnimBool(animVarName, true);
        entity.SetVelocity(d_ChargeState.chargeSpeed);
    }

    public override void EndAction()
    {
        base.EndAction();
        entity.SetAnimBool(animVarName, false);
    }
}
