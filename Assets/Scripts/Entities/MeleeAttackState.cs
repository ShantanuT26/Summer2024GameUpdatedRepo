using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    private MeleeAttackStateData d_MeleeAttackState;
    private Transform meleeAttackPositionLeft;
    private Transform meleeAttackPositionRight;
    public MeleeAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, 
        MeleeAttackStateData d_MeleeAttackState, Transform meleeAttackPositionLeft, Transform meleeAttackPositionRight) : base(entity, fsm, animVarName)
    {
        this.d_MeleeAttackState = d_MeleeAttackState;
        this.meleeAttackPositionLeft = meleeAttackPositionLeft;
        this.meleeAttackPositionRight = meleeAttackPositionRight;
    }
    protected AttackDetails attackDetails;

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        attackDetails.position = entity.GetActiveMeleeAttackPosition().position;
        Debug.Log("activemeleeattackposition: " + entity.GetActiveMeleeAttackPosition());
        attackDetails.damage = d_MeleeAttackState.attackDamage;
        Debug.Log("enemyposition: " + attackDetails.position);
    }
    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
    }
    public override void BeginAction()
    {   
        base.BeginAction();
        entity.SetAnimBool(animVarName, true);
        attackDetails.position = entity.GetActiveMeleeAttackPosition().position;
        attackDetails.damage = d_MeleeAttackState.attackDamage;
    }

    public override void DoDamage()
    {
        base.DoDamage();
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(entity.GetActiveMeleeAttackPosition().position,
            d_MeleeAttackState.attackRadius, d_MeleeAttackState.whatIsPlayer);
        Debug.Log("Player is: ");
        foreach (Collider2D hitobj in hitObjects)
        {
            PlayerStats player = hitobj.gameObject.GetComponent<PlayerStats>();
            
            if(player!=null)
            {
                Debug.Log("Playernotnull");
                player.TakeDamage(attackDetails);
            }
        }
    }

    public override void EndAction()
    {
        base.EndAction();
        entity.SetAnimBool(animVarName, false);

    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    
}
