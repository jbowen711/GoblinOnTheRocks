using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.CompilerServices;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreValue;

    private int score;

    [SerializeField]
    public GameObject player;

    void Start()
    {     
    }

    void Update()
    {
        if (player != null)
        {
            score = player.GetComponent<Player>().score;
            string scoreText = score.ToString();
            scoreValue.SetText(scoreText);
        }

    }
}
