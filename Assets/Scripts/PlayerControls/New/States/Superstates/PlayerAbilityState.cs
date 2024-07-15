using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        isGrounded = player.CheckGround();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone && !isGrounded)
        {
            UnityEngine.Debug.Log("changing to in air state");
            fsm.ChangeState(player.inAirState);
        }
        else if (isAbilityDone && isGrounded && player.GetVelocity().y < 0.1f)
        {
            UnityEngine.Debug.Log("changing to idle state");
            fsm.ChangeState(player.idleState);
        }
    }

    public override void PhysicsChecks()
    {
        base.PhysicsChecks();
        
    }

}
