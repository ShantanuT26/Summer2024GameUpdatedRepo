using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected bool isPlayerDetected;
    protected PlayerDetectedStateData d_PlayerDetectedState;
    protected bool isTimeUp;
    
    public PlayerDetectedState(Entity entity, FiniteStateMachine fsm, string animVarName, PlayerDetectedStateData d_PlayerDetectedState) : base(entity, fsm, animVarName)
    {
        this.d_PlayerDetectedState = d_PlayerDetectedState;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        if(Time.time > actionStartTime + d_PlayerDetectedState.timeUntilCharge)
        {
            isTimeUp = true;
        }
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
        isTimeUp = false;
    }

    public override void EndAction()
    {
        base.EndAction();
        isPlayerDetected = false;
        entity.SetAnimBool(animVarName, isPlayerDetected);
    }
}
