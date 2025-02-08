using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float score;
    public bool devMode; //If checked, delete high score at end of game


    public int stage = 1;
    public int enemiesKilled = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);


    }

    void InitializeHighScore()
    {
        if(PlayerPrefs.HasKey("highScore") == false)
        {
            PlayerPrefs.SetFloat("highScore", 0);
            Debug.Log("New player pref high score");
        }
        else
        {
            Debug.Log("High score exists already");
        }
    }

    public void CheckNewHighScore()
    {
        if(score > PlayerPrefs.GetFloat("highScore"))
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
    }

    public void ChangeScore(float _scoreAdded, TextMeshProUGUI _scoreText)
    {
        score += _scoreAdded;
        _scoreText.text = "Score: " + score;
    }

    public void EnterBossStage()
    {
        stage = 2;
        
    }

}
