using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool firstAttack;
    private bool attack1;
    private bool canAttack;
    private bool isAttacking;
    [SerializeField] private Transform attack1HitBox;

    [SerializeField]private float attack1Radius;
    private float attack1Damage = 20f;

    private float[] damageInfo;

    private Animator anim;

    [SerializeField] private LayerMask canBeAttacked;

    //IMPORTANT KEEP
    private PlayerController playerController;

    public InputAction mouseClick;

    private PlayerInput playerInput;

    private AttackDetails attackDetails;

    private void Awake()
    {
        //attack1 is the attack itself
        //firstattack is the subset of the attack
        playerInput = GetComponent<PlayerInput>();
        mouseClick = playerInput.actions["Click"];
        anim = GetComponent<Animator>();
        firstAttack = true;
        attack1 = false;
        canAttack = true;
        //IMPORTANT KEEP
        playerController = GetComponent<PlayerController>();
        damageInfo = new float[2];
        attackDetails.damage = attack1Damage;
    }
    private void OnEnable()
    {
        mouseClick.performed += StartAttack1;
    }
    private void OnDisable()
    {
        mouseClick.canceled -= StartAttack1;
    }
    public void StartAttack1(InputAction.CallbackContext context)
    {
        UnityEngine.Debug.Log("Attacking Right Now!!!");
        //I called this method while subscribing to click.performed from the InputHandler script
        if(!isAttacking && canAttack)
        {
            UnityEngine.Debug.Log("ATTACK1TRUE");
            attack1 = true;
            isAttacking = true;
        }
    }
    private void SetAnimBools()
    {
        anim.SetBool("attack1", attack1);
        anim.SetBool("firstAttack", firstAttack);
        anim.SetBool("isAttacking", isAttacking);
    }
    public void changeHitBoxPosition(int facingDirection)
    {
        //IMPORTANT KEEP
        
       if(facingDirection==1)
        {
            attack1HitBox.transform.localPosition = new Vector2(1.28f, -0.13f);
        }
       else if(facingDirection == -1)
        {
            attack1HitBox.transform.localPosition = new Vector2(-1.28f, -0.13f);
        }
    }

    private void OnAttack1End()
    {
        attack1 = false;
        firstAttack = !firstAttack;
        isAttacking = false;
    }
    public void CheckAttackHitbox()
    {
       UnityEngine.Debug.Log("CHECKING HITBOX");
        attackDetails.position = gameObject.transform.position;
        Collider2D[] attackedObjects = Physics2D.OverlapCircleAll(attack1HitBox.position, attack1Radius, canBeAttacked);

        foreach(Collider2D collider in attackedObjects)
        {
            UnityEngine.Debug.Log("ONE ENTITY");
            Entity entity = collider.transform.parent.GetComponent<Entity>();
            if(entity!=null)
            {
                entity.TakeDamage(attackDetails);
            }
        }
    }
    private void OnDrawGizmos()
    {
        UnityEngine.Debug.Log("Combat Gizmos");
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }
    private void Update()
    {
        UnityEngine.Debug.Log("PLAYERCOMBATUPDATE");
        SetAnimBools();
        //changeHitBoxPosition();
    }
}
