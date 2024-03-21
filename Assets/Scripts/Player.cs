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

    void Start()
    {
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(moveDirection.normalized * boostPower, ForceMode.Impulse);

        }

        //Orientation of facing diection
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

    }
}
