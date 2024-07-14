using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    

    public Enemy1_WalkingState myWalkState;
    public Enemy1_IdleState myIdleState;
    public Enemy1_PlayerDetectedState myPlayerDetectedState;
    public Enemy1_ChargeState myChargeState;
    public Enemy1_LookForPlayerState lookForPlayerState;
    public Enemy1_MeleeAttackState myMeleeAttackState;
    public Enemy1_StunState myStunState;
    public Enemy1_DeadState myDeadState;


    /*[SerializeField] IdleStateData d_IdleState;
    [SerializeField] WalkingStateData d_WalkingState;*/

    public override void Start()
    {
        base.Start();
        /* fsm.InitializeState(myWalkState);
         myWalkState = new Enemy1_WalkingState(this, fsm, "walking", d_WalkingState, this);
         myIdleState = new Enemy1_IdleState(this, fsm, "idle", d_IdleState, this);*/
        //fsm.InitializeState(myWalkState);

        myWalkState = new Enemy1_WalkingState(this, fsm, "walking", d_WalkingState, this);
        myIdleState = new Enemy1_IdleState(this, fsm, "idle", d_IdleState, this);
        myPlayerDetectedState = new Enemy1_PlayerDetectedState(this, fsm, "playerDetected", d_PlayerDetectedState, this);
        myChargeState = new Enemy1_ChargeState(this, fsm, "charge", d_ChargeState, this);
        lookForPlayerState = new Enemy1_LookForPlayerState(this, fsm, "lookForPlayer", d_LookForPlayerState, this);
        myMeleeAttackState = new Enemy1_MeleeAttackState(this, fsm, "meleeAttack", d_MeleeAttackState, meleeAttackPosition, 
            this);
        myStunState = new Enemy1_StunState(this, fsm, "stun", d_StunState, this);
        myDeadState = new Enemy1_DeadState(this, fsm, "dead", d_DeadState, this);

        fsm.InitializeState(myWalkState);
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //if(isDead)
        //{
           // fsm.ChangeState(myDeadState);
        //}
    }
    protected override void Die()
    {
        base.Die();
        fsm.ChangeState(myDeadState);
    }

    protected override void GetStunned()
    {
        base.GetStunned();
        fsm.ChangeState(myStunState);
    }

    protected override void Update()
    {
        base.Update();
    }
    /*private void OnDrawGizmos()
    {
        if (wallCheckRight == null || wallCheckLeft == null || groundCheckRight == null || groundCheckLeft == null)
        {
            Debug.Log("some error");
            return;
        }
        Gizmos.DrawLine(wallCheckRight.position, new Vector3(wallCheckRight.position.x + d_Entity.wallCheckDist, wallCheckRight.position.y, wallCheckRight.position.z));
        Gizmos.DrawLine(wallCheckLeft.position, new Vector3(wallCheckLeft.position.x - d_Entity.wallCheckDist, wallCheckLeft.position.y, wallCheckLeft.position.z));
        Gizmos.DrawLine(groundCheckLeft.position, new Vector3(groundCheckLeft.position.x, groundCheckLeft.position.y - d_Entity.groundCheckDist, groundCheckLeft.position.z));
        Gizmos.DrawLine(groundCheckRight.position, new Vector3(groundCheckRight.position.x, groundCheckRight.position.y - d_Entity.groundCheckDist, groundCheckRight.position.z));
    }*/
}
