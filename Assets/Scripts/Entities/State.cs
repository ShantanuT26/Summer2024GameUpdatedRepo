using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected float actionStartTime;
    protected Entity entity;
    protected FiniteStateMachine fsm;
    protected string animVarName;

    public State(Entity entity, FiniteStateMachine fsm, string animVarName)
    {
        this.entity = entity;
        this.fsm = fsm;
        this.animVarName = animVarName;
    }

    public virtual void BeginAction()
    {
        actionStartTime = Time.time;
    }
    public virtual void EndAction()
    {

    }
    public virtual void ActionLogicUpdate()
    {

    }
    public virtual void ActionPhysicsUpdate()
    {

    }
}
