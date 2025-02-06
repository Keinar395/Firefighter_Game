using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OtherMovement : MonoBehaviour
{
    private PlayerInputActions playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Enable();

        playerInputs.Player.Jump.performed += PlayerJump;
        playerInputs.Player.Stop.performed += PlayerStop;
        playerInputs.Player.Fire.performed += PlayerFire;

    }

    private void PlayerFire(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.ReadValueAsButton())
        {
            weapon.Instance.shoot();
        }
    }

    private void PlayerStop(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (!context.ReadValueAsButton())
        {
            Movement.Instance.Stop();
        }
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if(context.ReadValueAsButton())
        {
            Movement.Instance.HandleJump();
        }
    }
}
