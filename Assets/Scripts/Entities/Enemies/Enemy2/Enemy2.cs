using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public Enemy2_WalkingState walkingState;
    public Enemy2_IdleState idleState;
    public Enemy2_PlayerDetectedState playerDetectedState;
    public override void Start()
    {
        base.Start();
        idleState = new Enemy2_IdleState(this, fsm, "idle", d_IdleState, this);
        walkingState = new Enemy2_WalkingState(this, fsm, "walking", d_WalkingState, this);
        playerDetectedState = new Enemy2_PlayerDetectedState(this, fsm, "playerDetected", d_PlayerDetectedState, this);
        fsm.InitializeState(walkingState);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void GetStunned()
    {
        base.GetStunned();
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
