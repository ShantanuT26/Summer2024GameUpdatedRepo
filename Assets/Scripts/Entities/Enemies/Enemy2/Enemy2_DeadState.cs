using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_DeadState : DeadState
{
    private Enemy2 enemy2;
    public Enemy2_DeadState(Entity entity, FiniteStateMachine fsm, string animVarName, DeadStateData d_DeadState, Enemy2 enemy2) : base(entity, fsm, animVarName, d_DeadState)
    {
        this.enemy2 = enemy2;
    }

    public override void ActionLogicUpdate()
    {
        base.ActionLogicUpdate();
        entity.InstantiateDeathChunks(d_DeadState.deathChunkParticles);
        entity.InstantiateDeathChunks(d_DeadState.deathBloodParticles);
        entity.DestroyEntity();
    }

    public override void ActionPhysicsUpdate()
    {
        base.ActionPhysicsUpdate();
    }

    public override void BeginAction()
    {
        base.BeginAction();
    }

    public override void EndAction()
    {
        base.EndAction();
    }
}
