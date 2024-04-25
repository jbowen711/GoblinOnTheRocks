using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public Rigidbody rb;
    public float moveSpeed;
    public float boostPower;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Transform orientation;

    public ParticleSystem runTrail;
    public ParticleSystem boostTrail;

    public float boostRate;
    private float timeTillBoost;

    public int score;

    public AudioSource aS;
    public AudioClip boostSound;


    void Start()
    {
        score = 0;
        timeTillBoost = boostRate;
    }

    void Update()
    {        
        //Get Player Input Horizontal and Vertical
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Rigid Body movement calculations
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);

        //Dash using boostPower in movementDirection
        if(Input.GetKeyDown(KeyCode.Space) && (timeTillBoost <= 0))
        {
            rb.AddForce(moveDirection.normalized * boostPower, ForceMode.Impulse);
            //TODO: Particles On Dash
            boostTrail.Play();
            aS.PlayOneShot(boostSound);
            timeTillBoost = boostRate;
        }
        timeTillBoost -= 0.1f;

        //Orientation of facing diection
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
                
        // play run trail while moving
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (!runTrail.isPlaying)
            {
                runTrail.Play();
            }
        }
        else
        {
            if (runTrail.isPlaying)
            {
                runTrail.Stop();
            }
        }
    }

    public void Score()
    {
        score += 1;        
    }
}
