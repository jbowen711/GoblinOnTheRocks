using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Arrow Variables
    private float destroyTime = 5f;
    public int arrowDamage = 1;
    private bool isMoving = true;

    public Player playerScript;

    [SerializeField]
    private BoxCollider arrowCollider;

    [SerializeField]
    private Rigidbody arrowRb;

    public GameObject blood;
    public Transform bloodSpawn;

    public Transform arrowChildTransform;



    void Start()
    {
        //Get player object and playerScrip
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
        //Get arrow Rigid Body
        arrowRb = GetComponent<Rigidbody>();

        //Get player health component
        if (player != null)
        {
            playerHealth ph = player.GetComponent<playerHealth>();
        }

        arrowCollider = GetComponent<BoxCollider>();

        //Set arrows on a different layer
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Arrow"), LayerMask.NameToLayer("Arrow"));

        arrowRb = GetComponent<Rigidbody>();



    }

    void Update()
    {
        //Remove Collider from still arrows (allow player and enemies to walk through)
        if (isMoving == false)
        {
            arrowCollider.enabled = false;
        }

    }

                
    public void OnCollisionEnter(Collision collision)
    {
        //Player collides with arrow: does damage to ph if arrow is moving
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
        //Enemy collides wit arrow: enemy dies and runs blood particles
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
            //Arrow collides with map: isMoving set to false and arrow destroys with destroy timer
            Rigidbody arrowRb = GetComponent<Rigidbody>();
            arrowRb.velocity = Vector3.zero;
            isMoving = false;
            Destroy(gameObject, destroyTime);
        }

    }



}
