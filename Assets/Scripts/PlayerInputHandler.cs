using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movementInput { get; private set; }
    public bool jumpInput { get; private set; }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        if(context.canceled)
        {
            movementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.performed) 
        {
            jumpInput = true;
        }
        //TEMPORARY
        if(context.canceled)
        {
            jumpInput = false;
        }
    }   

}
