using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected float actionStartTime;
    protected Entity entity;
    protected FiniteStateMachine fsm;
    protected string animVarName;
    protected bool isInPlayerMinDist;
    protected bool isInPlayerMaxDist;
    protected bool isInPlayerMeleeAttackDist;
    protected bool isTouchingGround;
    protected bool isTouchingWall;

    public State(Entity entity, FiniteStateMachine fsm, string animVarName)
    {
        this.entity = entity;
        this.fsm = fsm;
        this.animVarName = animVarName;
    }

    public virtual void BeginAction()
    {
        actionStartTime = Time.time;
        Checks();
    }
    public virtual void EndAction()
    {

    }
    protected virtual void Checks()
    {
        isInPlayerMaxDist = entity.CheckPlayerMaxDist();
        isInPlayerMinDist = entity.CheckPlayerMinDist();
        isTouchingGround = entity.CheckGround();
        isTouchingWall = entity.CheckWall();
        isInPlayerMeleeAttackDist = entity.CheckMeleeAttackDist();
    }
    public virtual void ActionLogicUpdate()
    {

    }
    public virtual void ActionPhysicsUpdate()
    {
        Checks();
    }
}
