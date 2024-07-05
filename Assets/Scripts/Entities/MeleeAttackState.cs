using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    private MeleeAttackStateData d_MeleeAttackState;
    private Transform meleeAttackPosition;
    public MeleeAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, 
        MeleeAttackStateData d_MeleeAttackState, Transform meleeAttackPosition) : base(entity, fsm, animVarName)
    {
        this.d_MeleeAttackState = d_MeleeAttackState;
        this.meleeAttackPosition = meleeAttackPosition;
    }
    protected AttackDetails attackDetails;

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        attackDetails.position = entity.GetMeleeAttackPosition().position;
        Debug.Log("activemeleeattackposition: " + entity.GetMeleeAttackPosition());
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
        attackDetails.position = entity.GetMeleeAttackPosition().position;
        attackDetails.damage = d_MeleeAttackState.attackDamage;
    }

    public override void DoDamage()
    {
        base.DoDamage();
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(entity.GetMeleeAttackPosition().position,
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
