using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchAttack : PlayerState
{
    private bool attackInput;
    private bool animationFinished;

    private Vector2 movementInput;
    public PlayerCrouchAttack(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animationFinished = false;
        
        player.SetVelocityX(0);
        player.anim.SetBool("crouchAttack" , true);
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("crouchAttack" , false);
    }

    public override void LogicUpdate()
    {
        attackInput = player.inputHandler.attackInput;
        movementInput = player.inputHandler.movementInput;
        
        base.LogicUpdate();
        
        player.SetVelocityX(0);

        if (animationFinished)
        {
            if (attackInput && movementInput.y < -0.01f)
            {
                player.StateMachine.ChangeState(player.crouchAttackState);
            }
            else if (movementInput.y > -0.01f)
            {
                player.StateMachine.ChangeState(player.idleState);
            }
            else if(!attackInput && movementInput.y < -0.01f)
            {
                player.StateMachine.ChangeState(player.crouchState);
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
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();
        animationFinished = true;
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
        player.Attack();
    }
}
