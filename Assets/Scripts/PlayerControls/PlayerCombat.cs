using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool firstAttack;
    private bool attack1;
    private bool canAttack;
    private bool isAttacking;
    [SerializeField]private Transform attack1HitBox;

    [SerializeField]private float attack1Radius;
    private float attack1Damage = 20f;

    private float[] damageInfo;

    private Animator anim;

    [SerializeField] private LayerMask canBeAttacked;

    private PlayerController playerController;

    public InputAction mouseClick;

    private PlayerInput playerInput;

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
        playerController = GetComponent<PlayerController>();
        damageInfo = new float[2];
    }
    private void OnEnable()
    {
        mouseClick.performed += StartAttack1;
    }
    private void OnDisable()
    {
        mouseClick.performed -= StartAttack1;
    }
    public void StartAttack1(InputAction.CallbackContext context)
    {
        //I called this method while subscribing to click.performed from the InputHandler script
        if(!isAttacking && canAttack)
        {
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
    private void changeHitBoxPosition()
    {
       if(playerController.GetFacingDirection()==1)
        {
            attack1HitBox.transform.localPosition = new UnityEngine.Vector2(1.28f, -0.13f);
        }
       else if(playerController.GetFacingDirection() == -1)
        {
            attack1HitBox.transform.localPosition = new UnityEngine.Vector2(-1.28f, -0.13f);
        }
    }
    private void OnAttack1End()
    {
        attack1 = false;
        firstAttack = !firstAttack;
        isAttacking = false;
    }
    private void CheckAttackHitbox()
    {
        Collider2D[] attackedObjects = Physics2D.OverlapCircleAll(attack1HitBox.position, attack1Radius, canBeAttacked);

        foreach(Collider2D collider in attackedObjects)
        {
            damageInfo[0] = attack1Damage;
            damageInfo[1] = transform.position.x;
            collider.transform.parent.SendMessage("Damage", damageInfo);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }
    private void Update()
    {
        SetAnimBools();
        changeHitBoxPosition();
    }
}
