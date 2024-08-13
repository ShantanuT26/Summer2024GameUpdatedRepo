using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public override void BeginAction()
    {
        base.BeginAction();
        Debug.Log("entering run state");
    }

    public override void EndAction()
    {
        base.EndAction();
    }

    public override void LogicChecks()
    {
        base.LogicChecks();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(xInput==0)
        {
            fsm.ChangeState(player.idleState);
        }
    }

    public override void PhysicsChecks()
    {
        base.PhysicsChecks();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(xInput!=0 && !player.isCompletelyFrozen)
        {
            player.SetVelocity(xInput * playerData.runSpeed);
        }
    }
}
