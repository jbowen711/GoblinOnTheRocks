using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;

    private Vector3 facingDirection;
    private Vector3 moveDirection;

    public float moveSpeed;

    public bool isMoving;
    private float timeTillMove;
    private float timeTillStop;


    public int health;

    public float shootInterval = 8f;
    private float timer = 0f;
    [SerializeField] GameObject arrow;
    public float arrowSpeed = 50f;
    public Transform arrowSpawn;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        float stopTimeInterval = Random.Range(1f, 3f);
        timeTillMove = stopTimeInterval;
        isMoving = true;

        health = 1;
    }

    void Update()
    {
        if(player != null)
        {
        
            // enemy faces player
            facingDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(facingDirection.x, facingDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
    
            // enemy moves and stops on a timed interval
            if(isMoving)
            {
                if(timeTillStop <= 0)
                {
                    isMoving = false;
                    float stopTimeInterval = Random.Range(1f, 3f);
                    timeTillMove = stopTimeInterval;
                }
                else 
                {
                    rb.velocity = moveDirection.normalized * moveSpeed;
                    timeTillStop -= Time.deltaTime;
                }
            }
            else
            {
                if(timeTillMove <= 0)
                {
                    isMoving = true;
                    float moveTimeInterval = Random.Range(2f, 4f);
                    timeTillStop = moveTimeInterval;
                }
                else
                {
                    moveDirection = facingDirection;
                    timeTillMove -= Time.deltaTime;
                    if (timeTillMove == 1.0f)
                    {
                        FireArrow();
                    }
                }
            }


            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                FireArrow();
                timer = 0f;
            }
        }
    }

    /*public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "arrow")
        {
            Debug.Log("Collided");
            Destroy(collision.gameObject);
            health -= 1;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/

    /*public void FireArrow()
    {
        Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        if (arrowRb != null)
        {
            Debug.Log("ArrowMove");
            Vector3 direction = (player.transform.position - arrowSpawn.position).normalized;
            arrow.transform.rotation = Quaternion.LookRotation(direction);
            arrowRb.velocity = direction * arrowSpeed;
        }
        
    }
    */
    public void FireArrow()
    {
        GameObject arrowClone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.transform.rotation);
        
        Rigidbody arrowRb = arrowClone.GetComponent<Rigidbody>();

        if (arrowRb != null)
        {
            Vector3 direction = (player.transform.position - arrowSpawn.position).normalized;
            //arrowClone.transform.rotation = Quaternion.LookRotation(direction);
            arrowRb.AddForce(direction * arrowSpeed, ForceMode.VelocityChange);

        }
    }
}
