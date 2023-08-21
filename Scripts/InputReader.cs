using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Control.IPlayerActions
{
    private Control controls;

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;
    public event Action Attack;

    public event Action Roll;

    public event Action ClimbEvent;
    
    
    public Vector2 moveValue { get; private set; }
    
    public bool is_climb { get; private set; }

    private void Start()
    {
        controls = new Control();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    private void OnDestroy()
    {
        controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DodgeEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TargetEvent?.Invoke();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CancelEvent?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("attack input");
            Attack?.Invoke();
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Roll?.Invoke();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventCenter.Instance.EventTrigger(eventInput.INTERACTION);
        }
        
    }

    public void OnUI(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventCenter.Instance.EventTrigger(eventInput.Menu);
        }
    }

    public void OnClimb(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           // Debug.Log("hold space");
            is_climb = true;
            ClimbEvent?.Invoke();
        }

        if (context.canceled)
        {
            //Debug.Log("Stop Holding");
            is_climb = false;
        }
    }
   
}
