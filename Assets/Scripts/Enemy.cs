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
    public float stopTimeInterval;
    private float timeTillMove;
    public float moveTimeInterval;
    private float timeTillStop;

    public int health;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeTillMove = stopTimeInterval;
        isMoving = true;

        health = 1;
    }

    void Update()
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
                timeTillStop = moveTimeInterval;
            }
            else
            {
                moveDirection = facingDirection;
                timeTillMove -= Time.deltaTime;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "arrow")
        {
            Destroy(collision.gameObject);
            health -= 1;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
