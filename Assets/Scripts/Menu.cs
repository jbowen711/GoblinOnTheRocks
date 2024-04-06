using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas howToPlayCanvas;
    public Canvas creditsCanvas;

    public void StartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);    
    }

    public void HowToPlay()
    {
        menuCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(true);
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
    }

    public void Quit()
    {
        
    }

}
