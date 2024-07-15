using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    //TEMP variable
    private bool grounded;
    private PlayerFiniteStateMachine fsm;
    public PlayerRunState runState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    [SerializeField]private PlayerData playerData;
    private Animator animator;
    public PlayerInputHandler playerInputHandler { get; private set; }
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private Transform groundCheck;
    public bool animationFinished { get; private set; }
    public int facingDirection { get; private set; }

    public int numJumpsLeft {get; private set; }
    private void Awake()
    {
        fsm = new PlayerFiniteStateMachine();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        idleState = new PlayerIdleState(this, fsm, playerData, "idle");
        runState = new PlayerRunState(this, fsm, playerData, "run");
        jumpState = new PlayerJumpState(this, fsm, playerData, "inAir");
        landState = new PlayerLandState(this, fsm, playerData, "land");
        inAirState = new PlayerInAirState(this, fsm, playerData, "inAir");
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerInputHandler.SetCanJump(true);
        facingDirection = 1;
        animationFinished = false;
        numJumpsLeft = playerData.numJumps;
    }
    private void OnEnable()
    {   
        PlayerState.changeAnimBool += SetAnimBool;
        fsm.SetInitialState(idleState);
    }
    private void Start()
    {
        PhysicsUpdate();
    }
    private void Update()
    {
        fsm.currentState.LogicUpdate();
        CheckIfShouldFlip();
    }
    private void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();
        CheckGround();
        PhysicsUpdate();
    }
    private void SetAnimBool(string x, bool y)
    {
        Debug.Log("animboolset: " + x + ": " + y);
        animator.SetBool(x, y);
    }
    //Temporary method
    public void SuspendPlayerInAir(bool x)
    {
        if(x)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 8;
        }   
    }
    public void SetVelocity(float x)
    {
        rb.velocity = new Vector2(x, rb.velocity.y);
    }
    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
    public void SetAnimationFinished(bool x) => animationFinished = x;

    public void AnimationFinished() => animationFinished = true;
    public bool CheckGround()
    {
        bool x = Physics2D.OverlapCircle(groundCheck.transform.position, playerData.groundCheckRadius, playerData.isGround);
        return x;
    }
    public void SetVelocityY(float x)
    {
        rb.velocity = new Vector2(rb.velocity.x, x);
    }
    public void Flip()
    {
        facingDirection *= -1;
        switch(sr.flipX)
        {
            case true:
                sr.flipX = false;
                break;
            case false:
                sr.flipX = true;
                break;
        }
    }
    public void SubtractNumJumpsLeft()
    {
        numJumpsLeft--;
    }
    public void ResetNumJumpsLeft()
    {
        numJumpsLeft = playerData.numJumps;
    }
    public void PhysicsUpdate()
    {
        animator.SetFloat("XVelocity", rb.velocity.x);
        animator.SetFloat("YVelocity", rb.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);  
    }
    public void SuspendHorizontalMovement()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
    public void CheckIfShouldFlip()
    {
        if (playerInputHandler.movementInput.x == facingDirection * -1)
        {
            Flip();
        }
    }
}
    