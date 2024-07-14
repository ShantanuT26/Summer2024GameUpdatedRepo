using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private State currentState;

    public State GetCurrentState()
    {
        return currentState;
    }
    public void InitializeState(State inputState)
    {
        currentState = inputState;
        currentState.BeginAction();
    }
    public void ChangeState(State inputState)
    {
        currentState.EndAction();
        currentState = inputState;
        currentState.BeginAction();
    }
}
