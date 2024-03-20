using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    void Start()
    {
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}
