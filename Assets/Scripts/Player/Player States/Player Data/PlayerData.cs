using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData" , menuName= "Data/Player Data/playerData")]
public class PlayerData : ScriptableObject
{
    [Header("Input")] public float inputHoldTime = 0.3f;

    [Header("Physics Checks")] 
    
    public float groundCheckRadius = 0.7f;
    public LayerMask whatIsGround;
    public float wallCheckDistance = 0.4f;
    public LayerMask whatIsPlayer;

    [Header("Move State")] 
    public float movementSpeed = 10f;

    public float timeBetweenAfterImages = 0.001f;

    public float acceleration = 10f;
    
    

    [Header("Jump State")] 
    public float jumpVelocity = 10f;

    public int maxAmountOfJumps = 2;
    public float coyoteTime = 0.2f;

    [Header("In Air State")] public float inAirMovementMultiplier = 0.7f;

    [Header("Attack State")] public float attackRadius = 0.5f;
    public LayerMask whatIsDamageable;
    public AttackDetails attackDetails;
    public int damageAmount = 10;
    





}
