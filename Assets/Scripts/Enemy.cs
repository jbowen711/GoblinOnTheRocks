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
    public float arrowSpeed = 45f;
    public Transform arrowSpawn;

    public bool canTaunt;
    public float tauntCooldown = 5f;
    public GameObject tauntIcon;

    public ParticleSystem runTrail;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tauntIcon.SetActive(false);

        isMoving = true;
        timeTillStop = Random.Range(2f,4f);

        health = 1;

    }

    void Update()
    {
        if (player != null)
        {

            // enemy faces player
            facingDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(facingDirection.x, facingDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // enemy moves and stops on a timed interval
            if (isMoving)
            {
                if (timeTillStop <= 0)
                {
                    isMoving = false;
                    float stopTimeInterval = Random.Range(1f, 3f);
                    timeTillMove = stopTimeInterval;
                    runTrail.Stop(); 
                }
                else
                {   
                    if(moveDirection == Vector3.zero)
                    {
                        float randomAngle = Random.Range(-25f, 25f);
                        Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);
                        moveDirection = randomRotation * facingDirection;
                    }
                    rb.velocity = moveDirection.normalized * moveSpeed;
                    timeTillStop -= Time.deltaTime;
                }
            }
            else
            {
                if (timeTillMove <= 0)
                {
                    isMoving = true;
                    float moveTimeInterval = Random.Range(2f, 4f);
                    timeTillStop = moveTimeInterval;
                    runTrail.Play();
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
                float rand = Random.Range(0f,2f);
                if (rand >= 1)
                {
                    FireArrow();
                }
                timer = 0f;
            }
        }
    }

    public void FireArrow()
    {
        GameObject arrowClone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.transform.rotation);
        Rigidbody arrowRb = arrowClone.GetComponent<Rigidbody>();

        if (arrowRb != null)
        {
            Vector3 direction = (player.transform.position - arrowSpawn.position).normalized;     
            arrowRb.AddForce(direction * arrowSpeed, ForceMode.VelocityChange);
        }
    }

    public void TauntFireArrow()
    {    
        GameObject arrowClone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.transform.rotation);
        Rigidbody arrowRb = arrowClone.GetComponent<Rigidbody>();

        if (arrowRb != null)
        {
            // Calculate the initial direction towards the player
            Vector3 direction = (player.transform.position - arrowSpawn.position).normalized;

            // Apply a random spread to the direction vector
            float spreadAngle = Random.Range(-15f, 15f);
            direction = Quaternion.Euler(0f, spreadAngle, 0f) * direction;

            // Add force to the arrow clone to make it move in the randomized direction
            arrowRb.AddForce(direction * arrowSpeed, ForceMode.VelocityChange);
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(TauntCooldown());
        Taunt();
        tauntIcon.SetActive(true);
        StartCoroutine(TauntIconHide());
    }

    public void Taunt()
    {
        StartCoroutine(TauntDelayed(0.3f)); 
    }

    IEnumerator TauntDelayed(float delay)
    {
        for (int i = 0; i < 8; i++)
        {
            TauntFireArrow();
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator TauntCooldown()
    {
        canTaunt = false;
        yield return new WaitForSeconds(tauntCooldown);
        canTaunt = true;
    }
    IEnumerator TauntIconHide()
    {
        yield return new WaitForSeconds(2f);
        tauntIcon.SetActive(false);
    }
}
