using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerFiniteStateMachine fsm;
    protected string animBool;
    protected float actionStartTime;
    protected PlayerData playerData;

    public static event Action<string, bool> changeAnimBool;

    public PlayerState(Player player, PlayerFiniteStateMachine fsm, PlayerData playerData, string animBool)
    {
        this.player = player;
        this.fsm = fsm;
        this.animBool = animBool;
        this.playerData = playerData;
    }
    public virtual void BeginAction()
    {
        actionStartTime = Time.time;
        ChangeAnimBool(animBool, true);
    }
    public virtual void EndAction() 
    {
        ChangeAnimBool(animBool, true);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    private void ChangeAnimBool(string x, bool y)
    {
        changeAnimBool?.Invoke(x, y);
    }
}
