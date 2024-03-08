using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 position = transform.position;
        if(Input.GetKey(KeyCode.W))
        {
            position.x += speed * Time.deltaTime;
            Debug.Log(position);
        }
    }
}
