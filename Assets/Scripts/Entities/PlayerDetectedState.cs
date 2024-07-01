using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected bool isPlayerDetected;
    protected PlayerDetectedStateData d_PlayerDetectedState;
    public PlayerDetectedState(Entity entity, FiniteStateMachine fsm, string animVarName, PlayerDetectedStateData d_PlayerDetectedState) : base(entity, fsm, animVarName)
    {
        this.d_PlayerDetectedState = d_PlayerDetectedState;
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
        isPlayerDetected = true;
        entity.SetVelocity(0f);
        entity.SetAnimBool(animVarName, isPlayerDetected);
    }

    public override void EndAction()
    {
        base.EndAction();
        isPlayerDetected = false;
        entity.SetAnimBool(animVarName, isPlayerDetected);
    }
}
