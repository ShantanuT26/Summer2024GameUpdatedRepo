using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;
    protected bool jumpInput;
    public PlayerGroundedState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public override void BeginAction()
    {
        base.BeginAction();
        player.ResetNumJumpsLeft();
    }

    public override void EndAction()
    {
        base.EndAction();
    }

    public override void LogicChecks()
    {
        base.LogicChecks();
        xInput = player.playerInputHandler.movementInput.x;
        jumpInput = player.playerInputHandler.jumpInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        /*if (xInput == player.facingDirection * -1)
        {
            player.Flip();
        }*/
        if(jumpInput && player.numJumpsLeft > 0 && player.playerInputHandler.canJump) // also make sure that you have anough jumps (in case its set to 0)
        {
            //FinishJump();
            fsm.ChangeState(player.jumpState);
        }

    }

    /*public void FinishJump()
    {
        jumpInput = false;
    }*/
    public override void PhysicsChecks()
    {
        base.PhysicsChecks();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
