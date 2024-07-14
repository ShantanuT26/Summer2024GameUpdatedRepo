using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private float attackTime;
    private bool isTouchingGround;
    private Rigidbody2D rb;
    [SerializeField]private LayerMask isGround;
    [SerializeField]private LayerMask isPlayer;
    [SerializeField]private Transform damageCheck;
    [SerializeField] private float checksRadius;
    private AttackDetails attackDetails;
    private Entity entity;
    private SpriteRenderer sr;
    private int travelDirection;
    private string objPoolerTag = "Arrow";
    float angle=0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        rb.gravityScale = 0;
        attackDetails.position = damageCheck.position;
        attackDetails.damage = damage;
        isTouchingGround = false;
        speed = 0;
        travelDirection = 1;
        entity = GameObject.Find("Enemy2 (Archer)").GetComponent<Entity>();
    }


    private void Start()
    {
        //entity = GameObject.Find("Enemy2   (Archer)").GetComponent<Entity>();  
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed*travelDirection, rb.velocity.y);
        angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y)*Mathf.Rad2Deg;

        Collider2D collider = Physics2D.OverlapCircle(damageCheck.position, checksRadius, isPlayer);
        if (collider != null)
        {
            PlayerStats stats = collider.gameObject.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(attackDetails);
            }
            ObjectPooler.Instance.SendToQueue(this.gameObject, objPoolerTag);
        }
        isTouchingGround = Physics2D.OverlapCircle(damageCheck.position, checksRadius, isGround);
        if(isTouchingGround)
        {
            ObjectPooler.Instance.SendToQueue(this.gameObject, objPoolerTag);
        }
    }
    public void FireProjectile(float speed, float damage, float attackTime)
    {
        rb.gravityScale = 1;
        this.attackTime = attackTime;
        this.damage = damage;
        this.speed = speed;
        travelDirection = entity.GetFacingDirection();
    }
    void Update()
    {
        switch(entity.GetFacingDirection())
        {
            case 1: sr.flipX = false; break;
            case -1: sr.flipX = true; break;
        }
        attackDetails.position = damageCheck.position;
       // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damageCheck.position, checksRadius);
    }
}
