using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity : MonoBehaviour
{
    protected State currentState;
    protected FiniteStateMachine fsm;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected int facingDirection = 1;
    protected Animator anim;

    [SerializeField] protected Transform wallCheckRight;
    [SerializeField] protected Transform wallCheckLeft;
    [SerializeField] protected Transform groundCheckRight;
    [SerializeField] protected Transform groundCheckLeft;
    [SerializeField] protected Transform playerDistCheck;
    [SerializeField] protected Transform meleeAttackPositionRight;
    [SerializeField] protected Transform meleeAttackPositionLeft;
    protected GameObject aliveGO;

    protected float currentHealth;
    protected float lastTimeKnockedBack;

    [SerializeField]protected EntityData d_Entity;

    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;

    [SerializeField] protected bool debugTouchingGround;
    [SerializeField] protected bool debugTouchingWall;
    protected bool roadToStun;
    protected bool isDead;
    protected bool movementDeactivated;

    [SerializeField] protected IdleStateData d_IdleState;
    [SerializeField] protected WalkingStateData d_WalkingState;
    [SerializeField] protected PlayerDetectedStateData d_PlayerDetectedState;
    [SerializeField] protected ChargeStateData d_ChargeState;
    [SerializeField] protected LookForPlayerStateData d_LookForPlayerState;
    [SerializeField] protected MeleeAttackStateData d_MeleeAttackState;
    [SerializeField] protected StunStateData d_StunState;
    [SerializeField] protected DeadStateData d_DeadState;

    public AttackEventReceiver attackEventReceiver;

    protected delegate void GetAttacked(AttackDetails attackDetails);
    protected event GetAttacked damageEvent;

    protected int hitsUntilStunned;
    protected int knockBackDirection;

    public bool flipNow { get; private set; } = false;


    public virtual void Start()
    {
        Debug.Log("Startcalled");
        
        fsm = new FiniteStateMachine();
    }
    protected virtual void Awake()
    {
        aliveGO = transform.Find("Enemy_Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        spriteRenderer = aliveGO.GetComponent<SpriteRenderer>();
        anim = aliveGO.GetComponent<Animator>();
        wallCheckRight.gameObject.SetActive(true);
        groundCheckRight.gameObject.SetActive(true);
        wallCheckLeft.gameObject.SetActive(false);
        groundCheckLeft.gameObject.SetActive(false);
        attackEventReceiver = aliveGO.GetComponent<AttackEventReceiver>();
        meleeAttackPositionRight.gameObject.SetActive(true);
        meleeAttackPositionLeft.gameObject.SetActive(false);
        currentHealth = d_Entity.health;
        roadToStun = false;
        hitsUntilStunned = d_Entity.stunResistance;
        isDead = false;
        movementDeactivated = false;
    }
    protected virtual void Update()
    {
        if(fsm.GetCurrentState()==null)
        {
            Debug.Log("Houston, we have a problem");
        }
        else
        {
            fsm.GetCurrentState().ActionLogicUpdate();
        }
        if(roadToStun)
        {
            if(Time.time > lastTimeKnockedBack + d_Entity.stunRecovery)
            {
                hitsUntilStunned = d_Entity.stunResistance;
            }
        }
        
    }
    protected virtual void FixedUpdate()
    {
       /* wallCheckRight.transform.position = aliveGO.transform.position;
        groundCheckRight.transform.position = aliveGO.transform.position;
        wallCheckLeft.transform.position = aliveGO.transform.position;
        groundCheckLeft.transform.position = aliveGO.transform.position;*/
        fsm.GetCurrentState().ActionPhysicsUpdate();
        CheckMovementDeactivated();
        Debug.Log("currentstate: " + fsm.GetCurrentState());
        Debug.Log("entityvelocity: " + rb.velocity.x);
    }
    protected virtual void OnEnable()
    {
        damageEvent += DecreaseHealth;
    }
    protected virtual void OnDisable()
    {
        damageEvent -= DecreaseHealth;
    }
    protected void DecreaseHealth(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damage;
        roadToStun = true;
        lastTimeKnockedBack = Time.time;
        hitsUntilStunned--;
        if(currentHealth<=0)
        {
            Die();
        }
        else if(hitsUntilStunned==0)
        {
            GetStunned();
        }
        else
        {
            Debug.Log("bouttaknockbackenemy, knockbackforcex is: " + d_Entity.knockBackForce.x);
            if(attackDetails.position.x> transform.GetChild(0).transform.position.x)
            {
                knockBackDirection = -1;
            }
            else
            {
                knockBackDirection = 1;
            }
            Debug.Log("positiondebugenemy: " + transform.position.x);
            Debug.Log("positiondebugplayer: " + attackDetails.position.x);

            rb.AddForce(new Vector2(d_Entity.knockBackForce.x * knockBackDirection, d_Entity.knockBackForce.y));
        }
    }
    public void CheckMovementDeactivated()
    {
        if(movementDeactivated)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
    public void SetMovementDeactivated(bool x)
    {
        movementDeactivated = x;
    }

    public void TakeDamage(AttackDetails attackDetails)
    {
        damageEvent.Invoke(attackDetails);
    }
    protected virtual void Die()
    {
        roadToStun = false;
        isDead = true;
    }
    public void InstantiateDeathChunks(GameObject x)
    {
        Instantiate(x, gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.rotation);
    }
    public void DestroyEntity()
    {
        Destroy(this.gameObject);
    }
    protected virtual void GetStunned()
    {
        roadToStun = false;
    }
    public void SetFlipNow(bool x)
    {
        flipNow = x;
    }
    public Transform GetActiveWallCheck()
    {
        if(wallCheckRight.gameObject.activeSelf==false)
        {
            return wallCheckLeft;
        }
        else
        {
            return wallCheckRight;
        }
    }
    public Transform GetActiveGroundCheck()
    {
        if (groundCheckRight.gameObject.activeSelf == false)
        {
            return groundCheckLeft;
        }
        else
        {
            return groundCheckRight;
        }
    }
    public Transform GetActiveMeleeAttackPosition()
    {
        if(meleeAttackPositionRight.gameObject.activeSelf==false)
        {
            return meleeAttackPositionLeft;
        }
        else
        {
            return meleeAttackPositionRight;
        }
    }
    public void SetAnimBool(string varName, bool myBool)
    {
        Debug.Log("animboolset");
        anim.SetBool(varName, myBool);
        Debug.Log("animVar: " + varName);
        Debug.Log("animBool: " + myBool);
    }
    public void SetVelocity(float vel)
    {
        //rb.velocity.Set(vel, rb.velocity.y)
        rb.velocity = new Vector2(facingDirection*vel, rb.velocity.y);
    }
    public float GetVelocity()
    {
        return rb.velocity.x;
    }
    public void Flip()
    {
        if (spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
            wallCheckLeft.gameObject.SetActive(false);
            groundCheckLeft.gameObject.SetActive(false);
            wallCheckRight.gameObject.SetActive(true);
            groundCheckRight.gameObject.SetActive(true);
            meleeAttackPositionRight.gameObject.SetActive(true);
            meleeAttackPositionLeft.gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.flipX = true;
            wallCheckRight.gameObject.SetActive(false);
            groundCheckRight.gameObject.SetActive(false);
            wallCheckLeft.gameObject.SetActive(true);
            groundCheckLeft.gameObject.SetActive(true);
            meleeAttackPositionRight.gameObject.SetActive(false);
            meleeAttackPositionLeft.gameObject.SetActive(true);
        }
        facingDirection *= -1;
    }
    public bool CheckWall()
    {
        bool x = false;
        if(GetActiveWallCheck() == wallCheckRight)
        {
            x = Physics2D.Raycast(wallCheckRight.transform.position, transform.right, d_Entity.wallCheckDist, whatIsGround);
        }
        else if (GetActiveWallCheck() == wallCheckLeft)
        {
            x = Physics2D.Raycast(wallCheckLeft.transform.position, -transform.right, d_Entity.wallCheckDist, whatIsGround);
        }
        debugTouchingWall = x;
        Debug.Log("ActiveWallCheck: " + GetActiveWallCheck());
        return x;
    }
    public bool CheckGround()
    {
        bool x = false;
        if (GetActiveGroundCheck() == groundCheckRight)
        {
            x = Physics2D.Raycast(groundCheckRight.transform.position, -transform.up, d_Entity.groundCheckDist, whatIsGround);
            Debug.Log("groundcheck1: " + x);
        }
        else if (GetActiveGroundCheck() == groundCheckLeft)
        {
            x = Physics2D.Raycast(groundCheckLeft.transform.position, -transform.up, d_Entity.groundCheckDist, whatIsGround);
            Debug.Log("GroundCheck2: " + x);
        }
        debugTouchingGround = x;
        Debug.Log("ActiveGroundCheck: " + GetActiveGroundCheck());
        
        return x;
    }
    public bool CheckPlayerMinDist()
    {
        return Physics2D.Raycast(playerDistCheck.position, transform.right * facingDirection, d_Entity.playerDetectedMinDist, whatIsPlayer);
    }
    public bool CheckPlayerMaxDist()
    {
        return Physics2D.Raycast(playerDistCheck.position, transform.right * facingDirection, d_Entity.playerDetectedMaxDist, whatIsPlayer);
    }
    public bool CheckMeleeAttackDist()
    {
        return Physics2D.Raycast(playerDistCheck.position, transform.right * facingDirection, d_Entity.meleeAttackDist, whatIsPlayer);
    }
    public void OnDrawGizmos()
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
        Gizmos.DrawWireSphere(new Vector3(playerDistCheck.position.x + d_Entity.playerDetectedMaxDist, playerDistCheck.position.y,
            playerDistCheck.position.z), 0.5f);
        Gizmos.DrawWireSphere(new Vector3(playerDistCheck.position.x + d_Entity.playerDetectedMinDist, playerDistCheck.position.y,
            playerDistCheck.position.z), 0.5f);
        Gizmos.DrawWireSphere(new Vector3(playerDistCheck.position.x + d_Entity.meleeAttackDist, playerDistCheck.position.y,
            playerDistCheck.position.z), 0.5f);
        Gizmos.DrawWireSphere(meleeAttackPositionRight.position, d_Entity.attackRadius);
        Gizmos.DrawWireSphere(meleeAttackPositionLeft.position, d_Entity.attackRadius);
    }
}
