using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    private float destroyTime = 2f;

    void Start()
    {
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
                
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Players health goes down
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
        if (collision.collider.CompareTag("arrow"))
        {
            Destroy(gameObject);
        }
        else
        {
            Rigidbody arrowRb = GetComponent<Rigidbody>();
            arrowRb.velocity = Vector3.zero;
            Destroy(gameObject, destroyTime);
        }

    }



}
