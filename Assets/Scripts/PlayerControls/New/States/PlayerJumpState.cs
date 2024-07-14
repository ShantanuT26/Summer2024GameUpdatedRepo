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
        base.BeginAction();
        player.SubtractNumJumpsLeft();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
       // player.playerInputHandler.jumpInput = false;
    }
}
