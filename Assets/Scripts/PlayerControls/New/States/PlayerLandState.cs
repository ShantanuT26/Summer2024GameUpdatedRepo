using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public override void BeginAction()
    {
        base.BeginAction();
        Debug.Log("entering land state");
    }

    public override void LogicChecks()
    {
        base.LogicChecks();
        player.SuspendHorizontalMovement();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(player.animationFinished)
        {
            player.SetAnimationFinished(false);
            if(xInput == 0)
            {
                fsm.ChangeState(player.idleState);
            }
            else if(xInput!=0)
            {
                fsm.ChangeState(player.runState);
            }
        }
    }
}
