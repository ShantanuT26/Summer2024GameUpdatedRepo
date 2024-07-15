using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool canJump { get; private set; }  // set to true from player class
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
        if(context.performed && canJump) 
        {
            Debug.Log("contextperformed");
            jumpInput = true;
        }
        //TEMPORARY
        if(context.canceled)
        {
            jumpInput = false;
            canJump = true;
        }
    }  
    public void SetJumpInput(bool x)
    {
        jumpInput = x;
    }
    public void SetCanJump(bool x)
    {
        canJump = x;
    }
}
