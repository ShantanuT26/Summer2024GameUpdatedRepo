using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool) : base(player, fsm, playerData, animBool)
    {
    }
    public override void BeginAction()
    {
        Debug.Log("entering jump state");
        base.BeginAction();
        player.SubtractNumJumpsLeft();
        player.SetVelocityY(playerData.jumpVelocity);
       // player.playerInputHandler.jumpInput = false;
    }

    public override void EndAction()
    {
        base.EndAction();
        Debug.Log("entering end jump state");
    }

    public override void LogicChecks()
    {
        base.LogicChecks();
        if(player.playerInputHandler.canJump == true)
        {
            player.SetVelocityY(playerData.jumpVelocity);
        }
        
    }

    public override void LogicUpdate()
    {
        //player.SetVelocityY(playerData.jumpVelocity);
        base.LogicUpdate();
        
        if (player.playerInputHandler.jumpInput == false)
        {
            isAbilityDone = true;
        }
        else if(Time.time > actionStartTime + playerData.maxJumpTime)
        {
            Debug.Log("Jumptimeover");
            isAbilityDone = true;
            player.playerInputHandler.SetCanJump(false);
        }
    }
}
