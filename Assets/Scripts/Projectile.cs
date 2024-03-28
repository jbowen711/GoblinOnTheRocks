using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shootSpeed = 5f;
    public float shootDistance = 20f;

    

    private Transform goblinPos;


    void Start()
    {
        goblinPos = GameObject.FindGameObjectWithTag("Player").transform;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Timer
    }

    //Shoots towards player

}
