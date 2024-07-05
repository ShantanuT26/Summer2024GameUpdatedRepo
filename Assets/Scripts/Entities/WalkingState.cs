using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WalkingState : State
{
    protected WalkingStateData d_WalkState;
    protected bool isWalking;

    public WalkingState(Entity entity, FiniteStateMachine fsm, string animVarName, WalkingStateData d_WalkState) : base(entity, fsm, animVarName)
    {
        this.d_WalkState = d_WalkState;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
    }

    public override void ActionPhysicsUpdate()
    {
        Debug.Log("walkingphysicsupdate");
        base.ActionPhysicsUpdate();
        entity.SetVelocity(d_WalkState.walkingSpeed);

       /* if(isTouchingWall&&isTouchingGround)
        {
            entity.Flip();
        }
        else if(!isTouchingGround)
        {
            entity.Flip();
        }*/


        //This is where I update the isTouchingRround and isTouchingWall variables

        //This nect chunk goes inside the enemyWalkingState class, since the transform component belongs to the enemy, not the entity
        /*if (entity.wallCheckRight.gameObject.activeSelf)
        {
            Debug.Log("Right on!");
            isTouchingWall = Physics2D.Raycast(entity.wallCheckRight.transform.position, transform.right, wallCheckDist, whatIsGround);
            isTouchingGround = Physics2D.Raycast(entity.groundCheckRight.transform.position, -transform.up, groundCheckDist, whatIsGround);
        }
        else if (entity.wallCheckLeft.gameObject.activeSelf)
        {
            Debug.Log("Left on!");
            isTouchingWall = Physics2D.Raycast(entity.wallCheckLeft.transform.position, -transform.right, wallCheckDist, whatIsGround);
            isTouchingGround = Physics2D.Raycast(entity.groundCheckLeft.transform.position, -transform.up, groundCheckDist, whatIsGround);
        }*/
    }

    public override void BeginAction()
    {
        Debug.Log("WalkingBegun");
        base.BeginAction();
        isWalking = true;
        entity.SetVelocity(d_WalkState.walkingSpeed);
        Debug.Log("getvelocity: " + entity.GetVelocity());
        entity.SetAnimBool(animVarName, isWalking);
    }

    public override void EndAction()
    {
        base.EndAction();
        isWalking = false;
        entity.SetAnimBool(animVarName, isWalking);
    }
}
