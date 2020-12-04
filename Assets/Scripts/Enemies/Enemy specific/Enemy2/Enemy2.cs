using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
   public E2_IdleState idleState { get; private set; }
   public E2_MoveState moveState { get; private set; }
   
   public E2_RunState runState { get; private set; }

   [SerializeField] private D_Enemy2 e2_Data;

   [SerializeField] public Transform wallCheck,groundCheck,ledgeCheck;

   private AttackDetails attackDetails;

   public override void Awake()
   {
      base.Awake();
      
      idleState=new E2_IdleState(this,stateMachine,"idle",this,e2_Data);
      moveState=new E2_MoveState(this,stateMachine,"move",this,e2_Data);
      runState=new E2_RunState(this,stateMachine,"move",this,e2_Data);
      
      stateMachine.Initialize(idleState);
      
   }
   
   
   
   public void HitPlayer(float radius) //when player hit us
   {
      var player=Physics2D.OverlapCircle(transform.position, radius, entityData.whatIsPlayer);
      if (player)
      {
         attackDetails.damageAmount = e2_Data.damageAmount;
         attackDetails.damagePosition = enemyGO.transform.position;
         
         player.transform.SendMessage("Damage",attackDetails);
      }
   }




   
   
   
   
   
   
   
   
   
   
   
   
   
   
   


   private void OnDrawGizmos()
   {
      Gizmos.color=Color.red;
      Gizmos.DrawWireSphere(transform.position,e2_Data.idleDetectionRadius);
      Gizmos.DrawWireSphere(transform.position,e2_Data.DetectionRadius);
      Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x+e2_Data.wallCheckDistance,wallCheck.position.y));
      Gizmos.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x,groundCheck.position.y-e2_Data.groundCheckDistance));
      Gizmos.DrawLine(ledgeCheck.position,new Vector3(ledgeCheck.position.x,ledgeCheck.position.y-e2_Data.groundCheckDistance));
   }
}
