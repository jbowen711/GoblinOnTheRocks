using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float destroyTime = 2f;
    public float arrowDamage;

    public Player playerScript;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        if (player != null)
        {
            playerHealth ph = player.GetComponent<playerHealth>();
        }

    }

    void Update()
    {
        
    }
                
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Players health goes down
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth ph = player.GetComponent<playerHealth>();
                ph.TakeDamage(arrowDamage);
            }
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("enemy"))
        {
            playerScript.Score();
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
