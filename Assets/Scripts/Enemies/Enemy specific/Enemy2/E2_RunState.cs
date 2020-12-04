using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RunState : EnemyState
{
    private Enemy2 enemy;
    private D_Enemy2 stateData;

    private float runSpeed;

    private bool wallCheck, groundCheck, ledgeCheck;
    private bool isPlayerInShortRange, isPlayerInLongRange;
    
    
    public E2_RunState(Entity entity, EnemyStateMachine stateMachine, string animBoolName,Enemy2 enemy,D_Enemy2 stateData) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        runSpeed = (stateData.moveSpeed + UnityEngine.Random.Range(1, stateData.maxSpeedDelta));
        
        
        
        CheckIfShouldTurn();
        
        entity.SetVelocityX(runSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckIfShouldTurn();
        
        isPlayerInLongRange = entity.CheckForPlayerInRange(stateData.idleDetectionRadius);
        isPlayerInShortRange = entity.CheckForPlayerInRange(stateData.DetectionRadius);

        if (isPlayerInLongRange && !isPlayerInShortRange)
        {
            entity.stateMachine.ChangeState(enemy.moveState);
        }

        if (!isPlayerInLongRange)
        {
            entity.stateMachine.ChangeState(enemy.idleState);
        }
        
        entity.SetVelocityX(runSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    
    
    
    
    
    
    private void CheckIfShouldTurn()
    {
        wallCheck = entity.WallCheck(enemy.wallCheck,stateData.wallCheckDistance);
        groundCheck = entity.GroundCheck(enemy.groundCheck, stateData.groundCheckDistance);
        ledgeCheck = entity.GroundCheck(enemy.ledgeCheck, stateData.ledgeCheckDistance);
        
        if (wallCheck)
        {
            entity.Flip();
            
            wallCheck = entity.WallCheck(enemy.wallCheck,stateData.wallCheckDistance);
        }

        if (groundCheck && !ledgeCheck)
        {
            entity.Flip();
            
            groundCheck = entity.GroundCheck(enemy.groundCheck, stateData.groundCheckDistance);
            ledgeCheck = entity.GroundCheck(enemy.ledgeCheck, stateData.ledgeCheckDistance);
        }
    }
}
