using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A flying enemy that moves toward the player if it detects the player and keeps following the player 'til its in range  
 */
public class Enemy1 : Entity
{
   public E1_IdleState idleState { get; private set; }
   public E1_PlayerDetected playerDetectedState { get; private set; }
   



   [SerializeField] private D_Enemy1 e1_Data;


   private AttackDetails attackDetails;

   public override void Awake()
   {
      base.Awake();

      
      idleState=new E1_IdleState(this,stateMachine,"idle",e1_Data,this);
      playerDetectedState=new E1_PlayerDetected(this,stateMachine,"playerDetected",e1_Data,this);
      
      
      stateMachine.Initialize(idleState);
   }
   
   
   
   
   
   
   public void HitPlayer(float radius) //when player hit us
   {
      var player=Physics2D.OverlapCircle(transform.position, radius, entityData.whatIsPlayer);
      if (player)
      {
         attackDetails.damageAmount = e1_Data.damageAmount;
         attackDetails.damagePosition = enemyGO.transform.position;
         
         player.transform.SendMessage("Damage",attackDetails);
      }
   }

   private void OnDrawGizmos()
   {
      Gizmos.DrawWireSphere(transform.position,e1_Data.playerCheckRadius);
   }
}
