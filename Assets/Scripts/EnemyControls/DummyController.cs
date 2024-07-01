using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField]private float maxHealth=60f;
    private float currentHealth;
    [SerializeField]private float damageXVelocity;
    [SerializeField] private float damageMovementTime;
    [SerializeField] private float deathXForce;
    [SerializeField] private float deathTorque;
    private float damageMovementStartTime;

    private bool isAttacked;

    private Rigidbody2D aliveRB;
    private Rigidbody2D topDeadRB;
    private Rigidbody2D bottomDeadRB;

    private GameObject alive;
    private GameObject topDead;
    private GameObject bottomDead;

    private Animator anim;

    private PlayerController playerController;

    [SerializeField] private GameObject player;

    [SerializeField]private HitParticle hitParticle;
    private void Awake()
    {
        currentHealth = maxHealth;
        alive = transform.Find("Alive").gameObject;
        topDead = transform.Find("Dead_Top").gameObject;
        bottomDead = transform.Find("Dead_Bottom").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        topDeadRB = topDead.GetComponent<Rigidbody2D>();
        bottomDeadRB = bottomDead.GetComponent<Rigidbody2D>();
        topDead.SetActive(false);
        bottomDead.SetActive(false);
        anim = alive.GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }
    private void Damage(float[] damageInfo)
    {
        currentHealth-= damageInfo[0];
        damageMovementStartTime = Time.time;
        isAttacked = true;
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
    private void Die()
    {
        topDead.SetActive(true);
        bottomDead.SetActive(true);
        bottomDead.transform.position = alive.transform.position;
        topDead.transform.position = alive.transform.position;
        alive.SetActive(false);
        topDeadRB.AddForce(new UnityEngine.Vector2(playerController.GetFacingDirection() * deathXForce, 0), ForceMode2D.Impulse);
        isAttacked = false;
        topDeadRB.AddTorque(-playerController.GetFacingDirection() * deathTorque);
        bottomDeadRB.AddForce(new UnityEngine.Vector2(playerController.GetFacingDirection() * deathXForce, 0));
    }
    private void CheckAttack()
    {
        if(currentHealth>0 && isAttacked)
        {
            if(Time.time<damageMovementStartTime+damageMovementTime)
            {
                aliveRB.velocity = new UnityEngine.Vector2(playerController.GetFacingDirection() * damageXVelocity, 
                    aliveRB.velocity.y);
                anim.SetTrigger("Damage");
            }
            else
            {
                aliveRB.velocity = new UnityEngine.Vector2(0, aliveRB.velocity.y);
                isAttacked = false;
            }
        }
        else if(currentHealth<=0 && isAttacked)
        {
            Die();
        }
    }
    private void SetAnimBools()
    {
        anim.SetBool("rightAttack", !playerController.GetFacingRight());
    }
    private void Update()
    {
        CheckAttack();
    }
}
