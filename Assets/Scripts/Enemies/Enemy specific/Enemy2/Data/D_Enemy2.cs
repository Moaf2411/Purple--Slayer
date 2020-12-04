using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemy2Data" , menuName = "Data/Enemy Data/Enemy2 Data")]
public class D_Enemy2 : ScriptableObject
{

    [Header("Detecting the Player")] 
    public float idleDetectionRadius = 10f;

    public float DetectionRadius = 2f;

    [Header("Move State")] 
    public float moveSpeed = 3f;

    public float wallCheckDistance = 1f;
    public float groundCheckDistance = 1f;
    public float ledgeCheckDistance = 1f;

    [Header("Run State")] 
    public float maxSpeedDelta = 5f;

    [Header("Damage details")] public float damageAmount = 5f;



}
