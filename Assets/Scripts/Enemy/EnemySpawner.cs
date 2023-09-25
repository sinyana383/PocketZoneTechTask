using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform[] spawnPoints; // An array of spawn points within the NavMesh

    public int maxEnemiesToSpawn = 3; // Maximum number of enemies to spawn
    public float spawnInterval = 5f; // Time between spawns

    private List<Transform> availableSpawnPoints; // List of available spawn points

    private void Start()
    {
        // Initialize the list of available spawn points
        availableSpawnPoints = new List<Transform>(spawnPoints);

        // Start spawning enemies at regular intervals
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        int spawnedEnemyCount = 0;

        while (spawnedEnemyCount < maxEnemiesToSpawn && availableSpawnPoints.Count > 0)
        {
            // Choose a random index from the availableSpawnPoints list
            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);

            // Get the random spawn point transform
            Transform randomSpawnPoint = availableSpawnPoints[randomSpawnIndex];

            // Remove the used spawn point from the list
            availableSpawnPoints.RemoveAt(randomSpawnIndex);

            // Instantiate the enemy at the chosen spawn point
            Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);

            // Increment the spawned enemy count
            spawnedEnemyCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
