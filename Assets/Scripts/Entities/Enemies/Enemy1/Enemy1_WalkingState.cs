using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WalkingState : WalkingState
{
    private Enemy1 enemy1;

    public Enemy1_WalkingState(Entity entity, FiniteStateMachine fsm, string animVarName, WalkingStateData d_WalkState, Enemy1 enemy1) : base(entity, fsm, animVarName, d_WalkState)
    {
        this.enemy1 = enemy1;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        Debug.Log("myenemyposition: " + entity.gameObject.transform.GetChild(0).transform.position.x);
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
       if((isTouchingWall || !isTouchingGround) && isWalking)
        {
            //enemy.myIdleState.SetShouldBeFlipped(true);
            entity.SetFlipNow(true);
            fsm.ChangeState(enemy1.myIdleState);
        }
       else if(isWalking && entity.CheckPlayerMinDist())
        {
            fsm.ChangeState(enemy1.myPlayerDetectedState);
        }
        Debug.Log("Currentstate: " + fsm.GetCurrentState());
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
