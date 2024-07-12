using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerFiniteStateMachine fsm;
    protected string animBool;

    public PlayerState(Player player, PlayerFiniteStateMachine fsm, string animBool)
    {
        this.player = player;
        this.fsm = fsm;
        this.animBool = animBool;
    }
    private void BeginAction()
    {
        
    }
    private void EndAction() 
    {

    }
    private void LogicUpdate()
    {

    }
    private void PhysicsUpdate()
    {

    }
}
