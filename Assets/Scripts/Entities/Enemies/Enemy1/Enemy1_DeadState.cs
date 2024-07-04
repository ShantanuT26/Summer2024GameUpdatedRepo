using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy1_DeadState : DeadState
{
    public Enemy1_DeadState(Entity entity, FiniteStateMachine fsm, string animVarName, DeadStateData d_DeadState, Enemy1 enemy1) : base(entity, fsm, animVarName, d_DeadState, enemy1)
    {
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
        entity.InstantiateDeathChunks(d_DeadState.deathBloodParticles);
        entity.InstantiateDeathChunks(d_DeadState.deathChunkParticles);
        entity.DestroyEntity();
        /*entity.Instantiate(d_DeadState.deathBloodParticles, entity.gameObject.transform.GetChild(0).gameObject.transform.position,
           entity.gameObject.transform.GetChild(0).gameObject.transform.rotation);*/
       // Destroy(entity.gameObject);

    }

    public override void EndAction()
    {
        base.EndAction();
    }
}
