using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerFiniteStateMachine fsm;
    private PlayerRunState runState;
    private PlayerIdleState idleState;
    private PlayerData playerData;
    private Animator animator;
    private Rigidbody rb;
    private void Awake()
    {
        fsm = new PlayerFiniteStateMachine();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        idleState = new PlayerIdleState(this, fsm, playerData, "idle");
        runState = new PlayerRunState(this, fsm, playerData, "run");
        fsm.SetInitialState(idleState);
    }
    private void OnEnable()
    {
        PlayerState.changeAnimBool += SetAnimBool;
    }
    private void Start()
    {
    }
    private void Update()
    {
        fsm.currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();
    }
    public void SetAnimBool(string x, bool y)
    {
        animator.SetBool(x, y);
    }
}
