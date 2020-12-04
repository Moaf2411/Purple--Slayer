using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private int amountOfJumpsLeft;

    

    
    
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
        amountOfJumpsLeft = player.playerData.maxAmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }

    public override void Exit()
    {
        base.Exit();
        player.inputHandler.UseJumpInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        player.SetVelocityY(player.playerData.jumpVelocity);
        
        player.StateMachine.ChangeState(player.inAirState);
    }

    public override void Check()
    {
        base.Check();
    }

    public bool CheckIfCanJump()
    {
        return amountOfJumpsLeft > 0;

    }

    public void DecreaseAmountOfJumps()
    {
        amountOfJumpsLeft--;
    }

    public void ResetJump()
    {
        amountOfJumpsLeft = player.playerData.maxAmountOfJumps;
    }

    
    
    
}
