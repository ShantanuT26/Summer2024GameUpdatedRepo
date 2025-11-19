using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private bool endAttack;

    //TESTING
    private PlayerCombat playerCombat;
    public PlayerAttackState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, 
        string animBool) : base(player, fsm, playerData, animBool)
    {
        playerCombat = GameObject.Find("Player")?.GetComponent<PlayerCombat>();
    }

    public void SetWeapon(int x)
    {
        weapon = player.weaponLoadout[x];
       // weapon.SetPlayer(player);
    }
    public override void BeginAction()
    {
        base.BeginAction();
        UnityEngine.Debug.Log("BEGINNING ATTACK ACTION");
        endAttack = false;
        if (weapon != null)
        {
            weapon.SetAnimBools(animBool, true);
            //2025TEST
            playerCombat.CheckAttackHitbox();
            //set attack bool to true for weapon animators and vanilla player animator
        }
        
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
            if(isGrounded)
            {
                fsm.ChangeState(player.idleState);
            }
            else
            {
                fsm.ChangeState(player.inAirState);
            }
        }
    }
}
