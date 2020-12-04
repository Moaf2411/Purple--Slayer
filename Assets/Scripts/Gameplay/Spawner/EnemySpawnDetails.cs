using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemySpawnDetails", menuName = "Spawner/Enemy Spawner")]
public class EnemySpawnDetails : ScriptableObject
{
    [SerializeField] public GameObject enemyPrefab;

    public int numberOfEnemies = 1;
    
    public float timeBetweenSpawns;
    public float radius;

    public LayerMask whatIsPlayer;








}
