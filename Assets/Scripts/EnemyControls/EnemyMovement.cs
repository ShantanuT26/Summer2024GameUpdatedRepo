using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private enum States
    {
        Walking,
        Knockback,
        Dead
    }
    [SerializeField] private float maxSpeed, wallCheckDist, groundCheckDist, maxHealth, knockBackMaxTime;
    [SerializeField] private Transform wallCheckRight, groundCheckRight, wallCheckLeft, groundCheckLeft;
    [SerializeField] private GameObject enemyAlive, hitParticle, deathChunkParticle, deathBloodParticle;
    [SerializeField] private Vector2 knockBackForce;

    private States currentState;

    private float walkingSpeed, currentHealth, knockBackStartTime;

    private int facingDirection, attackDirection; //for attackdirection, 1 is right, and -1 is left

    [SerializeField]private LayerMask whatIsGround;

    [SerializeField]private bool isTouchingWall, isTouchingGround;
    private bool isHurt, isWalking;

    private Animator anim;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D enemyRB;

    [SerializeField] private float touchDamage, touchDamageCoolDownTime;

    [SerializeField] private float touchBoxWidth, touchBoxHeight;

    private float lastTouchDamageTime;

    private Vector2 touchBoxBotLeft, touchboxTopRight;

    [SerializeField] private Transform touchedPlayerCheck;

    private bool touchedPlayer;

    [SerializeField] private LayerMask isPlayer;

    private float[] damageInfo = new float[2];
    private void Awake()
    {
        currentState = States.Walking;
        walkingSpeed = maxSpeed;
        enemyRB =enemyAlive.GetComponent<Rigidbody2D>();
        spriteRenderer = enemyAlive.GetComponent<SpriteRenderer>();
        anim = enemyAlive.GetComponent<Animator>();
        facingDirection = 1;
        currentHealth = maxHealth;
        wallCheckLeft.gameObject.SetActive(false);
        groundCheckLeft.gameObject.SetActive(false);
        isHurt = false;
        isWalking = true;
        touchBoxBotLeft = new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2));
        touchboxTopRight = new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2));
        touchedPlayer = false;
    }
    private void Update()
    {
        touchBoxBotLeft = new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2));
        touchboxTopRight = new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2));
        switch (currentState)
        {
            case States.Walking:
                UpdateWalking();
                break;
            case States.Knockback:
                UpdateKnockback(); 
                break;
            case States.Dead:
                UpdateDeath();
                break;
        }
    }
    private void changeState(States state)
    {
        switch(currentState)
        {
            case States.Walking:
                FinishWalking();
                break;
            case States.Knockback:
                FinishKnockback(); 
                break;
            case States.Dead:
                FinishDeath();
                break;
        }
        switch(state)
        {
            case States.Walking:
                StartWalking();
                break;
            case States.Knockback:
                StartKnockback();
                break;
            case States.Dead:
                StartDeath();
                break;
        }
        currentState = state;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheckRight.position, new Vector3(wallCheckRight.position.x + wallCheckDist, wallCheckRight.position.y,
            wallCheckRight.position.z));
        Gizmos.DrawLine(groundCheckRight.position, new Vector3(groundCheckRight.position.x,
            groundCheckRight.position.y-groundCheckDist, groundCheckRight.position.z));
        Gizmos.DrawLine(wallCheckLeft.position, new Vector3(wallCheckLeft.position.x - wallCheckDist, wallCheckLeft.position.y,
            wallCheckLeft.position.z));
        Gizmos.DrawLine(groundCheckLeft.position, new Vector3(groundCheckLeft.position.x,
            groundCheckLeft.position.y - groundCheckDist, groundCheckLeft.position.z));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)));
    }
    private void CheckTouchDamage()
    {
        Collider2D hit = Physics2D.OverlapArea(touchBoxBotLeft, touchboxTopRight, isPlayer);
        if (hit != null && Time.time > lastTouchDamageTime + touchDamageCoolDownTime)
        {
            lastTouchDamageTime = Time.time;
            Debug.Log("HIT!!!");
            damageInfo[0] = touchDamage;
            damageInfo[1] = transform.position.x;
            //hit.transform.parent.SendMessage("Damage", damageInfo);
            hit.SendMessage("Damage", damageInfo);
        }
    }
    private void Flip()
    {
        facingDirection *= -1;
        if(spriteRenderer.flipX==true)
        {
            spriteRenderer.flipX = false;
            wallCheckLeft.gameObject.SetActive(false);
            groundCheckLeft.gameObject.SetActive(false);
            wallCheckRight.gameObject.SetActive(true);
            groundCheckRight.gameObject.SetActive(true);
        }           
        else
        {
            spriteRenderer.flipX = true;
            wallCheckRight.gameObject.SetActive(false);
            groundCheckRight.gameObject.SetActive(false);
            wallCheckLeft.gameObject.SetActive(true);
            groundCheckLeft.gameObject.SetActive(true);
        }
    }
    private void SetAnimBools()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isHurt", isHurt);
    }
    private void Damage(float[]damageInfo)
    {
        currentHealth-= damageInfo[0];
        Instantiate(hitParticle, enemyAlive.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        if (damageInfo[1]>enemyAlive.transform.position.x)
        {
            attackDirection = 1;
        }
        else
        {
            attackDirection = -1;
        }
        if(currentHealth>0)
        {
            changeState(States.Knockback);
        }
        else if(currentHealth<=0)
        {
            changeState(States.Dead);
        }
    }
    private void StartWalking()
    {
        isWalking = true;
    }
    private void UpdateWalking()
    {
        CheckTouchDamage();
        if (wallCheckRight.gameObject.activeSelf)
        {
            Debug.Log("Right on!");
            isTouchingWall = Physics2D.Raycast(wallCheckRight.transform.position, transform.right, wallCheckDist, whatIsGround);
            isTouchingGround = Physics2D.Raycast(groundCheckRight.transform.position, -transform.up, groundCheckDist, whatIsGround);
        }
        else if(wallCheckLeft.gameObject.activeSelf)
        {
            Debug.Log("Left on!");
            isTouchingWall = Physics2D.Raycast(wallCheckLeft.transform.position, -transform.right, wallCheckDist, whatIsGround);
            isTouchingGround = Physics2D.Raycast(groundCheckLeft.transform.position, -transform.up, groundCheckDist, whatIsGround);
        }
        if (/*isTouchingGround && */isTouchingWall)
        {
            Flip();
        }
        if (!isTouchingGround && !isTouchingWall)   
        {
            Flip();
        }
        enemyRB.velocity = new UnityEngine.Vector2(walkingSpeed*facingDirection, enemyRB.velocity.y);
    }
    private void FinishWalking()
    {
        enemyRB.velocity = new UnityEngine.Vector2(0f, enemyRB.velocity.y);
        isWalking = false;
    }
    private void StartKnockback()
    {
        //enemyRB.velocity = new UnityEngine.Vector2(knockBackForce.x * -attackDirection, knockBackForce.y);
        isHurt = true;
        knockBackStartTime = Time.time;
        enemyRB.AddForce(new UnityEngine.Vector2(0f, knockBackForce.y), ForceMode2D.Impulse);
    }
    private void UpdateKnockback()
    {
        if (Time.time > knockBackStartTime + knockBackMaxTime)
        {
            changeState(States.Walking);
        }
        else
        {
            enemyRB.velocity = new UnityEngine.Vector2(knockBackForce.x * -attackDirection, enemyRB.velocity.y);
        }
    }
    private void FinishKnockback()
    {
        isHurt = false;
        //enemyRB.velocity = new UnityEngine.Vector2(0, 0);
    }
    private void StartDeath()
    {
        Instantiate(deathChunkParticle, enemyAlive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, enemyAlive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateDeath()
    {

    }
    private void FinishDeath()
    {

    }


}
