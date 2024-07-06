using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_WalkingState : WalkingState
{
    Enemy2 enemy2;
    public Enemy2_WalkingState(Entity entity, FiniteStateMachine fsm, string animVarName, WalkingStateData d_WalkState, 
        Enemy2 enemy2) : base(entity, fsm, animVarName, d_WalkState)
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
        if(isTouchingWall || !isTouchingGround)
        {
            entity.SetFlipNow(true);

            fsm.ChangeState(enemy2.idleState);
        }
        else if(isInPlayerMinDist)
        {
            //stay in walking state
            if(isInPlayerMeleeAttackDist)
            {
                fsm.ChangeState(enemy2.meleeAttackState);
            }
        }
        else if(isInPlayerMaxDist)
        {
            fsm.ChangeState(enemy2.playerDetectedState);
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
