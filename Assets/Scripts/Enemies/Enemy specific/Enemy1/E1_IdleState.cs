using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : EnemyState
{
    private Enemy1 enemy;
    private D_Enemy1 stateData;

    private bool isPlayerDetected;
    
    public E1_IdleState(Entity entity, EnemyStateMachine stateMachine, string animBoolName,D_Enemy1 stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        this.enemy = enemy;
    }


    public override void Enter()
    {
        base.Enter();
        
        entity.SetVelocityX(0);
        entity.SetVelocityY(0);

        isPlayerDetected = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isPlayerDetected = entity.CheckForPlayerInRange(stateData.playerCheckRadius);

        if (isPlayerDetected)
        {
            enemy.stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        entity.SetVelocityX(0);
        entity.SetVelocityY(0);
    }
    
    
    
}
