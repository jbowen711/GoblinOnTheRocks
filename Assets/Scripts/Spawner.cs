using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject archer; // The object you want to spawn
    public Transform spawnPoint; // The location where you want to spawn the objects
    [SerializeField] float spawnTime = 10f; // The time interval between spawns

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true) // Infinite loop to continuously spawn objects
        {
            Instantiate(archer, spawnPoint.position, spawnPoint.rotation);

            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
