using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //First Spawner - Front
    //Spawn variables
    public GameObject archer;
    public Transform spawnPoint; 
    [SerializeField] float spawnTime = 10f; 

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true) //Infinte Spawning - could slow down if there are too many
        {
            //Instantiate spawner at spawnposition
            Instantiate(archer, spawnPoint.position, spawnPoint.rotation);

            // Wait Spawn time
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
