using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnDetails spawnDetails;

    private bool isSpawning;

    private void OnEnable()
    {
        isSpawning = false;
    }

    private void Update()
    {
        if (CheckForPlayer(spawnDetails.radius) && !isSpawning)
        {
            StartCoroutine(SpawnEnemies());
            isSpawning = true;
        }
    }


   private IEnumerator SpawnEnemies()
   {
       for (int i = 0; i < spawnDetails.numberOfEnemies; i++)
       {
           Instantiate(spawnDetails.enemyPrefab, transform.position, quaternion.identity);
           yield return new WaitForSeconds(spawnDetails.timeBetweenSpawns);
       }
       gameObject.SetActive(false);
       
   }

    private bool CheckForPlayer(float radius)
    {
        return Physics2D.OverlapCircle(transform.position,radius, spawnDetails.whatIsPlayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,spawnDetails.radius);
    }
}
