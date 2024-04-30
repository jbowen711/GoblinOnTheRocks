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

    // when press "How To Play" Button
    public void HowToPlay()
    {
        // disable start menu buttons
        menuCanvas.gameObject.SetActive(false);
        // enable tutorial canvas
        howToPlayCanvas.gameObject.SetActive(true);
        // get rid of trees
        tree1.gameObject.SetActive(false);
        tree2.gameObject.SetActive(false);
        tree3.gameObject.SetActive(false);
        tree4.gameObject.SetActive(false);
        tree5.gameObject.SetActive(false);

    }


    // when press "Credits" Button
    public void Credits()
    {
        // disable start menu buttons
        menuCanvas.gameObject.SetActive(false);
        // enable credits canvas
        creditsCanvas.gameObject.SetActive(true);
    }

    // when press "Return" Button
    public void Return()
    {
        // enable menu buttons
        menuCanvas.gameObject.SetActive(true);
        // disable tutorial canvas
        howToPlayCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        // bring back trees
        tree1.gameObject.SetActive(true);
        tree2.gameObject.SetActive(true);
        tree3.gameObject.SetActive(true);
        tree4.gameObject.SetActive(true);
        tree5.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        
    }

}
