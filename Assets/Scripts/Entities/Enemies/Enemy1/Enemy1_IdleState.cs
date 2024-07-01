using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_IdleState : IdleState
{
    private Enemy1 enemy1;

    public Enemy1_IdleState(Entity entity, FiniteStateMachine fsm, string animVarName, IdleStateData d_IdleState, Enemy1 enemy1) : base(entity, fsm, animVarName, d_IdleState)
    {
        this.enemy1 = enemy1;
    }

    //private bool shouldBeFlipped;




    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
        
        if (Time.time > actionStartTime + d_IdleState.maxIdleTime && isIdle)
        {
            if (entity.flipNow)
            {
                entity.Flip();
                entity.SetFlipNow(false);
            }
            fsm.ChangeState(enemy1.myWalkState);
        }
        else if(entity.CheckPlayerMinDist())
        {
            fsm.ChangeState(enemy1.myPlayerDetectedState);
        }
        //else if(isIdle && )
        
    }

    public override void BeginAction()
    {
        base.BeginAction();

    }
    private void CheckIfFlip()
    {

    }
    public override void EndAction()
    {
        base.EndAction();
    }
   /* public void SetShouldBeFlipped(bool x)
    {
        shouldBeFlipped = x;
    }*/
}
