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
    public AudioSource aS;
    public AudioClip taunt;
    public AudioSource aS2;
    public AudioClip arrowfire;

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

            // after timeTillMove hits 0
            if (isMoving)
            {
                if (timeTillStop <= 0)
                {
                    // switch to not moving
                    isMoving = false;
                    float stopTimeInterval = Random.Range(1f, 3f);
                    timeTillMove = stopTimeInterval;
                    runTrail.Stop(); 
                }
                else
                {   
                    if(moveDirection == Vector3.zero)
                    {
                        // mave the move direction randomized within a 50 degree range
                        float randomAngle = Random.Range(-25f, 25f);
                        Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);
                        moveDirection = randomRotation * facingDirection;
                    }
                    // move enemy
                    rb.velocity = moveDirection.normalized * moveSpeed;
                    timeTillStop -= Time.deltaTime;
                }
            }
            //after timeTillStop hits 0 
            else
            {
                if (timeTillMove <= 0)
                {
                    // switch to is moving
                    isMoving = true;
                    float moveTimeInterval = Random.Range(2f, 4f);
                    timeTillStop = moveTimeInterval;
                    runTrail.Play();
                }
                else
                {
                    // face player while stopped
                    moveDirection = facingDirection;
                    timeTillMove -= Time.deltaTime;
                    // shoot arrow one second before move
                    if (timeTillMove == 1.0f)
                    {
                        FireArrow();
                    }
                }
            }

            
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                //Random fire arrow
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
        //Instantiate arrow at arrow spawn position(child of enemy)
        GameObject arrowClone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.transform.rotation);
        Rigidbody arrowRb = arrowClone.GetComponent<Rigidbody>();
        //play fire sound
        aS2.PlayOneShot(arrowfire);

        if (arrowRb != null)
        {
            //Set direction and add rb force in direction
            Vector3 direction = (player.transform.position - arrowSpawn.position).normalized;     
            arrowRb.AddForce(direction * arrowSpeed, ForceMode.VelocityChange);
        }
    }

    public void TauntFireArrow()
    {    
        //Instantiate arrow
        GameObject arrowClone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.transform.rotation);
        Rigidbody arrowRb = arrowClone.GetComponent<Rigidbody>();
        aS2.PlayOneShot(arrowfire);

        if (arrowRb != null)
        {
            // Calculate direction
            Vector3 direction = (player.transform.position - arrowSpawn.position).normalized;

            // Add random angle spread to arrow
            float spreadAngle = Random.Range(-15f, 15f);
            direction = Quaternion.Euler(0f, spreadAngle, 0f) * direction;

            // Add force in target direction
            arrowRb.AddForce(direction * arrowSpeed, ForceMode.VelocityChange);
            
        }
    }

    private void OnMouseDown()
    {
        //Taunt abiliy - use coroutine as timer
        StartCoroutine(TauntCooldown());
        Taunt();
        //Set taunt icon to true - child of parent enemy
        tauntIcon.SetActive(true);
        //Courouine countdown to hide icon
        StartCoroutine(TauntIconHide());
        //play taunt sound "hehehe"
        aS.PlayOneShot(taunt);
    }

    public void Taunt()
    {
        //Taunt delay
        StartCoroutine(TauntDelayed(0.3f)); 
    }

    IEnumerator TauntDelayed(float delay)
    {
        //Loop for multiple arrows - slight delay in between taunt fires
        for (int i = 0; i < 8; i++)
        {
            TauntFireArrow();
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator TauntCooldown()
    {
        //Taunt cooldown
        canTaunt = false;
        yield return new WaitForSeconds(tauntCooldown);
        canTaunt = true;
    }
    IEnumerator TauntIconHide()
    {
        //Hide icon after 2f
        yield return new WaitForSeconds(2f);
        tauntIcon.SetActive(false);
    }
}
