using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool isGrounded;
    public PlayerAbilityState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {

    }

    public override void BeginAction()
    {
        base.BeginAction();
        isAbilityDone = false;
    }

    public override void LogicChecks()
    {
        base.LogicChecks();
        if(isAbilityDone && !isGrounded)
        {
            fsm.ChangeState(player.inAirState);
        }
        else if(isAbilityDone && isGrounded)
        {
            fsm.ChangeState(player.idleState);
        }
    }

    public override void PhysicsChecks()
    {
        base.PhysicsChecks();
        isGrounded = player.CheckGround();
    }

}
