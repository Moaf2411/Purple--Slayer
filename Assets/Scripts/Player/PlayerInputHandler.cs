using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player player;
    public Vector2 movementInput { get; private set; }

    public bool jumpInput { get; private set; }
    
    public bool attackInput { get; private set; }
    
    public bool runInput { get; private set; }
    
    public bool actionInput { get; private set; }

    private float inputHoldTime, inputStartTime;
    

    private void Awake()
    {
        jumpInput = false;
        attackInput = false;
        movementInput.Set(0,0);
        player = GetComponent<Player>();
        inputHoldTime = player.playerData.inputHoldTime;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1)
        {

            movementInput = context.ReadValue<Vector2>();

        }

        if (context.canceled)
        {
            movementInput=Vector2.zero;
        }
        
        
        
    }
    
    

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            inputStartTime = Time.time; 
        }

        
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInput = true;
            inputStartTime = Time.time;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            runInput = true;
        }

        if (context.canceled)
        {
            runInput = false;
        }
    }

    public void OnActionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            actionInput = true;
            inputStartTime = Time.time;
        }
        
    }

    public void SetActionInput(bool state)
    {
        actionInput = state;
    }

    private void Update()
    {
        SetInputToFalse();
    }


    private void SetInputToFalse()
    {
        if (Time.time >= inputStartTime + inputHoldTime)
        {
            jumpInput = false;
            attackInput = false;
            actionInput = false;
            
            //TODO should define separated start times for each input
            
        }

        if (Time.timeScale==0) // when pausing the game 
        {
           
            jumpInput = false;
            attackInput = false;
            actionInput = false;
            runInput = false;
        }
    }

    public void UseJumpInput()
    {
        jumpInput = false;
    }

    public void useAttackInput()
    {
        attackInput = false;
    }
    
   
}
