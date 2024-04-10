using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public int maxHealth = 5;
    public int health;
    

    // Start is called before the first frame update
    void Start()
    {
        
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    public void Die() {
        Destroy(gameObject);
    }
}
