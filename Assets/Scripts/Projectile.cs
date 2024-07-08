using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private float attackTime;
    private Rigidbody2D rb;
    private Vector2 currPos;
    private LayerMask isGround;
    private LayerMask isPlayer;
    [SerializeField]private Transform damageCheck;
    [SerializeField] private float checksRadius;
    private AttackDetails attackDetails;
    private Entity entity;
    float angle=0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        currPos = transform.position;
        attackDetails.damage = damage;
        speed = 0;
    }


    private void Start()
    {
        entity = GameObject.Find("Enemy2 (Archer)").GetComponent<Entity>();  
    }
    private void FixedUpdate()
    {
        Debug.Log("Projectile speed: " + this.speed);
        rb.velocity = new Vector2(speed*entity.GetFacingDirection(), rb.velocity.y);
        Debug.Log("Projectile velocity: " + rb.velocity.x);
        angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y)*Mathf.Rad2Deg;

       /* Collider2D collider = Physics2D.OverlapCircle(damageCheck.position, checksRadius, isPlayer);
        PlayerStats stats = collider.gameObject.GetComponent<PlayerStats>();
        if(stats!= null)
        {
            stats.TakeDamage(attackDetails);
        }*/
        
    }
    public void FireProjectile(float speed, float damage, float attackTime)
    {
        Debug.Log("Projectilefired");
        this.attackTime = attackTime;
        this.damage = damage;
        this.speed = speed;
        Debug.Log("proj speed: " + this.speed);
    }
    void Update()
    {
        Debug.Log("Projectile speed: " + this.speed);
        attackDetails.position = transform.position;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damageCheck.position, checksRadius);
    }
}
