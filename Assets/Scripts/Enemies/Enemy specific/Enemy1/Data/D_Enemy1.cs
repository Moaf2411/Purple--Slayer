using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    [CreateAssetMenu(fileName = "newEnemy1Data" , menuName = "Data/Enemy Data/Enemy1")]
public class D_Enemy1 : ScriptableObject
{
    [Header("Idle State")]
    public float playerCheckRadius = 5f;

    [Header("Player Detected State")] public float movementSpeed = 7f;
    public float xOffset = 2f;
    public float yOffset = 2f;

    [Header("Combat")] public float damageAmount = 10f;
    public float hitRadius = 0.6f;
}
