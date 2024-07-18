using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private bool endAttack;
    public PlayerAttackState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, 
        string animBool) : base(player, fsm, playerData, animBool)
    {
    }

    public void SetWeapon(int x)
    {
        weapon = player.weaponLoadout[x];
       // weapon.SetPlayer(player);
    }
    public override void BeginAction()
    {
        base.BeginAction();
        endAttack = false;
        if(weapon!=null)
        {
            weapon.SetAnimBools(animBool, true);
        }
        //set attack bool to true for weapon animators and vanilla player animator
    }
    public void FinishAnimation()
    {
        endAttack = true;
        if (weapon != null)
        {
            weapon.SetAnimBools(animBool, false);
        }
    }
    public override void EndAction()
    {
        base.EndAction();
        
        //set attack bool to false for weapon animators and vanilla player animator
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(endAttack)
        {
            //only temporary. Add more logic later
            fsm.ChangeState(player.idleState);
        }
    }
}
