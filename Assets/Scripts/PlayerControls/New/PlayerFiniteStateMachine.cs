using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiniteStateMachine
{
    private State currentState;

    public void SetInitialState(State x)
    {
        currentState = x;
    }
    public void ChangeState(State x)
    {
        currentState.EndAction();

        currentState = x;

        currentState.BeginAction();
    }
}
