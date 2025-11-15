using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Control : PlayerControler
{
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            canJump = true;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.performed;
    }
}
