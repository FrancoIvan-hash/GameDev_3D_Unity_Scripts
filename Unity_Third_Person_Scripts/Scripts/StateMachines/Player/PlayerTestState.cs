using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        // this is how you subscribe to an event
        stateMachine.InputReader.JumpEvent += OnJump;
        Debug.Log("Enter");
    }

    public override void Exit()
    {
        // this is how you unsubscribe to an event
        stateMachine.InputReader.JumpEvent -= OnJump;
        Debug.Log("Exit");
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        Debug.Log(timer);

    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
