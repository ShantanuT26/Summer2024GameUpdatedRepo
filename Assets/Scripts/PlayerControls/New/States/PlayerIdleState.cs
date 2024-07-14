using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public override void BeginAction()
    {
        base.BeginAction();
        Debug.Log("Idlestatebegun");
        player.SetVelocity(0f);

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
    }

    public override void PhysicsChecks()
    {
        base.PhysicsChecks();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(xInput!=0)
        {
            Debug.Log("xinput not 0");
            fsm.ChangeState(player.runState);
        }
    }
}
