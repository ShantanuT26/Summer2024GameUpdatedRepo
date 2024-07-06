using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 enemy2;
    public Enemy2_PlayerDetectedState(Entity entity, FiniteStateMachine fsm, string animVarName, PlayerDetectedStateData d_PlayerDetectedState,
        Enemy2 enemy2) : base(entity, fsm, animVarName, d_PlayerDetectedState)
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
        //If player is in max dist, fire arrows
        else if(!isInPlayerMaxDist)
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
}
