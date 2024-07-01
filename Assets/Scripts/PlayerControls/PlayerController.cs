using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Diagnostics;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEditor.Tilemaps;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using Unity.Burst;


/*
    KEY: @ = feature where you arent allowed to jump off a wall, and then turn around and jump onto the same wall
 */
public class PlayerController : MonoBehaviour
{
    [SerializeField]private int numJumps;
    [SerializeField]private int numJumpsLeft;
    private int lastWallJumpDirection;
    private int facingDirection;
    private int knockBackDir;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float movementForceInAir;
    [SerializeField]private float ledgeoffset1x=0f;
    [SerializeField] private float ledgeoffset1y = 0.1f;
    [SerializeField] private float ledgeoffset2x = 1f;
    [SerializeField] private float ledgeoffset2y = 1f;
    [SerializeField] private float wallDist;
    [SerializeField]private float knockBackTime;
    private float movementSpeed;
    private float dashSpeed;
    private float jumpForce = 16f;
    private UnityEngine.Vector2 forceToAdd;
    private float airDragMultiplier = 0.95f;
    private float variableJumpHeightMultiplier = 0.5f;
    private float wallHopForce=40f;
    private float wallJumpForce=40f;
    private UnityEngine.Vector2 wallHopDirection = new UnityEngine.Vector2(2f, 2f);
    private UnityEngine.Vector2 wallJumpDirection = new UnityEngine.Vector2(2f, 2f);
    private float wallJumpTimer;
    private float wallJumpTimerSet=0.04f;
    private float maxDashTime = 0.4f;
    private float timeDashActivated;
    private float knockBackStartTime;


    private Rigidbody2D rb;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    UnityEngine.Vector2 mydebug;
    UnityEngine.Vector2 movementVector;
    [SerializeField]private UnityEngine.Vector2 knockback;
    [SerializeField] private bool isGrounded;
    [SerializeField]private bool isTouchingWall;
    [SerializeField]private bool isTouchingLedge;
    [SerializeField]private bool canJump;
    [SerializeField]private bool isWallSliding;
    [SerializeField]private bool canMove;
    private bool isFacingRight;
    private bool hasWallJumped;
    private bool hasWallJumpFailed;
    private bool canLedgeClimb;
    private bool isWalking;
    private bool unfinishedLedgeClimb;
    private bool isStuck;
    private bool isDashing;
    private bool isKnockedBack;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    [SerializeField]private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    private UnityEngine.Vector2 ledgePos1;
    private UnityEngine.Vector2 ledgePos2;
    private UnityEngine.Vector2 startPosition;

    private PlayerCombat playerCombat;

