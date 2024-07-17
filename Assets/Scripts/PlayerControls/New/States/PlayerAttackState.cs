using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    public PlayerAttackState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, 
        string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public void SetWeapon(int x)
    {
        weapon = player.weaponLoadout[x];
    }
    public override void BeginAction()
    {
        base.BeginAction();
        //set attack bool to true for weapon animators and vanilla player animator
    }

    public override void EndAction()
    {
        base.EndAction();
        //set attack bool to false for weapon animators and vanilla player animator
    }
}
