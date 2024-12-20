using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 2.0f; // Set the delay in seconds

    void Start()
    {
        Invoke("SpawnEnemy", spawnDelay);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
