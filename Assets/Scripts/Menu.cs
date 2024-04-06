using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas howToPlayCanvas;
    public Canvas creditsCanvas;

    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;
    public GameObject tree5;


    public void StartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);    
    }

    public void HowToPlay()
    {
        menuCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(true);
        tree1.gameObject.SetActive(false);
        tree2.gameObject.SetActive(false);
        tree3.gameObject.SetActive(false);
        tree4.gameObject.SetActive(false);
        tree5.gameObject.SetActive(false);

    }

    public void Credits()
    {
        menuCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    public void Return()
    {
        menuCanvas.gameObject.SetActive(true);
        howToPlayCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        tree1.gameObject.SetActive(true);
        tree2.gameObject.SetActive(true);
        tree3.gameObject.SetActive(true);
        tree4.gameObject.SetActive(true);
        tree5.gameObject.SetActive(true);
    }

    public void Quit()
    {
        
    }

}
