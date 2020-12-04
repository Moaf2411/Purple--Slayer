using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private bool isTouchingGround;
    private bool attackInput;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        
        player.anim.SetBool("standingAttack",true);
        
        if (isTouchingGround)
        {
            player.SetVelocityX(0);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        player.anim.SetBool("standingAttack",false);
    }

    public override void LogicUpdate()
    {
        attackInput = player.inputHandler.attackInput;
        
        base.LogicUpdate();

        if (isTouchingGround)
        {
            player.SetVelocityX(0);
        }

        if (isAnimationFinished)
        {
            
            if (attackInput)
            {
                player.StateMachine.ChangeState(player.attackState);
                
            }
        
            else if (isTouchingGround)
            {
                player.jumpState.ResetJump();
                player.StateMachine.ChangeState(player.idleState);
                
            }
            else if(!isTouchingGround)
            {
                player.StateMachine.ChangeState(player.inAirState);
                
            }
            
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
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();
        
        isAnimationFinished = true; 
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
        
        player.Attack();
        
    }
    
    
}
