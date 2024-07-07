using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    // Update is called once per frame
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit(); // exit current state if not null
        currentState = newState; // set currentState to the newState
        currentState?.Enter(); // enter newState stored in currentState
    }
}
