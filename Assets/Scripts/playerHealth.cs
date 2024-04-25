using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class playerHealth : MonoBehaviour
{

    public int maxHealth = 5;
    public int health;

    [SerializeField]
    public GameObject[] hearts;


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
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= health)
            {
                hearts[i].SetActive(false);
            }
        }

    }

    public void Die() {
        // Destroy(gameObject);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);    
    }
}
