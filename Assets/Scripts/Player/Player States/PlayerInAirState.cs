using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool jumpInput;
    private bool isTouchingGround;
    private bool coyoteTime;
    private bool attackInput;
    
    private float coyoteStartTime;
    
    private Vector2 movementInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
        coyoteTime = false;
    }

    public override void Enter()
    {
        base.Enter();
        

    }

    public override void Exit()
    {
        base.Exit();
        coyoteTime = false;
    }

    public override void LogicUpdate()
    {
        movementInput = player.inputHandler.movementInput;
        jumpInput = player.inputHandler.jumpInput;
        attackInput = player.inputHandler.attackInput;
       
        base.LogicUpdate();
        
        CheckCoyoteTime();
       
        


        if (isTouchingGround && player.rb.velocity.y < 0.01f)
        {
            player.jumpState.ResetJump();
            player.StateMachine.ChangeState(player.idleState);
        }
        else if (jumpInput && coyoteTime) // we set the coyote time to true only when we came to inAir state from moving state (when moving and suddenly falling down)
        
            // it seems that it doesn't work with the condition : jumpstate.checkifcanjump
        
        {
            
           
            player.jumpState.DecreaseAmountOfJumps();
            player.StateMachine.ChangeState(player.jumpState);
        }
        else if (jumpInput && player.jumpState.CheckIfCanJump() && player.rb.velocity.y < 0)//double jump
        {
            player.jumpState.DecreaseAmountOfJumps();
            player.StateMachine.ChangeState(player.jumpState);
        }
        else if(attackInput)
        {
            player.inputHandler.useAttackInput();
            player.StateMachine.ChangeState(player.attackState);
        }
        
        if (Mathf.Abs(movementInput.x)> Mathf.Epsilon)
        {
            player.CheckFlip(movementInput.x);
            player.SetVelocityX(player.playerData.movementSpeed * player.playerData.inAirMovementMultiplier * player.facingDirection);
        }
        else
        {
            player.SetVelocityX(player.rb.velocity.x);
        }
        
       
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        player.anim.SetFloat("Yvelocity",player.rb.velocity.y); //setting the animator (Rise and Fall animations)
        
        
       
        
    }

    public override void Check()
    {
        base.Check();

        isTouchingGround = player.GroundCheck();
        
    }

    public void SetCoyoteTime()
    {
        coyoteTime = true;
        StartCoyoteTime();
    }
    public void StartCoyoteTime()
    {
        coyoteStartTime = Time.time;
       
    }

    public void CheckCoyoteTime()
    {
        if (Time.time >= player.playerData.coyoteTime + coyoteStartTime && coyoteTime)
        {
            coyoteTime = false;
        }
    }
}
