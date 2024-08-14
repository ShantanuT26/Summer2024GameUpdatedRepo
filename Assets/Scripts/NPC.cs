using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    public NPC_IdleState idleState { get; private set; }
    public NPC_WalkingState walkingState { get; private set; }

    public override void Start()
    {
        base.Start();
        fsm = new FiniteStateMachine();
        idleState = new NPC_IdleState(this, fsm, "idle", d_IdleState, this);
        walkingState = new NPC_WalkingState(this, fsm, "walking", d_WalkingState, this);
        fsm.InitializeState(walkingState);
    }

    protected override void Awake()
    {
        base.Awake();
        
    }
}
