using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector2 movementInput;

    private bool jumpInput;
    private bool isTouchingGround;
    private bool attackInput;
    private bool runInput,oldRunInput;

    private bool setAfterImage;

    private float accelerationMultiplier = 0f;
    private float velocity;
    private float lastAfterImageTime;

    private Vector2 lastRunPosition;

    private GameObject afterImage;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpInput = player.inputHandler.jumpInput;
        runInput = false;
        velocity = player.playerData.movementSpeed;
        oldRunInput = false;
        setAfterImage = false;
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        
        movementInput = player.inputHandler.movementInput;
        jumpInput = player.inputHandler.jumpInput;
        attackInput = player.inputHandler.attackInput;
        runInput = player.inputHandler.runInput;
        
        SetTheAcceleration(runInput);
        CheckRunAfterImage();

        if (setAfterImage)
        {
            PlaceRunAfterImage(lastRunPosition);
            setAfterImage = false;
            lastAfterImageTime = Time.time;
        }

        if (!oldRunInput && runInput)
        {
            oldRunInput = true;
            lastAfterImageTime = Time.time;
        }

        if (!runInput)
        {
            oldRunInput = false;
        }
        
        

        
        velocity = movementInput.x * player.playerData.movementSpeed + (accelerationMultiplier * player.playerData.acceleration * player.facingDirection);
        
        
        base.LogicUpdate();
        
        ///////////////////////////////////////////////////////////////////////////////TRANSITIONS
        
        if (jumpInput && player.jumpState.CheckIfCanJump() && isTouchingGround)
        {
            player.jumpState.DecreaseAmountOfJumps();

            player.StateMachine.ChangeState(player.jumpState);

        }
        else if (attackInput)
        {
            player.inputHandler.useAttackInput();
            player.StateMachine.ChangeState(player.attackState);
        }

        if (!isTouchingGround)
        {
           
            player.inAirState.SetCoyoteTime();
            player.jumpState.DecreaseAmountOfJumps();
            player.StateMachine.ChangeState(player.inAirState);
           
        }
        
        
         
        if (Mathf.Abs(movementInput.x) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
        if(Mathf.Sign(movementInput.y)==-1 && Mathf.Abs(movementInput.y) > Mathf.Epsilon)
        {
            player.StateMachine.ChangeState(player.crouchState);
        }
        
        ///////////////////////////////////////////////////////////////////////////END OF TRANSITIONS
       
        
        player.CheckFlip(movementInput.x);
        player.SetVelocityX(velocity);
        

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        
        
      
        
    }

    public override void Check()
    {
        base.Check();
        isTouchingGround = player.GroundCheck();
    }

    private void SetTheAcceleration(bool increase)
    {
        if (increase)
        {
            accelerationMultiplier = Mathf.Clamp(accelerationMultiplier + Time.deltaTime, 0f, 1f);
        }
        else
        {
            accelerationMultiplier = Mathf.Clamp(accelerationMultiplier - Time.deltaTime, 0f, 1f);
        }
    }

    #region AfterImage For Run

    private void PlaceRunAfterImage(Vector2 position)
    {
        afterImage = AfterImagePool.AfterImagePoolInstance.GetFromPool();
        afterImage.transform.position = position;
        afterImage.gameObject.SetActive(true);
        

    }

    private void CheckRunAfterImage()
    {
        if (Time.time >= lastAfterImageTime + player.playerData.timeBetweenAfterImages && runInput)
        {
            setAfterImage = true;
            lastRunPosition = player.transform.position;
        }
    }

    #endregion

    
}
