using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiniteStateMachine
{
    public PlayerState currentState { get; private set; }

    public void SetInitialState(PlayerState x)
    {
        currentState = x;
        currentState.BeginAction();
    }
    public void ChangeState(PlayerState x)
    {
        currentState.EndAction();

        currentState = x;

        currentState.BeginAction();
    }
}
