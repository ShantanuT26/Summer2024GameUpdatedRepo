using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private float xInput;
    public PlayerInAirState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public override void BeginAction()
    {
        base.BeginAction();
        Debug.Log("entering in air state");
    }

    public override void LogicChecks()
    {
        base.LogicChecks();
        xInput = player.playerInputHandler.movementInput.x;
        isGrounded = player.CheckGround();
        if (player.playerInputHandler.attacks[(int)PlayerAttacks.primaryAttack])
        {
            player.attackState.SetWeapon((int)PlayerAttacks.primaryAttack);
            fsm.ChangeState(player.attackState);
        }
        else if (player.playerInputHandler.attacks[(int)PlayerAttacks.secondaryAttack])
        {
            player.attackState.SetWeapon((int)PlayerAttacks.secondaryAttack);
            fsm.ChangeState(player.attackState);
        }
        else if (xInput != 0)
        {
            player.SetVelocity(xInput*playerData.runSpeed); // change runspeed to air speed in player data
        }
        if(isGrounded)
        {
           fsm.ChangeState(player.landState);
        }
        else if(player.playerInputHandler.jumpInput && player.numJumpsLeft > 0 && player.playerInputHandler.canJump) 
        {
            fsm.ChangeState(player.jumpState);
        }
    }
}
