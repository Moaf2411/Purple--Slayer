using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E1_PlayerDetected : EnemyState
{
    private D_Enemy1 stateData;
    private Enemy1 enemy;
    
    

    private bool isDetectingPlayer;

    private Transform player;

    private Vector2 directionToMove;
    public E1_PlayerDetected(Entity entity, EnemyStateMachine stateMachine, string animBoolName,D_Enemy1 stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        isDetectingPlayer = entity.CheckForPlayerInRange(stateData.playerCheckRadius);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isDetectingPlayer = entity.CheckForPlayerInRange(stateData.playerCheckRadius);

        if (!isDetectingPlayer)
        {
            stateMachine.ChangeState(enemy.idleState);
           
        }

        if (isDetectingPlayer)
        {
            player = entity.GetPlayerCollider(stateData.playerCheckRadius).transform;

            
            //added some offsets so that when enemy reaches the player, it doesn't lock into it and made it to sort of float around the player
            if (player.position.x + 1f  < entity.enemyGO.transform.position.x)
            {
                directionToMove.Set(player.position.x-entity.enemyGO.transform.position.x -stateData.xOffset,player.position.y-entity.enemyGO.transform.position.y - stateData.yOffset);
                directionToMove.Normalize();
            }
            
            else if (player.position.x - 1f  > entity.enemyGO.transform.position.x)
            {
                directionToMove.Set(player.position.x-entity.enemyGO.transform.position.x + stateData.xOffset,player.position.y-entity.enemyGO.transform.position.y + stateData.yOffset);
                directionToMove.Normalize();
            }

            

           
            

             if (player.position.x + 1f  < entity.enemyGO.transform.position.x && entity.facingDirection != -1)
             {
                 entity.Flip();
             }
             
             if (player.position.x - 1f > entity.enemyGO.transform.position.x && entity.facingDirection != 1)
             {
                 entity.Flip();
             }



        }
        
       
        
        //enemy.HitPlayer(stateData.hitRadius); //if its touching the player damage the player
        
        entity.SetVelocity(directionToMove * stateData.movementSpeed); // move toward the player 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
