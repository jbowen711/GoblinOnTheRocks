using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    //Second Spawner - Backside
    public GameObject archer; 
    public Transform spawnPoint; 
    [SerializeField] float spawnTime = 10f; 

    private void Start()
    {
        
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true) 
        {
            Instantiate(archer, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
