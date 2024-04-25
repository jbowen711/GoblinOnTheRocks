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

    public GameObject blood;
    public Transform bloodSpawn;


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
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
            Quaternion rot = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            Instantiate(blood, bloodSpawn.position, rot);


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
