using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{

    //First Spawner - Front
    //Spawn variables
    public GameObject archer;
    public Transform spawnPoint;
    public float spawnTime;
    [SerializeField] float initialSpawnTime = 10f;
    [SerializeField] float minSpawnTime = 2f;
    [SerializeField] float spawnTimeDecreaseRate = 0.6f;

    private void Start()
    {
        spawnTime = initialSpawnTime;
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true) // Infinite Spawning - could slow down if there are too many
        {
            // Instantiate spawner at spawn position
            Instantiate(archer, spawnPoint.position, spawnPoint.rotation);

            // Decrease spawn time
            spawnTime = Mathf.Max(minSpawnTime, spawnTime - spawnTimeDecreaseRate);

            // Wait Spawn time
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
