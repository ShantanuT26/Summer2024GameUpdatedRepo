using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public Enemy2_WalkingState walkingState;
    public Enemy2_IdleState idleState;
    public Enemy2_PlayerDetectedState playerDetectedState;
    public Enemy2_MeleeAttackState meleeAttackState;
    public Enemy2_LookForPlayerState lookForPlayerState;
    public Enemy2_StunState stunState;
    public Enemy2_DeadState deadState;
    public Enemy2_RangedAttackState rangedAttackState;
    public override void Start()
    {
        base.Start();
        idleState = new Enemy2_IdleState(this, fsm, "idle", d_IdleState, this);
        walkingState = new Enemy2_WalkingState(this, fsm, "walking", d_WalkingState, this);
        playerDetectedState = new Enemy2_PlayerDetectedState(this, fsm, "playerDetected", d_PlayerDetectedState, this);
        meleeAttackState = new Enemy2_MeleeAttackState(this, fsm, "meleeAttack", d_MeleeAttackState, meleeAttackPosition, this);
        lookForPlayerState = new Enemy2_LookForPlayerState(this, fsm, "lookForPlayer", d_LookForPlayerState, this);
        stunState = new Enemy2_StunState(this, fsm, "stun", d_StunState, this);
        deadState = new Enemy2_DeadState(this, fsm, "dead", d_DeadState, this);
        rangedAttackState = new Enemy2_RangedAttackState(this, fsm, "rangedAttack", d_RangedAttackState, projectile, this);
        fsm.InitializeState(walkingState);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Die()
    {
        base.Die();
        fsm.ChangeState(deadState);
        //Change to dead state
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void GetStunned()
    {
        base.GetStunned();
        fsm.ChangeState(stunState);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }
}
