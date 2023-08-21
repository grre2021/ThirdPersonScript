using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    public Dictionary<System.Type, State> states;

    protected virtual void Update()
    {
        currentState.Tick(Time.deltaTime);
    }

    public void SwitchState(State playerBaseState)
    {
        currentState = playerBaseState;
        currentState.Enter();
        //        Debug.Log(currentState.ToString());
    }

    public void ChangeState(State playerBaseState)
    {
        currentState.Exit();
        currentState = playerBaseState;
        currentState.Enter();
    }

}
