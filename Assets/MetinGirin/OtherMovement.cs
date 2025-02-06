using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OtherMovement : MonoBehaviour
{
    private PlayerInputActions playerInputs;

    public static OtherMovement Instance { get; private set; }

    bool moving = true;

    private void Awake()
    {
        Instance = this;
        
        
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Enable();

        playerInputs.Player.Jump.performed += PlayerJump;
        playerInputs.Player.Stop.performed += PlayerStop;
        playerInputs.Player.Stop.canceled += PlayerStop_canceled;
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
        moving = false;
        Debug.Log(context);
    }

    private void PlayerStop_canceled(InputAction.CallbackContext context)
    {
        moving = true;
        Debug.Log(context);
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if(context.ReadValueAsButton())
        {
            Movement.Instance.HandleJump();
        }
    }

    public bool StopControl()
    {
        return moving;
    }
}