    private void Awake()
    {
        startPosition = transform.position;
        canMove = true;
        canLedgeClimb = false;
        numJumpsLeft = numJumps;
        isWalking = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movementSpeed = 10f;
        dashSpeed = 25f;
        spriteRenderer=GetComponent<SpriteRenderer>();
        isFacingRight = true;
        facingDirection = 1;
        playerInput=GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        dashAction = playerInput.actions["Dash"];
        mydebug = moveAction.ReadValue<UnityEngine.Vector2>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        /*hasWallJumped = false;
        hasWallJumpFailed = false;*/
        unfinishedLedgeClimb = false;
        isStuck = false;
        playerCombat = GetComponent<PlayerCombat>();
        knockBackStartTime = Mathf.NegativeInfinity;
        isKnockedBack = false;

    }
    private void OnDisable()
    {
        moveAction.started -= OnMoveStarted;
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        jumpAction.performed -= OnJumpPerformed;
        jumpAction.canceled -= OnJumpCanceled;
        dashAction.performed -= OnDashPerformed;
    }
    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isWallSliding", isWallSliding);
    }
    void OnEnable()
    {
        jumpAction.performed += OnJumpPerformed;
        jumpAction.canceled += OnJumpCanceled;
        moveAction.started += OnMoveStarted;
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        dashAction.performed += OnDashPerformed;
    }

    public UnityEngine.Vector2 GetStartingPosition()
    {
        return startPosition;
    }
    public bool GetFacingRight()
    {
        return isFacingRight;
    }
    public int GetFacingDirection()
    {
        return facingDirection;
    }
    public void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (canJump&&!isWallSliding)
        {
            numJumpsLeft--;
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpForce);
        }
        /*else if(canJump&&isWallSliding&&movementVector.x==0)
         {
             UnityEngine.Debug.Log("Hopstarted");
             //StartCoroutine(DelayedWallJump());
             numJumpsLeft--;
             rb.AddForce(new UnityEngine.Vector2(wallHopForce * -facingDirection * wallHopDirection.x, 
                 wallHopDirection.y*wallHopForce), ForceMode2D.Impulse);
             Flip();
             isWallSliding = false;
         }
         else if((isWallSliding||isTouchingWall) && canJump && movementVector.x!=0)
         {
             UnityEngine.Debug.Log("walljumpstarted");
             rb.AddForce(new UnityEngine.Vector2(wallJumpForce * movementVector.x * wallJumpDirection.x,
                 wallJumpDirection.y * wallJumpForce), ForceMode2D.Impulse);
         }*/

        //This below section is a copy of the above section so I don't accidentally damage it
        /*This else if statement makes sure that the character can press the arrow key before jumping, and it 
        still registers as a wall jump. The Delayed Wall Jump method is a timer that makes sure the character 
        can press the arrow key after jumping (within a certain time limit), and it still registers as a wall jump*/
        else if (canJump && (isWallSliding || isTouchingWall) && movementVector.x != 0)
        {
            rb.AddForce(new UnityEngine.Vector2(wallJumpForce * movementVector.x * wallJumpDirection.x,
                wallJumpDirection.y * wallJumpForce), ForceMode2D.Impulse);
            /* @
              wallJumpTimer = wallJumpTimerSet;
            hasWallJumped = true;
            lastWallJumpDirection = facingDirection;*/
        }
        else if (canJump && (isWallSliding||isTouchingWall) && movementVector.x == 0)
        {
            StartCoroutine(DelayedWallJump());

            /*UnityEngine.Debug.Log("Hopstarted");
            //StartCoroutine(DelayedWallJump());
            numJumpsLeft--;
            rb.AddForce(new UnityEngine.Vector2(wallHopForce * -facingDirection * wallHopDirection.x,
                wallHopDirection.y * wallHopForce), ForceMode2D.Impulse);
            Flip();
            isWallSliding = false;*/
        }
        /*else if ((isWallSliding || isTouchingWall) && canJump && movementVector.x != 0)
        {
            UnityEngine.Debug.Log("walljumpstarted");
            rb.AddForce(new UnityEngine.Vector2(wallJumpForce * movementVector.x * wallJumpDirection.x,
                wallJumpDirection.y * wallJumpForce), ForceMode2D.Impulse);
        }*/
    }
    private IEnumerator DelayedWallJump()
    {
        yield return new WaitForSeconds(0.05f);
        if(isTouchingWall && isWallSliding&& movementVector.x == 0)
        {
            numJumpsLeft--;
            rb.AddForce(new UnityEngine.Vector2(wallHopForce * -facingDirection * wallHopDirection.x,
                wallHopDirection.y * wallHopForce), ForceMode2D.Impulse);
            Flip();
            isWallSliding = false;
        }
        else if((isTouchingWall || isWallSliding) && movementVector.x != 0)
        {
            rb.AddForce(new UnityEngine.Vector2(wallJumpForce * movementVector.x * wallJumpDirection.x,
                wallJumpDirection.y * wallJumpForce), ForceMode2D.Impulse);
            /* @
             wallJumpTimer = wallJumpTimerSet;
            hasWallJumped = true;
            lastWallJumpDirection = facingDirection;
            UnityEngine.Debug.Log("lastwalljumpdir: " + lastWallJumpDirection);*/
        }
    }
    public void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if(rb.velocity.y>0)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }
     public void OnMoveStarted(InputAction.CallbackContext context)
     {

     }
    public void OnMovePerformed(InputAction.CallbackContext context)
    {
        isWalking = true;
        movementVector.x = context.ReadValue<UnityEngine.Vector2>().x;

        CheckMovementDirection();
    }
    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        isWalking = false;
        movementVector.x = 0;
    }
    public void OnDashPerformed(InputAction.CallbackContext context)
    {
        CheckIfCanDash();
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallDist, whatIsWall) ||
            Physics2D.Raycast(wallCheck.position, -transform.right, wallDist, whatIsWall);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallDist, whatIsWall) ||
            Physics2D.Raycast(ledgeCheck.position, -transform.right, wallDist, whatIsWall);
    }
    /* @
       private void WatchJump()
    {
        if (hasWallJumped)
        {
            wallJumpTimer -= Time.deltaTime;
        }
        if(wallJumpTimer<=0)
        {
            UnityEngine.Debug.Log("if_one");
            if(facingDirection!=lastWallJumpDirection)
            {
                UnityEngine.Debug.Log("if_two");
                rb.velocity = new UnityEngine.Vector2(-rb.velocity.x, 0);
                hasWallJumped = false;
                hasWallJumpFailed = true;
            }      
            wallJumpTimer = wallJumpTimerSet;
        }
        if(hasWallJumpFailed&&isGrounded)
        {
            hasWallJumpFailed = false;
        }
    }*/
    private void CheckIfCanLedgeClimb()
    {
        if (isTouchingWall && !isTouchingLedge)
        {
            UnityEngine.Debug.Log("canledgeclimb");
            canLedgeClimb = true;
        }
        if(canLedgeClimb)
        {
            if(isFacingRight)
            {
                ledgePos1 = new UnityEngine.Vector2(Mathf.Floor(wallCheck.position.x+wallDist)-ledgeoffset1x, 
                    Mathf.Floor(wallCheck.position.y)-ledgeoffset1y);
                ledgePos2 = new UnityEngine.Vector2(Mathf.Floor(wallCheck.position.x + wallDist) + ledgeoffset2x,
                    Mathf.Ceil(wallCheck.position.y)+ledgeoffset2y);
                /*here, I am suspending the position of the character to the wall corner of the ledge
                 until the animation is complete */
                transform.position = ledgePos1;
                canMove = false;
                isStuck = true;
                canLedgeClimb = false;
                unfinishedLedgeClimb = true;
            }
            else if(!isFacingRight)
            {
                //CHANGE THESE VALUES!! I WILL SIMPLY TEST WITH RIGHT SIDE FIRST!!
                ledgePos1 = new UnityEngine.Vector2(Mathf.Ceil(wallCheck.position.x - wallDist) + ledgeoffset1x,
                    Mathf.Floor(wallCheck.position.y) - ledgeoffset1y);
                ledgePos2 = new UnityEngine.Vector2(Mathf.Ceil(wallCheck.position.x - wallDist) - ledgeoffset2x,
                    Mathf.Ceil(wallCheck.position.y) + ledgeoffset2y);
                transform.position = ledgePos1;
                canMove = false;
                canLedgeClimb = false;
                unfinishedLedgeClimb = true;
            }
            animator.SetBool("canLedgeClimb", true);
        }
    }

    public void FinishedLedgeClimb()
    {
        animator.SetBool("canLedgeClimb", false);
        transform.position = ledgePos2;
        canMove = true;
        isStuck = false;
        
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDirection *= -1;
        if (spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        
    }
    private void CheckMovementDirection()
    {
        if(isFacingRight&&movementVector.x<0)
        {
            Flip();
        }
        else if(!isFacingRight&& movementVector.x>0)
        {
            Flip();
        }
    }
    private void CheckIfCanJump()
    {
        if((isGrounded==true && rb.velocity.y<=0.01) || isWallSliding)
        {
            numJumpsLeft = numJumps;
        }
        if(numJumpsLeft > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGrounded&& rb.velocity.y<0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void CheckIfCanDash()
    {
        if(canMove)
        {
            isDashing = true;
            timeDashActivated = Time.time;
        }
    }
    private void AdjustWallSpeed()
    {
        if(isWallSliding && rb.velocity.y< -wallSlideSpeed)
        {
            rb.velocity= new UnityEngine.Vector2(movementVector.x * movementSpeed, -wallSlideSpeed);
        }
    }
    private float ToUnit(float x)
    {
        if(x>0)
        {
            return 1;
        }
        else if(x<0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public void KnockedBack(int dir)
    {
        isKnockedBack = true;
        knockBackStartTime = Time.time;
        knockBackDir = dir;
    }
    private void CheckIfKnockedBack()
    {
        if((Time.time > knockBackStartTime + knockBackTime) && isKnockedBack)
        {
            isKnockedBack = false;
        }
    }
    private void ApplyMovement()
    {
        if(isStuck)
        {
            UnityEngine.Debug.Log("cond1");
            rb.velocity = new UnityEngine.Vector2(0, 0);
            rb.gravityScale = 0;
            //transform.position = new UnityEngine.Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            UnityEngine.Debug.Log("cond2");
            rb.gravityScale = 4;
        }
        if (isGrounded && canMove && !isStuck && !isDashing && !isKnockedBack)
        {
            UnityEngine.Debug.Log("cond3");
            rb.velocity = new UnityEngine.Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        }
        else if(!isGrounded && movementVector.x!=0 && !isWallSliding && canMove && !isStuck && !isDashing && !isKnockedBack/*&& !hasWallJumpFailed*/)
        {
            /*This commented code makes it so you if you jump without any previous velocity, you can't move around too much
            if you try to move in the air */
            /*forceToAdd = new UnityEngine.Vector2(movementForceInAir * ToUnit(movementVector.x), 0);
            rb.AddForce(forceToAdd);   
            if(Mathf.Abs(rb.velocity.x) >movementSpeed)
            {
                rb.velocity = new UnityEngine.Vector2(movementVector.x * movementSpeed, rb.velocity.y);
            }*/

            rb.velocity = new UnityEngine.Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        }
        else if(!isGrounded && movementVector.x==0 && !isWallSliding && !isStuck && !isDashing && !isKnockedBack/*&& !hasWallJumpFailed*/)
        {
            UnityEngine.Debug.Log("cond4");
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if(isDashing && canMove && !isStuck)
        {
            UnityEngine.Debug.Log("cond5");
            if (Time.time < timeDashActivated + maxDashTime)
            {
                PlayerAfterImagePrefabPool.instance.retrievePlayerPrefabFromPool();
                rb.velocity = new UnityEngine.Vector2(facingDirection * dashSpeed, rb.velocity.y);
            }
            else
            {
                isDashing = false;
            }  
        }
        else if(isKnockedBack)
        {
            UnityEngine.Debug.Log("cond6knockback");
            UnityEngine.Debug.Log("regularKNOCKEDBACK");
             rb.velocity = new UnityEngine.Vector2(/*knockBackDir*/-facingDirection*knockback.x, knockback.y);
        }
        else
        {
            UnityEngine.Debug.Log("cond7");
            //  @ if (!hasWallJumpFailed)
            // {
            if (canMove && !isStuck)
           {
                rb.velocity = new UnityEngine.Vector2(rb.velocity.x, rb.velocity.y);
           }
                
          //  }
        }
    }
    private void Update()
    {
        UpdateAnimations();
    }
    private void FixedUpdate()
    {
        CheckSurroundings();
        CheckIfWallSliding();
        CheckIfCanJump();
        CheckIfCanLedgeClimb();
        CheckIfKnockedBack();
        ApplyMovement();
        //  @ WatchJump();
        AdjustWallSpeed();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new UnityEngine.Vector3(wallCheck.position.x + wallDist, wallCheck.position.y,
            wallCheck.position.z));
        Gizmos.DrawLine(ledgeCheck.position, new UnityEngine.Vector3(ledgeCheck.position.x + wallDist, ledgeCheck.position.y,
            ledgeCheck.position.z));
    }
}
