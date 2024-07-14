using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState
{
    protected Player player;
    protected PlayerFiniteStateMachine fsm;
    protected string animBool;
    protected float actionStartTime;
    protected PlayerData playerData;
    

    //Temporary variable;

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
        LogicChecks();
        PhysicsChecks();
    }
    public virtual void EndAction() 
    {
        ChangeAnimBool(animBool, false);
    }
    public virtual void LogicUpdate()
    {
        LogicChecks();
        
    }
    public virtual void PhysicsUpdate()
    {
        PhysicsChecks();
    }
    private void ChangeAnimBool(string x, bool y)
    {
        Debug.Log("Begininvokeprocess");
        changeAnimBool?.Invoke(x, y);
    }
    public virtual void LogicChecks()
    {
        
    }
    public virtual void PhysicsChecks()
    {

    }
}
