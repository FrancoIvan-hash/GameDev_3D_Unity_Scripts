using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }

    // this is an event
    public event Action JumpEvent; // event for jumping action
    public event Action DodgeEvent; // event for dodge action
    public event Action TargetEvent; // event for target action
    public event Action CancelEvent; // event for cancel action

    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }

    private Controls controls;

    // Start is called before the first frame update
    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        // enable the controls
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        // disable the controls
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // if we pressed the Jump button/action, invoke event, otherwise return
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // we don't actually use it in code, cinemachine component is using it for us so we don't need to store it
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        CancelEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) { IsAttacking = true; }

        else if (context.canceled) { IsAttacking = false; }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed) { IsBlocking = true; }

        else if (context.canceled) { IsBlocking = false; }
    }
}
