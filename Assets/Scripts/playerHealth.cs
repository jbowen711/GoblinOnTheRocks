using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class playerHealth : MonoBehaviour
{

    //Player Health variables
    public int maxHealth = 5;
    public int health;

    //Hearts UI
    [SerializeField]
    public GameObject[] hearts;


    void Start()
    {
        
        health = maxHealth;
    }
    void Update()
    {
        //No Health = death 
        if (health <= 0)
        {
            Die();
        }
        
    }

    public void TakeDamage(int damage)
    {

        //Take damage function - remove one heart from array
        health -= damage;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= health)
            {
                hearts[i].SetActive(false);
            }
        }

    }

    public void Die() {
        // Go back to menu screen
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);    
    }
}
