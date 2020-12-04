using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : EnemyState
{
    private Enemy2 enemy;
    private D_Enemy2 stateData;

    private bool isPlayerDetectedInLongRange,isPlayerDetectedInShortRange;
    
    public E2_IdleState(Entity entity, EnemyStateMachine stateMachine, string animBoolName,Enemy2 enemy,D_Enemy2 statedata) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
        this.stateData = statedata;
    }


    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0);
        entity.SetVelocityY(0);

        isPlayerDetectedInLongRange = false;
        isPlayerDetectedInShortRange = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isPlayerDetectedInLongRange = entity.CheckForPlayerInRange(stateData.idleDetectionRadius);
        isPlayerDetectedInShortRange = entity.CheckForPlayerInRange(stateData.DetectionRadius);

        if (isPlayerDetectedInLongRange) //start Moving when player is in range
        {
            entity.stateMachine.ChangeState(enemy.moveState);
        }

        if (isPlayerDetectedInShortRange)//start Moving Fast when player is close
        {
            entity.stateMachine.ChangeState(enemy.runState);
        }
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        entity.SetVelocityX(0);
        entity.SetVelocityY(0);
    }
}
