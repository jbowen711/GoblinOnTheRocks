using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float destroyTime = 2f;
    public int arrowDamage = 1;
    private bool isMoving = true;

    public Player playerScript;

    [SerializeField]
    private BoxCollider arrowCollider;

    [SerializeField]
    private Rigidbody arrowRb;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        arrowRb = GetComponent<Rigidbody>();

        if (player != null)
        {
            playerHealth ph = player.GetComponent<playerHealth>();
        }

        arrowCollider = GetComponent<BoxCollider>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Arrow"), LayerMask.NameToLayer("Arrow"));
    
    
    }

    void Update()
    {
        if (arrowRb.velocity.magnitude < 0.1f)
        {
            isMoving = false;
            arrowCollider.enabled = false;
        }
        else
        {
            isMoving = true;
            arrowCollider.enabled = true;
        }
    }

                
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Players health goes down
            if (isMoving && arrowRb.velocity.magnitude > 0.5f)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    Debug.Log("damage taken");
                    playerHealth ph = player.GetComponent<playerHealth>();
                    ph.TakeDamage(arrowDamage);
                }

                Destroy(gameObject);
            }
        }
        else if (collision.collider.CompareTag("enemy"))
        {
            playerScript.Score();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            

        }
        else if (collision.collider.CompareTag("arrow"))
        {
            //Destroy(gameObject);
        }
        else
        {
            
            
            Rigidbody arrowRb = GetComponent<Rigidbody>();
            arrowRb.velocity = Vector3.zero;
            arrowCollider.enabled = false;
            Destroy(gameObject, destroyTime);
        }

    }



}
