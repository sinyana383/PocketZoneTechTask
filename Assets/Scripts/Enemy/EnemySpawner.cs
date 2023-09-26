using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int maxEnemiesToSpawn = 3;
    public float spawnInterval = 5f;

    private List<Transform> availableSpawnPoints;

    private void Start()
    {
        availableSpawnPoints = new List<Transform>(spawnPoints);
        
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        int spawnedEnemyCount = 0;

        while (spawnedEnemyCount < maxEnemiesToSpawn && availableSpawnPoints.Count > 0)
        {
            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform randomSpawnPoint = availableSpawnPoints[randomSpawnIndex];
            
            availableSpawnPoints.RemoveAt(randomSpawnIndex);
            Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
            spawnedEnemyCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
