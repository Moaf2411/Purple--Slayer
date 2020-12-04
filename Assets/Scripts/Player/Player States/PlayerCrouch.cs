using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCrouch : PlayerState
{
    private Vector2 movementInput;
    private bool attackInput;
    public PlayerCrouch(Player player, PlayerStateMachine stateMachine, PlayerData stateData, string animBoolName) : base(player, stateMachine, stateData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        movementInput = player.inputHandler.movementInput;
        attackInput = player.inputHandler.attackInput;
        
        base.LogicUpdate();
        player.SetVelocityX(0);

        if (Mathf.Sign(movementInput.y) != -1)
        {
            player.StateMachine.ChangeState(player.idleState);
        }
        else if (Mathf.Sign(movementInput.y) != -1 && Mathf.Abs(movementInput.x) > Mathf.Epsilon)
        {
            player.StateMachine.ChangeState(player.moveState);
        }
        else if (attackInput && Mathf.Abs(movementInput.y) >Mathf.Epsilon && Mathf.Sign(movementInput.y) == -1)
        {
            player.StateMachine.ChangeState(player.crouchAttackState);
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
}
