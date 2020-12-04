using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : EnemyState
{
    private Enemy2 enemy;
    private D_Enemy2 stateData;

    private float moveSpeed;

    private bool isPlayerDetectedInShortRange, isPlayerDetectedInLongRange;
    private bool wallCheck,groundCheck,LedgeCheck;
    
    
    public E2_MoveState(Entity entity, EnemyStateMachine stateMachine, string animBoolName,Enemy2 enemy,D_Enemy2 stateData) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
        

        isPlayerDetectedInLongRange = false;
        isPlayerDetectedInShortRange = false;
        
        CheckIfShouldTurn();

        moveSpeed = stateData.moveSpeed;
        
        entity.SetVelocityX(moveSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        
        CheckIfShouldTurn();
        
        entity.SetVelocityX(moveSpeed);

        isPlayerDetectedInLongRange = entity.CheckForPlayerInRange(stateData.idleDetectionRadius);
        isPlayerDetectedInShortRange = entity.CheckForPlayerInRange(stateData.DetectionRadius);

        if (!isPlayerDetectedInLongRange)//idle when player is out of range
        {
            entity.stateMachine.ChangeState(enemy.idleState);
        }

        if (isPlayerDetectedInShortRange)//start running when player is close
        {
            entity.stateMachine.ChangeState(enemy.runState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    private void CheckIfShouldTurn()
    {
        wallCheck = entity.WallCheck(enemy.wallCheck,stateData.wallCheckDistance);
        groundCheck = entity.GroundCheck(enemy.groundCheck, stateData.groundCheckDistance);
        LedgeCheck = entity.GroundCheck(enemy.ledgeCheck, stateData.ledgeCheckDistance);
        
        if (wallCheck)
        {
            entity.Flip();
            
            wallCheck = entity.WallCheck(enemy.wallCheck,stateData.wallCheckDistance);
        }

        if (groundCheck && !LedgeCheck)
        {
            entity.Flip();
            
            groundCheck = entity.GroundCheck(enemy.groundCheck, stateData.groundCheckDistance);
            LedgeCheck = entity.GroundCheck(enemy.ledgeCheck, stateData.ledgeCheckDistance);
        }
    }
    
    
}
