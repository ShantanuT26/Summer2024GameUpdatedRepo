using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected RangedAttackStateData d_RangedAttackState;
    protected GameObject projectile;
    protected Projectile projectileScript;
    public RangedAttackState(Entity entity, FiniteStateMachine fsm, string animVarName, RangedAttackStateData d_RangedAttackState, 
        GameObject projectile) : base(entity, fsm, animVarName)
    {
        this.d_RangedAttackState = d_RangedAttackState;
        this.projectile = projectile;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
    }

    public override void BeginAction()
    {
        base.BeginAction();
        GameObject.Instantiate(projectile, entity.gameObject.transform.GetChild(0).transform.position, Quaternion.Euler(0, 0, 0));
        projectileScript = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>();
        projectileScript.FireProjectile(d_RangedAttackState.attackSpeed, d_RangedAttackState.attackDamage, d_RangedAttackState.attackTime);
    }

    public override void EndAction()
    {
        base.EndAction();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
