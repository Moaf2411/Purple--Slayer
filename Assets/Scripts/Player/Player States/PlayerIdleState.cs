using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private bool isTouchingGround;
    private bool isTouchingWall;
    private bool jumpInput;
    private bool attackInput;

    private Vector2 movementInput;
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpInput = player.inputHandler.jumpInput;
        
        player.SetVelocityX(0);
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
        
        base.LogicUpdate();
        
        player.SetVelocityX(0);
   
        if (Mathf.Abs(movementInput.x)>Mathf.Epsilon)
        {
            player.CheckFlip(movementInput.x);
            stateMachine.ChangeState(player.moveState);
            
        }

        if (jumpInput && player.jumpState.CheckIfCanJump())
        {
            player.jumpState.DecreaseAmountOfJumps();
            player.StateMachine.ChangeState(player.jumpState);
        }

        else if (attackInput)
        {
            player.inputHandler.useAttackInput();
            player.StateMachine.ChangeState(player.attackState);
        }

        else if(Mathf.Sign(movementInput.y)==-1 && Mathf.Abs(movementInput.y) > Mathf.Epsilon)
        {
            player.StateMachine.ChangeState(player.crouchState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Check()
    {
        base.Check();
        isTouchingGround = player.GroundCheck();
        isTouchingWall = player.WallCheck();
    }
}
