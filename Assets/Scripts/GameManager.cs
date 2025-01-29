using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score;
    public bool devMode; //If checked, delete high score at end of game


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

}
